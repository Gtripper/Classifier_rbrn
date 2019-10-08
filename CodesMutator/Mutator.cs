﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Classifier;
using Classifier.Nodes;

namespace CodesMutator
{
    public static class Mutator
    {
        public static Dictionary<string, string> map =
            new Dictionary<string, string> {
                { "1.0.0", "1.0"},
                { "1.1.0", "1.1"},
                { "1.2.0", "1.2"},
                { "1.3.0", "1.3"},
                { "1.4.0", "1.4"},
                { "1.5.0", "1.5"},
                { "1.6.0", "1.6"},
                { "1.7.0", "1.7"},
                { "1.8.0", "1.8"},
                { "1.9.0", "1.9"},
                { "1.10.0", "1.10"},
                { "1.11.0", "1.11"},
                { "1.12.0", "1.12"},
                { "1.13.0", "1.13"},
                { "1.14.0", "1.14"},
                { "1.15.0", "1.15"},
                { "1.16.0", "1.16"},
                { "1.17.0", "1.17"},
                { "1.18.0", "1.18"},
                { "2.0.0", "2.0"},
                { "2.1.0", "2.1"},
                { "2.1.1.0", "2.1.1"},
                { "2.2.0", "2.2"},
                { "2.3.0", "2.3"},
                { "2.4.0", "2.4"},
                { "2.5.0", "2.5"},
                { "2.6.0", "2.6"},
                { "2.7.0", "2.7"},
                { "2.7.1.0", "2.7.1"},
                { "3.0.0", "3.0"},
                { "3.1.1", "3.1.1"},
                { "3.1.2", "12.0.2"},
                { "3.1.3", "3.1.2"},
                { "3.2.1", "3.2.1"},
                { "3.2.2", "3.2.2"},
                { "3.2.3", "3.2.3"},
                { "3.2.4", "3.2.2"},
                { "3.3.0", "3.3"},
                { "3.4.0", "3.4"},
                { "3.4.1.0", "3.4.1"},
                { "3.4.2.0", "3.4.2"},
                { "3.5.1.0", "3.5.1"},
                { "3.5.2.0", "3.5.2"},
                { "3.6.1", "3.6.1"},
                { "3.6.2", "12.0.2"},
                { "3.6.3", "3.6.3"},
                { "3.7.1", "3.7.1"},
                { "3.7.2", "3.7.2"},
                { "3.8.1", "3.8.1"},
                { "3.8.2", "3.8.1"}, 
                { "3.8.3", "3.8.2"},
                { "3.9.2", "3.9.2"},
                { "3.9.3", "3.9.3"},
                { "3.9.4", "6.12"},
                { "3.9.5", "6.12"},
                { "3.9.1.0", "3.9.1"},
                { "3.10.1.0", "3.10.1"},
                { "3.10.2.0", "3.10.2"},
                { "4.0.0", "4.0"},
                //{ "4.0.0", "4.1, 4.2, 4.3, 4.4, 4.5, 4.6, 4.8, 4.9, 4.10"}, //Раскомментировать для ПЗЗ
                { "4.1.0", "4.1"},
                { "4.2.0", "4.2"},
                //{ "4.2.0", "4.5, 4.6, 4.8.1, 4.8.2" }, //Раскомментировать для ПЗЗ
                { "4.3.0", "4.3"},
                { "4.4.0", "4.4"},
                { "4.5.0", "4.5"},
                { "4.6.0", "4.6"},
                { "4.7.1", "4.7"},
                { "4.7.2", "4.7"},
                { "4.7.3", "3.2.4"},
                { "4.8.0", "4.8.1, 4.8.2"},
                { "4.9.0", "4.9"},
                { "4.9.1.1", "4.9.1.1"},
                { "4.9.1.2", "4.9.1.2"},
                { "4.9.1.3", "4.9.1.3"},
                { "4.9.1.4", "4.9.1.4"},
                { "4.10.0", "4.10"},
                { "5.0.1", "5.0"},
                //{ "5.0.1", "5.1., 5.1.2, 5.1.3, 5.1.4, 5.1.5, 5.1.6, 5.2, 5.3, 5.4, 5.5"}, //Same shit
                { "5.0.2", "5.0"},
                //{ "5.0.2", "5.1., 5.1.2, 5.1.3, 5.1.4, 5.1.5, 5.1.6, 5.2, 5.3, 5.4, 5.5"}, //Same shit
                { "5.1.1", "5.1.1"},
                { "5.1.2", "5.1.2"},
                { "5.1.3", "5.1.3, 5.1.4"},
                { "5.1.4", "5.1.5"},
                { "5.1.5", "5.1.7"},
                { "5.2.0", "5.2"},
                { "5.2.1.0", "5.2.1"},
                { "5.3.0", "5.3"},
                { "5.4.0", "5.4"},
                { "5.5.0", "5.5"},
                { "6.0.0", "6.0"},
                { "6.1.0", "6.1"},
                { "6.2.0", "6.2"},
                { "6.2.1.0", "6.2.1"},
                { "6.3.0", "6.3"},
                { "6.3.1.0", "6.3.1"},
                { "6.4.0", "6.4"},
                { "6.5.0", "6.5"},
                { "6.6.0", "6.6"},
                { "6.7.0", "6.7"},
                { "6.8.0", "6.8"},
                { "6.9.0", "6.9"},
                { "6.10.0", "6.10"},
                { "6.11.0", "6.11"},
                { "7.1.1", "7.1.1"}, /// SIC111               
                { "7.1.2", "7.1.2, 7.6"},
                { "7.2.1", "7.2.2, 12.0.1"}, /// SIC111                
                { "7.2.2", "7.2.3"},
                { "7.3.0", "7.3"},
                { "7.4.1", "7.4"},
                { "7.4.2", "7.4"},
                { "7.5.0", "7.5"},
                { "8.0.1", "8.0"},
                { "8.0.2", "8.0"},
                { "8.0.3", "8.0"},
                { "8.1.0", "8.1"},
                { "8.3.0", "8.3"},
                { "8.4.0", "8.4"},
                { "9.0.0", "9.0"},
                { "9.1.0", "9.1"},
                { "9.2.0", "9.2"},
                { "9.2.1.0", "9.2.1"},
                { "9.3.0", "9.3"},
                { "10.1.0", "10.1"},
                { "10.2.0", "10.2"},
                { "10.3.0", "10.3"},
                { "10.4.0", "10.4"},
                { "11.0.0", "11.0"},
                { "11.1.0", "11.1"},
                { "11.2.0", "11.2"},
                { "11.3.0", "11.3"},
                { "12.0.1", "12.0.2"},               
                { "12.0.2", "12.0.1"},               
                { "12.1.0", "12.1"},
                { "12.2.0", "12.2"},
                { "12.3.0", "12.3"},
                { "13.1.0", "13.1"},
                { "13.2.0", "13.2"},
                { "13.3.0", "13.2"}
            };

