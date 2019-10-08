using MapInfoWrap;
using System;
using System.Collections.Generic;
using System.Linq;
using MapInfo;
using System.Text.RegularExpressions;
using System.Diagnostics;
using DBLayer;

namespace ConsoleControl
{
    class Program
    {
        static void Main(string[] args)
        {
            MapInfoAppControls map = new MapInfoAppControls(new MapinfoCurrentApp());
            map.TablesShow();
            var table = map.GetTable();

            var context = new Context();

            Console.WriteLine("Start reading and precessing...");

            var list = new List<BTI>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var cad_num = "";
            var cad_num_old = "";
            var unom = 0;
            Plot plot = null;

            foreach (var row in table.Rows)
            {
                if (row.RowID % 1000 == 0)
                {
                    Console.WriteLine("Row {0} затрачено времени {1} ", row.RowID, sw.Elapsed.Seconds.ToString());
                    sw.Restart();
                }

                cad_num = row["CAD_NUM"];
                unom = row["UNOM"];
                var building = context.BTIBuildings.FirstOrDefault(p => p.UNOM == unom);

                if (cad_num != cad_num_old)
                {
                    plot = context.Plots.FirstOrDefault(p => p.CadNum == cad_num);

                    if (plot != null && building != null)
                    {
                        plot.Buildings.Add(building);
                    }
                }
                else
                {
                    if (plot != null && building != null)
                    {
                        plot.Buildings.Add(building);
                    }
                }
                cad_num_old = cad_num;
            }            

            
            context.SaveChanges();
        }

        private static DateTime? dtConvert(string date)
        {
            if (date != null && DateTime.TryParse(date, out var dt))
                return dt;
            else
                return null;
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
