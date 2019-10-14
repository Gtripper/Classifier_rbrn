using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MapInfoWrap;
using JsonReader;
using System.Collections.Concurrent;
using DBLayer;

namespace ConsoleControl
{
    public class MapInfo2
    {
        private Table Plots { get; set; }

        public MapInfo2()
        {
            MapInfoAppControls map = new MapInfoAppControls(new MapinfoCurrentApp());
            map.TablesShow();

            Plots = map.GetTable();
        }

        public void Mutate()
        {
            var mf = new Classifier.Nodes.NodesCollection();
            var list = "";
            var type = 0;
            foreach (var row in Plots.Rows)
            {
                Console.WriteLine(row.RowID);
                try
                {
                    list = row["VRI_List"];
                    type = row["fs_tip"];
                }
                catch { continue; }
                var newList = CodesMutator.Mutator.Execute(list, type, mf);
                row["Vri_PZZ"] = newList;
            }
        }

        public void BackToPZZ()
        {
            foreach (var row in Plots.Rows)
            {
                Console.WriteLine(row.RowID);
                string list = row["VRI_List"];
                if (String.IsNullOrEmpty(list))
                    continue;
                else
                {
                    var result = CodesMutator.Mutator.BackToPZZ(list);
                    if (!String.IsNullOrEmpty(result))
                    {
                        row["Vri_PZZ"] = result;
                        row["Converted"] = true;
                    }
                }
            }
        }

        public void check()
        {
            var mf = new Classifier.Nodes.NodesCollection();
            var dict = new Dictionary<(int, int), int>();
            var list = "";
            var type = 0;
            var kind = 0;

            foreach (var row in Plots.Rows)
            {
                if (row.RowID % 10000 == 0)
                    Console.WriteLine(row.RowID);
                try
                {
                    list = row["VRI_List"];
                    type = row["fs_tip"];
                    kind = row["fs_vid"];
                }
                catch { continue; }
                var error = CodesMutator.CheckCodes.CheckTypesIsConfirmityCodes(list, type, kind, ref dict, mf);
                list = "";
                type = 0;
                kind = 0;

                row["Error"] = error;
            }
        }

        public void Execute()
        {
            var bag = new ConcurrentBag<Plot>();
            using (var context = new Context())
            {
                var plots = context.Plots.ToList();

                foreach (var _plot in plots)
                {
                    _plot.Buildings.ToList();
                    bag.Add(_plot);
                }
            }
            Parallel.ForEach(Plots.Rows, (row) =>
            {
                var rowID = row.RowID - 1;
                var data = Read(rowID, bag);
                if (data != null)
                {
                    var result = Processing(data);
                    Write(result, rowID);
                }
            });

        }

        private Classifier.IInputData Read(int rowID, ConcurrentBag<Plot> bag)
        {
            Monitor.Enter(Plots);
            var row = Plots.Rows[rowID];
            string cad_num = row["CAD_NUM"];
            string vri_doc = row["VRI_DOC"];
            int area = (int)(row["Площадь"] * 10000);
            Monitor.Exit(Plots);

            if (String.IsNullOrEmpty(cad_num))
            {
                return null;
            }
            var plot = bag.FirstOrDefault(p => p.CadNum.Equals(cad_num, StringComparison.InvariantCulture));
            if (plot != null)
            {
                return new Classifier.InputDataDB(plot, area);
            }
            else
            {
                string bti_code = "";
                bool lo = false;
                bool mid = false;
                bool hi = false;
                return new Classifier.InputData(vri_doc, area, bti_code, lo, mid, hi);
            }
        }

        private Classifier.IOutputData Processing(Classifier.IInputData data)
        {
            var factory = new Classifier.Factory(data);
            factory.Execute();

            return factory.outputData;
        }

        private void Write(Classifier.IOutputData data, int RowID)
        {
            Monitor.Enter(Plots);
            var row = Plots.Rows[RowID];
            row["VRI"] = data.VRI_List;
            row["Type"] = data.Type;
            row["Kind"] = data.Kind;
            Monitor.Exit(Plots);
        }
    }
}
