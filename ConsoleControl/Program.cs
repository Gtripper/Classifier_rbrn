using Classifier;
using MapInfoWrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleControl
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = MapInfo.CreateMap();
            Console.WriteLine("Enter current json name^");
            var jsonName = Console.ReadLine() + ".json";
            map.ReadData();
            map.ExecuteData(jsonName);
            map.WriteData();


            Console.Read();
        }
    }

    public class Doter
    {
        public void PrintDotToConsole()
        {
            Console.Write(".");
        }
    }
}