        public static Dictionary<string, string> backToPZZ =
            new Dictionary<string, string> {
                { "1.0", "1.0.0"},
                { "1.1", "1.1.0"},
                { "1.2", "1.2.0"},
                { "1.3", "1.3.0"},
                { "1.4", "1.4.0"},
                { "1.5", "1.5.0"},
                { "1.6", "1.6.0"},
                { "1.7", "1.7.0"},
                { "1.8", "1.8.0"},
                { "1.9", "1.9.0"},
                { "1.10", "1.10.0"},
                { "1.11", "1.11.0"},
                { "1.12", "1.12.0"},
                { "1.13", "1.13.0"},
                { "1.14", "1.14.0"},
                { "1.15", "1.15.0"},
                { "1.16", "1.16.0"},
                { "1.17", "1.17.0"},
                { "1.18", "1.18.0"},
                { "2.0", "2.0.0"},
                { "2.1", "2.1.0"},
                { "2.1.1", "2.1.1.0"},
                { "2.2", "2.2.0"},
                { "2.3", "2.3.0"},
                { "2.4", "2.4.0"},
                { "2.5", "2.5.0"},
                { "2.6", "2.6.0"},
                { "2.7", "2.7.0"},
                { "2.7.1", "2.7.1.0"},
                { "3.0", "3.0.0"},
                { "3.1.1", "3.1.1"},
                { "3.1.2", "3.1.3"},
                { "3.2.1", "3.2.1"},
                { "3.2.3", "3.2.3"},
                { "3.3", "3.3.0"},
                { "3.4", "3.4.0"},
                { "3.4.1", "3.4.1.0"},
                { "3.4.2", "3.4.2.0"},
                { "3.5.1", "3.5.1.0"},
                { "3.5.2", "3.5.2.0"},
                { "3.6.1", "3.6.1"},
                { "3.6.3", "3.6.3"},
                { "3.7.1", "3.7.1"},
                { "3.7.2", "3.7.2"},
                { "3.8.2", "3.8.3"},
                { "3.9.2", "3.9.2"},
                { "3.9.3", "3.9.3"},
                { "3.9.1", "3.9.1.0"},
                { "3.10.1", "3.10.1.0"},
                { "3.10.2", "3.10.2.0"},
                { "4.0", "4.0.0"},
                { "4.1", "4.1.0"},
                { "4.2", "4.2.0"},
                { "4.3", "4.3.0"},
                { "4.4", "4.4.0"},
                { "4.5", "4.5.0"},
                { "4.6", "4.6.0"},
                { "3.2.4", "4.7.3"},
                { "4.8.1", "4.8.0"},
                { "4.8.2", "4.8.0"},
                { "4.9", "4.9.0"},
                { "4.9.1.1", "4.9.1.1"},
                { "4.9.1.2", "4.9.1.2"},
                { "4.9.1.3", "4.9.1.3"},
                { "4.9.1.4", "4.9.1.4"},
                { "4.10", "4.10.0"},
                { "5.1.1", "5.1.1"},
                { "5.1.2", "5.1.2"},
                { "5.1.3", "5.1.3"},
                { "5.1.4", "5.1.3"},
                { "5.1.5", "5.1.4"},
                { "5.1.7", "5.1.5"},
                { "5.2", "5.2.0"},
                { "5.2.1", "5.2.1.0"},
                { "5.3", "5.3.0"},
                { "5.4", "5.4.0"},
                { "5.5", "5.5.0"},
                { "6.0", "6.0.0"},
                { "6.1", "6.1.0"},
                { "6.2", "6.2.0"},
                { "6.2.1", "6.2.1.0"},
                { "6.3", "6.3.0"},
                { "6.3.1", "6.3.1.0"},
                { "6.4", "6.4.0"},
                { "6.5", "6.5.0"},
                { "6.6", "6.6.0"},
                { "6.7", "6.7.0"},
                { "6.8", "6.8.0"},
                { "6.9", "6.9.0"},
                { "6.10", "6.10.0"},
                { "6.11", "6.11.0"},
                { "7.1.1", "7.1.1"}, /// SIC111               
                { "7.1.2", "7.1.2"},
                { "7.6", "7.1.2"},               
                { "7.2.3", "7.2.2"},
                { "7.3", "7.3.0"},
                { "7.5", "7.5.0"},
                { "8.1", "8.1.0"},
                { "8.3", "8.3.0"},
                { "8.4", "8.4.0"},
                { "9.0", "9.0.0"},
                { "9.1", "9.1.0"},
                { "9.2", "9.2.0"},
                { "9.2.1", "9.2.1.0"},
                { "9.3", "9.3.0"},
                { "10.1", "10.1.0"},
                { "10.2", "10.2.0"},
                { "11.0", "11.0.0"},
                { "11.1", "11.1.0"},
                { "11.2", "11.2.0"},
                { "11.3", "11.3.0"},
                { "12.1", "12.1.0"},
                { "12.2", "12.2.0"},
                { "12.3", "12.3.0"},
                { "13.1", "13.1.0"},
                { "13.2", "13.2.0"}
            };

