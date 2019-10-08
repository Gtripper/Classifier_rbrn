using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Classifier;
using Classifier.Nodes;

namespace CodesMutator
{
    public static class CheckCodes
    {
        public static int CheckTypesIsConfirmityCodes(string vri, int type, int kind, 
            ref Dictionary<(int, int), int> dict, NodesCollection mf)
        {       

            Codes codes = new Codes(mf);
            codes.AddNodes(vri);

            TypeAndKind typeAndKind = new TypeAndKind(codes);

            var estimatedType = typeAndKind.Type;
            var estimatedKind = typeAndKind.Kind;

            if (type == estimatedType)
                return 0;
            else
            {
                if (dict.TryGetValue((estimatedType, type), out var value))
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("ВРИ => {0}", vri);
                    Console.WriteLine("Тип в таблице => {0}", type);
                    Console.WriteLine("Ожидвемый тип исходя из ВРИ {0}", estimatedType);

                    var custumErrorType = Convert.ToInt32(Console.ReadLine());
                    dict.Add((estimatedType, type), custumErrorType);

                    return custumErrorType;
                }
            }
        }
    }
}
