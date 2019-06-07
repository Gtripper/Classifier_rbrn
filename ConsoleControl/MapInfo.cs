using Classifier;
using MapInfoWrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Serializators;
using Newtonsoft.Json;
using System.IO;

namespace ConsoleControl
{
    public class MapInfo
    {
        private MapInfoAppControls mapInfo;
        private Table table;
        private List<ObjectData> tableData;
        private Tuple<string, string, int>[] result;

        public static MapInfo CreateMap()
        {
            MapInfo map = new MapInfo();
            map.mapInfo = new MapInfoAppControls(new MapinfoCurrentApp());
            map.mapInfo.TablesShow();
            map.ChooseTable();
            map.tableData = new List<ObjectData>();
            map.result = new Tuple<string, string, int>[map.table.Lenght];

            return map;
        }

        public void ChooseTable()
        {
            table = mapInfo.ChooseTable();
        }

        public void ReadData()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
            Console.Write("Чтение данных:     [");
            mapInfo.Cycle(table, (it) =>
            {
                var doc = mapInfo.Eval(table.Name + @".Utilization__");
                var cadNum = mapInfo.Eval(table.Name + @".CadastralNumber__");
                var areaAsString = mapInfo.Eval(@"Int( Area( " + table.Name + @".Obj, ""sq m"") )");
                var area = Convert.ToInt32(areaAsString);
                string lvl = mapInfo.Eval(table.Name + @".этажи__");
                var bti = BTI_lvls(lvl);
                var klass = mapInfo.Eval(table.Name + @".назнач_старый_слой__");

                tableData.Add(ObjectData.Create(new InputData(klass, area, bti.Item1, bti.Item2, bti.Item3, bti.Item4, doc), it, cadNum));

                if (it % (table.Lenght / 20) == 0)
                {
                    Console.Write(".");
                }
            });
            Console.WriteLine("]    OK");
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
        }

        public void ExecuteData(string fileName)
        {
            var list = new List<ClassifierResults>();
            Parallel.ForEach(tableData, item =>
            {
                IFactory factory = new Factory(item.Data);
                factory.Execute();
                var index = tableData.IndexOf(item);
                result[index] = Tuple.Create(factory.outputData.VRI_List, factory.outputData.Matches, factory.outputData.Type);

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

            var resultAsJson = JsonConvert.SerializeObject(list, Formatting.Indented);

            string folder = @"M:\Mapinfo Files\Генплан_Уфа\Сущпол\Json";
            string path = Path.Combine(folder, fileName);

            FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(resultAsJson);
            file.Position = 0;

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
            while (it <= table.Lenght)
            {
                if (it % (table.Lenght / 20) == 0)
                    Console.Write(".");
                mapInfo.Do(@"Update " + table.Name + @" Set VRI__ = """ + result[it - 1].Item1 + @""" Where RowID = " + it);
                mapInfo.Do(@"Update " + table.Name + @" Set Matches__ = """ + result[it - 1].Item2 + @""" Where RowID = " + it);
                mapInfo.Do(@"Update " + table.Name + @" Set fs_tip = """ + result[it - 1].Item3.ToString() + @""" Where RowID = " + it);

                it++;
            }
            Console.WriteLine("]    OK");
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
        }


        private void main()
        {
            Console.WriteLine("FUCK");

            MapInfoAppControls mapInfo = new MapInfoAppControls(new MapinfoCurrentApp());

            mapInfo.TablesShow();

            Table table = mapInfo.ChooseTable();

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

            Tuple<string, string, int>[] result = new Tuple<string, string, int>[table.Lenght];

            Console.WriteLine("Начало обработки");

            Parallel.ForEach(list, item =>
            {
                IFactory factory = new Factory(item);
                factory.Execute();
                var index = list.IndexOf(item);
                result[index] = (Tuple.Create(factory.outputData.VRI_List, factory.outputData.Matches, factory.outputData.Type));
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

            public static ObjectData Create(IInputData _data, int _rowID, string _cadNum)
            {
                ObjectData data = new ObjectData();

                data.Data = _data;
                data.RowID = _rowID;
                data.CadNum = _cadNum;

                return data;
            }
        }
    }
}