        public static string BackToPZZ(string vri)
        {
            if (vri.Equals(""))
                return "";

            var pattern = @"\d+([.]\d+)?([.]\d+)?([.]\d+)?";
            var matches = Regex.Matches(vri, pattern).Cast<Match>().Select(p => p.Value);

            bool isAllCodesConverted = true;
            string result = "";

            foreach (var val in matches)
            {
                if (!backToPZZ.ContainsKey(val))
                    return "";
                else
                    result += result.Length == 0 ? backToPZZ[val] : ", " + backToPZZ[val];
            }

            return result;
        }

        public static string Execute(string vri, int type, INodesCollection mf)
        {
            var pattern = @"\d+[.]\d+([.]\d+)?([.]\d+)?";
            var matches = Regex.Matches(vri, pattern).Cast<Match>().Select(p => p.Value);

            Codes codes = new Codes(mf);

            foreach (var match in matches)
            {
                if (map.ContainsKey(match))
                {
                    if (match.Equals("12.0.1") && type == 500)
                        codes.AddNodes(match);
                    else if (match.Equals("12.0.2") && type == 400)
                        codes.AddNodes(match);
                    else
                        codes.AddNodes(map[match]);
                }
                else
                    codes.AddNodes(match);
            }

            codes.Sort();
            codes.Distinct();            

            return codes.Show;
        }

        public static string Execute540(string vri, INodesCollection mf)
        {
            var lmap = new List<(string, string)>();

            foreach (var kv in map)
            {
                lmap.Add((kv.Key.Substring(0, kv.Key.Length - 2), kv.Value));
            }

            var pattern = @"\d+[.]\d+([.]\d+)?([.]\d+)?";
            var matches = Regex.Matches(vri, pattern).Cast<Match>().Select(p => p.Value);
            var result = "";
            foreach (var match in matches)
            {           

                foreach (var val in lmap)
                {
                    if (match.Equals(val.Item1))
                        result += result.Length == 0? val.Item2 : ", " + val.Item2;
                }
            }

            Codes codes = new Codes(mf);
            codes.AddNodes(result);

            codes.Sort();
            codes.Distinct();

            return codes.Show;
        }
    }
}
