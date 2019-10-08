using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MapInfoWrap;
using JsonReader;

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
            IEnumerable<PlotsFeatures.Properties> props;
            using (Plots collection = JsonReader.Plots.GetPlots())
            {
                props = collection.Proprties;
            }
                Parallel.ForEach(Plots.Rows, (row) =>
                {
                    var rowID = row.RowID - 1;
                    var data = Read(rowID, props);
                    var result = Processing(data);
                    Write(result, rowID);
                });
            
        }

        private Classifier.InputData Read(int rowID, IEnumerable<PlotsFeatures.Properties> props)
        {
            Monitor.Enter(Plots);
            var row = Plots.Rows[rowID];
            string cad_num = row["CAD_NUM"];
            string vri_doc = "";
            //if (cad_num.Equals(""))
            //{
            vri_doc = row["VRI_DOC"];
            //}
            //else
            //{
            //    string vri_doc_fromJson = props.FirstOrDefault(p => p.CadNum.Equals(cad_num)).VriDoc;
            //    vri_doc = vri_doc_fromJson == null ? row["VRI_DOC"] : vri_doc_fromJson;
            //}
            string bti_code = "";
            bool lo = false;
            bool mid = false;
            bool hi = false;
            Monitor.Exit(Plots);

            var data = new Classifier.InputData(vri_doc, 400, bti_code, lo, mid, hi);
            return data;
        }

        private Classifier.IOutputData Processing(Classifier.InputData data)
        {
            var factory = new Classifier.Factory(data);
            factory.Execute();

            return factory.outputData;
        }

        private void Write(Classifier.IOutputData data, int RowID)
        {
            Monitor.Enter(Plots);
            var row = Plots.Rows[RowID];
            row["VRI_List"] = data.VRI_List;
            row["fs_tip"] = data.Type;
            row["fs_vid"] = data.Kind;
            Monitor.Exit(Plots);
        }
    }
}
