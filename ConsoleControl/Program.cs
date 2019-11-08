using MapInfoWrap;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using MapInfo;
using System.Text.RegularExpressions;
using System.Diagnostics;
using DBLayer;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleControl
{
    class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Не передавать литералы в качестве локализованных параметров", Justification = "<Ожидание>")]
        static void Main()
        {
            var mf = new Classifier.Nodes.NodesCollection();            

            var app = new MapInfoAppControls(new MapinfoCurrentApp());
            app.TablesShow();

            Context context = null;
            try
            {
                context = new Context();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                context.Dispose();
            }

            var table = app.GetTable();
            Console.WriteLine("Number of rows is: {0}", table.Lenght);
            var sw = new Stopwatch();
            sw.Start();
            foreach (var val in table.Rows)
            {
                
                 Console.WriteLine("{0} rows was comleted", val.RowID);                
                

                try
                {
                    string vri_egrn = string.IsNullOrEmpty(val["Наименование_ОКС"]) ? val["Назначение"] : val["Наименование_ОКС"];                    
                    string vri_dgi = val["ДГИ_назначение"];

                    if (!string.IsNullOrEmpty(vri_egrn))
                    {
                        Classifier.InputData data = new Classifier.InputData(vri_egrn, 600);
                        Classifier.Factory factory = new Classifier.Factory(data);
                        factory.Execute();
                        var vri = factory.outputData.VRI_List;
                        val["VRI_EGRN"] = vri;
                    }

                    if (!string.IsNullOrEmpty(vri_dgi))
                    {
                        Classifier.InputData data = new Classifier.InputData(vri_dgi, 600);
                        Classifier.Factory factory = new Classifier.Factory(data);
                        factory.Execute();
                        var vri = factory.outputData.VRI_List;
                        val["VRI_DGI"] = vri;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }

            sw.Stop();
        }

        private static DateTime? dtConvert(string date)
        {
            if (date != null && DateTime.TryParse(date, out var dt))
                return dt;
            else
                return null;
        }

        private void uselessshit()
        {
            MapInfoAppControls map = new MapInfoAppControls(new MapinfoCurrentApp());
            map.TablesShow();
            var table = map.GetTable();

            var cadNums = new ConcurrentDictionary<(string, double), Classifier.IFactory>();

            Console.WriteLine("Start collecting cad_nums");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (var row in table.Rows)
            {
                if (row.RowID % 10000 == 0)
                {
                    Console.WriteLine("{0} Elapsed time => {1}", row.RowID, sw.Elapsed.Seconds);
                    sw.Restart();
                }

                string cadNum = row["CAD_NUM"];
                double area = row["Площадь"] * 10000;
                if (cadNum.Length > 10)
                    cadNums.TryAdd((cadNum, area), null);
            }

            Console.WriteLine("Start processing cad_nums");
            sw.Restart();

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

            Parallel.ForEach(cadNums, (item) =>
            {
                var plot = bag.FirstOrDefault(p => p.CadNum.Equals(item.Key.Item1, StringComparison.InvariantCulture));

                if (plot != null)
                {
                    var data = new Classifier.InputDataDB(plot, (int)item.Key.Item2);
                    var factory = new Classifier.Factory(data);
                    factory.Execute();
                    cadNums[item.Key] = factory;
                }
            });

            Console.WriteLine("Start writing data");

            foreach (var row in table.Rows)
            {
                if (row.RowID % 10000 == 0)
                {
                    Console.WriteLine("{0} Elapsed time => {1}", row.RowID, sw.Elapsed.Seconds);
                    sw.Restart();
                }

                string cadNum = row["CAD_NUM"];
                if (cadNum.Length > 10)
                {
                    var data = cadNums.FirstOrDefault(p => p.Key.Item1.Equals(cadNum, StringComparison.InvariantCulture)).Value;
                    if (data != null)
                    {
                        row["VRI"] = data.outputData.VRI_List;
                        row["Type"] = data.outputData.Type;
                        row["Kind"] = data.outputData.Kind;
                    }
                }
            }
        }

        private void BDTemp()
        {
            MapInfoAppControls map = new MapInfoAppControls(new MapinfoCurrentApp());
            map.TablesShow();
            var table = map.GetTable();
            var query = map.GetTable();

            var context = new Context();

            Console.WriteLine("Start reading and precessing...");

            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo.BTIs]");

            foreach (var val in context.BTIBuildings)
            {
                context.BTIBuildings.Remove(val);
                Console.WriteLine(val.UNOM);
            }
            context.SaveChanges();

            Console.WriteLine(context.BTIBuildings.ToList().Count());

            var dict = new Dictionary<int, string>();
            var list = new List<BTI>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (var val in query.Rows)
            {
                if (val.RowID % 1000 == 0)
                {
                    Console.WriteLine("{0} Elapsed time = {1}", val.RowID, sw.Elapsed.Seconds);
                    sw.Restart();
                }
                var unom = val["UNOM"];
                var cad_num = val["CAD_NUM"];

                if (unom == 0)
                    continue;

                if (dict.ContainsKey(unom))
                {
                    continue;
                }
                else
                    dict.Add(unom, cad_num);
            }


            sw.Restart();
            foreach (var row in table.Rows)
            {
                if (row.RowID % 1000 == 0)
                {
                    Console.WriteLine("Row {0} затрачено времени {1} ", row.RowID, sw.Elapsed.Seconds.ToString());
                    sw.Restart();
                }

                var unom = row["UNOM"];
                var cad_num = dict.ContainsKey(unom) ? dict[unom] : null;

                //var bti = DBLayer.BTI.Create(0, (int)row[0], (string)row[1], (string)row[2], (string)row[3], (string)row[4]
                //    , (string)row[5], row[8], (int?)row[15], (string)row[86], (string)row[87], (double?)row[89]
                //    , (int?)row[90], (int?)row[92], (string)row[100], (int)row[101], (int)row[102], cad_num);

                //list.Add(bti);

            }

            sw.Restart();
            int it = 0;
            foreach (var val in list)
            {
                if (it++ % 1000 == 0)
                {
                    Console.WriteLine("{0} Elapsed time = {1}", it, sw.Elapsed.Seconds);
                    sw.Restart();
                }

                context.BTIBuildings.Add(val);
            }
            context.SaveChanges();


            Console.ReadKey();
        }
    }

    public class Doter
    {
        public void PrintDotToConsole()
        {
            Console.Write(".");
        }

        private void SpecialMethod()
        {
            var map = MapInfo.CreateMap();
            var mapinfo = map.mapInfo;

            Table tab = mapinfo.GetTable();





            DMapInfo inst = map.mapInfo.instance;
            DMIMapGen mapGen = inst.MIMapGen;
            DMITable table = mapGen.GetTable("БТИ_2019");

            DMIFields columns = table.Fields;

            foreach (DMIField col in columns)
            {

                Console.WriteLine(col.Name());
                Console.WriteLine(col.Type());
            }

            DMIRows rows = table.Rows;

            List<string> list = new List<string>();



            foreach (DMIRow row in rows)
            {
                list.Add(row.Value[(object)"VRI"]);
            }

            var set = new List<string>();
            var pattern = @"\d+[.]\d+([.]\d+)?([.]\d+)?";
            foreach (var val in list)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    var result = Regex.Matches(val, pattern).Cast<Match>().Select(p => p.Value).ToList();
                    set.AddRange(result);
                }
            }

            var res = set.Distinct();

            foreach (var val in res)
            {
                Console.WriteLine(val);
            }
        }
    }
}
