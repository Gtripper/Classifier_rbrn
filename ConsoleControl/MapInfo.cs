using Classifier;
using MapInfoWrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serializators;
using Newtonsoft.Json;
using System.IO;
using JsonReader;

namespace ConsoleControl
{
    public class MapInfo
    {
        public MapInfoAppControls mapInfo;
        private Table table;
        private List<ObjectData> tableData;
        private Tuple<string, IOutputData>[] result;
        private string json;

        public static MapInfo CreateMap()
        {
            MapInfo map = new MapInfo();
            map.mapInfo = new MapInfoAppControls(new MapinfoCurrentApp());
            map.mapInfo.TablesShow();
            map.ChooseTable();
            map.tableData = new List<ObjectData>();
            map.result = new Tuple<string, IOutputData>[map.table.Lenght];
            map.json = null;
            return map;
        }

        public void ChooseTable()
        {
            table = mapInfo.GetTable();
        }

        public void SinchroRun()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));

            using (Plots collections = Plots.GetPlots())
            {
                var props = collections.Proprties;
                var end = table.Lenght;
                var it = 1;
                Console.Write("Чтение данных:     [");
                while (it < end)
                {
                    mapInfo.Do(@"Fetch Rec " + it + @" From " + table.Name);
                    var doc = mapInfo.Eval(table.Name + @".VRI_DOC");
                    var cadNum = mapInfo.Eval(table.Name + @".CAD_NUM");
                    var areaAsString = mapInfo.Eval(@"Int( Area( " + table.Name + @".Obj, ""sq m"") )");
                    var area = Convert.ToInt32(areaAsString);
                    var lo = mapInfo.Eval(table.Name + @".lo_lvl");
                    var lo_lvl = (lo == "true") ? true : false;
                    var mid = mapInfo.Eval(table.Name + @".mid_lvl");
                    var mid_lvl = (mid == "true") ? true : false;
                    var hi = mapInfo.Eval(table.Name + @".hi_lvl");
                    var hi_lvl = (hi == "true") ? true : false;
                    var klass = mapInfo.Eval(table.Name + @".VRI_KLASSI");

                    if (doc.Length == 254)
                    {
                        doc = props.FirstOrDefault(p => p.CadNum.Equals(cadNum)).VriDoc;
                    }
                    var inputData = new InputData(klass, area, "", lo_lvl, mid_lvl, hi_lvl, doc);
                    var factory = new Factory(inputData);
                    factory.Execute();


                    var rowid = it.ToString();

                    mapInfo.Do(@"Update " + table.Name + @" Set VRI_List = """ + factory.outputData.VRI_List + @""" Where RowID = " + rowid);
                    mapInfo.Do(@"Update " + table.Name + @" Set VRI_Sorted = """ + factory.outputData.VRI_List + @""" Where RowID = " + rowid);
                    mapInfo.Do(@"Update " + table.Name + @" Set Matches = """ + factory.outputData.Matches + @""" Where RowID = " + rowid);
                    mapInfo.Do(@"Update " + table.Name + @" Set fs_tip = """ + factory.outputData.Type.ToString() + @""" Where RowID = " + rowid);
                    mapInfo.Do(@"Update " + table.Name + @" Set fs_vid = """ + factory.outputData.Kind.ToString() + @""" Where RowID = " + rowid);

                    it++;
                    if (it % (table.Lenght / 20) == 0)
                    {
                        Console.Write(".");
                    }
                }
            }
            Console.WriteLine("]    OK");
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
        }

        public IEnumerable<PlotsFeatures.Properties> GetProperties()
        {
            using (Plots collections = Plots.GetPlots())
            {
                return collections.Proprties;
            }
        }

        public void ReadData()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));


            var props = GetProperties();

            GC.Collect();

            Console.Write("Чтение данных:     [");
            mapInfo.Cycle(table, (it) =>
            {
                    //var doc = mapInfo.Eval(table.Name + @".VRI_DOC");
                    var cadNum = mapInfo.Eval(table.Name + @".CAD_NUM");
                var areaAsString = mapInfo.Eval(@"Int( Area( " + table.Name + @".Obj, ""sq m"") )");
                var area = Convert.ToInt32(areaAsString);
                var lo = mapInfo.Eval(table.Name + @".lo_lvl");
                var lo_lvl = (lo == "true") ? true : false;
                var mid = mapInfo.Eval(table.Name + @".mid_lvl");
                var mid_lvl = (mid == "true") ? true : false;
                var hi = mapInfo.Eval(table.Name + @".hi_lvl");
                var hi_lvl = (hi == "true") ? true : false;
                var klass = mapInfo.Eval(table.Name + @".VRI_KLASSI");

                var doc = props.FirstOrDefault(p => p.CadNum.Equals(cadNum)).VriDoc;



                tableData.Add(ObjectData.Create(new InputData(doc, area, "", lo_lvl, mid_lvl, hi_lvl, klass), it + 1, cadNum));

                if (it % (table.Lenght / 20) == 0)
                {
                    Console.Write(".");
                }
            });

            Console.WriteLine("]    OK");
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
        }

        public void ExecuteData(string fileName = "current.json")
        {
            var list = new List<ClassifierResults>();
            Parallel.ForEach(tableData, item =>
            {
                IFactory factory = new Factory(item.Data);
                factory.Execute();
                item.OutputData = factory.outputData;

                ClassifierResults classifierResults = ClassifierResults.Create(
                      item.RowID
                    , item.CadNum
                    , factory.outputData.VRI_List
                    , factory.outputData.Matches
                    , factory.outputData.IsMainSearch
                    , factory.outputData.IsPZZSearch
                    , factory.outputData.IsFederalSearch
                    , factory.outputData.IsLandscape
                    , factory.outputData.IsMaintenance
                    , factory.outputData.Type
                    , factory.outputData.Kind);
                list.Add(classifierResults);
            });

            list = list.OrderBy(p => p.ID).ToList();

            json = JsonConvert.SerializeObject(list, Formatting.Indented);

            string folder = @"M:\Mapinfo Files\Генплан_Уфа\Сущпол\Json";
            string path = Path.Combine(folder, fileName);

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(json);
            writer.Flush();

            writer.Close();
            writer.Dispose();

            file.Close();
            file.Dispose();
        }

        public void WriteData()
        {
            int it = 1;
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
            Console.Write("Запись результата: [");
            //while (it <= table.Lenght)
            //{
            //    if (it % (table.Lenght / 20) == 0)
            //        Console.Write(".");
            //    mapInfo.Do(@"Update " + table.Name + @" Set VRI_List = """ + result[it - 1].Item1 + @""" Where RowID = " + it);
            //    mapInfo.Do(@"Update " + table.Name + @" Set VRI_Sorted = """ + result[it - 1].Item1 + @""" Where RowID = " + it);
            //    mapInfo.Do(@"Update " + table.Name + @" Set Matches = """ + result[it - 1].Item2 + @""" Where RowID = " + it);
            //    mapInfo.Do(@"Update " + table.Name + @" Set fs_tip = """ + result[it - 1].Item3.ToString() + @""" Where RowID = " + it);
            //    mapInfo.Do(@"Update " + table.Name + @" Set fs_vid = """ + result[it - 1].Item4.ToString() + @""" Where RowID = " + it);
            //    it++;
            //}

            //mapInfo.Cycle(table, (it) =>
            //{
            //    var cadNum = mapInfo.Eval(table.Name + @".CAD_NUM");
            //    var rowID = Convert.ToInt32(mapInfo.Eval(table.Name + @".RowID"));
            //    var res = result.FirstOrDefault(p => p.Item1.Equals(cadNum)).Item2;

            //    mapInfo.Do(@"Update " + table.Name + @" Set VRI_List = """ + res.VRI_List + @""" Where RowID = " + rowID);
            //    mapInfo.Do(@"Update " + table.Name + @" Set VRI_Sorted = """ + res.VRI_List + @""" Where RowID = " + rowID);
            //    mapInfo.Do(@"Update " + table.Name + @" Set Matches = """ + res.Matches + @""" Where RowID = " + rowID);
            //    mapInfo.Do(@"Update " + table.Name + @" Set fs_tip = """ + res.Type.ToString() + @""" Where RowID = " + rowID);
            //    mapInfo.Do(@"Update " + table.Name + @" Set fs_vid = """ + res.Kind.ToString() + @""" Where RowID = " + rowID);

            //    if (it % (table.Lenght / 20) == 0)
            //    {
            //        Console.Write(".");
            //    }
            //});

            foreach (var row in tableData)
            {
                mapInfo.Do(@"Update " + table.Name + @" Set VRI_List = """ + row.OutputData.VRI_List + @""" Where RowID = " + row.RowID);
                mapInfo.Do(@"Update " + table.Name + @" Set VRI_Sorted = """ + row.OutputData.VRI_List + @""" Where RowID = " + row.RowID);
                mapInfo.Do(@"Update " + table.Name + @" Set fs_tip = """ + row.OutputData.Type.ToString() + @""" Where RowID = " + row.RowID);
                mapInfo.Do(@"Update " + table.Name + @" Set fs_vid = """ + row.OutputData.Kind.ToString() + @""" Where RowID = " + row.RowID);
                mapInfo.Do(@"Update " + table.Name + @" Set Matches = """ + row.OutputData.Matches + @""" Where RowID = " + row.RowID);


                if (it % (table.Lenght / 20) == 0)
                {
                    Console.Write(".");
                }
                it++;
            }

            Console.WriteLine("]    OK");
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
        }

        public bool ValidateData()
        {
            var list = JsonConvert.DeserializeObject<List<ClassifierResults>>(json);
            bool result = true;
            mapInfo.Cycle(table, (it) =>
            {
                var vri = mapInfo.Eval(table.Name + @".VRI__");
                var match = mapInfo.Eval(table.Name + @".Matches__");
                var tip = mapInfo.Eval(table.Name + @".fs_tip");
                if (
                list[it - 1].VRI_List != vri ||
                list[it - 1].Matches != match ||
                list[it - 1].Type.ToString() != tip)
                    result = false;
            });
            return result;
        }


        private void main()
        {
            Console.WriteLine("FUCK");

            MapInfoAppControls mapInfo = new MapInfoAppControls(new MapinfoCurrentApp());

            mapInfo.TablesShow();

            Table table = mapInfo.GetTable();

            var list = new List<IInputData>();

            Console.WriteLine("Начало считывания");
            Console.WriteLine("[");

            mapInfo.Cycle(table, (k) =>
            {
                var doc = mapInfo.Eval(table.Name + @".Utilization");
                var areaAsString = mapInfo.Eval(@"Int( Area( " + table.Name + @".Obj, ""sq m"") )");
                var area = Convert.ToInt32(areaAsString);
                string lvl = mapInfo.Eval(table.Name + @".этажи");
                var bti = BTI_lvls(lvl);
                var klass = mapInfo.Eval(table.Name + @".назнач_старый_слой");

                list.Add(new InputData(doc, area, bti.Item1, bti.Item2, bti.Item3, bti.Item4, klass));
            });

            Tuple<string, string, int, int>[] result = new Tuple<string, string, int, int>[table.Lenght];

            Console.WriteLine("Начало обработки");

            Parallel.ForEach(list, item =>
            {
                IFactory factory = new Factory(item);
                factory.Execute();
                var index = list.IndexOf(item);
                result[index] = (Tuple.Create(factory.outputData.VRI_List, factory.outputData.Matches, factory.outputData.Type, factory.outputData.Kind));
            });

            int it = 1;

            Console.WriteLine("Начало записи");

            while (it <= table.Lenght)
            {
                if (it % 1000 == 0)
                    Console.WriteLine("Запись: итерация {0}", it);
                mapInfo.Do(@"Update " + table.Name + @" Set VRI = """ + result[it - 1].Item1 + @""" Where RowID = " + it);
                mapInfo.Do(@"Update " + table.Name + @" Set Matches = """ + result[it - 1].Item2 + @""" Where RowID = " + it);
                mapInfo.Do(@"Update " + table.Name + @" Set fs_tip = """ + result[it - 1].Item3.ToString() + @""" Where RowID = " + it);
                mapInfo.Do(@"Update " + table.Name + @" Set fs_vid = """ + result[it - 1].Item4.ToString() + @""" Where RowID = " + it);

                it++;
            }

            Console.Read();
        }

        private static Tuple<string, bool, bool, bool> BTI_lvls(string lvl)
        {
            if (Int32.TryParse(lvl, out int level))
            {
                if (level <= 0)
                    return Tuple.Create("", false, false, false);
                else if (level > 0 && level <= 4)
                    return Tuple.Create("", true, false, false);
                else if (level > 4 && level <= 8)
                    return Tuple.Create("", false, true, false);
                else
                    return Tuple.Create("", false, false, true);
            }
            else
                return Tuple.Create("", false, false, false);
        }

        public class ObjectData
        {
            public IInputData Data { get; private set; }
            public int RowID { get; private set; }
            public string CadNum { get; private set; }

            public IOutputData OutputData { get; set; }

            public static ObjectData Create(IInputData _data, int _rowID, string _cadNum)
            {
                ObjectData data = new ObjectData();

                data.Data = _data;
                data.RowID = _rowID;
                data.CadNum = _cadNum;
                data.OutputData = null;
                return data;
            }
        }
    }
}
