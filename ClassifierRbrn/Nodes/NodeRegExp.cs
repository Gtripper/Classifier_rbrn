using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classifier
{
    public interface INodeRegExp
    {
        string[] Regexep { get; }
        string GetSearchResult(string input);
    }

    public class NodeRegExp : INodeRegExp
    {
        public string[] Regexep { get; }

        public NodeRegExp(string[] _reg)
        {
            Regexep = _reg;
        }

        public string GetSearchResult(string input)
        {
            var positivePattern = "";
            var negativePattern = "";
            var match = "";
            for (int i = 0; i < Regexep.Length; i += 2)
            {
                negativePattern = Regexep[i];
                positivePattern = Regexep[i + 1];
                if (negativePattern.Equals(""))
                    match = NegativePatternIsNull(input, positivePattern);
                else
                    match = NegativePatternIsNotNull(input, negativePattern, positivePattern);

                if (!Equals(match, ""))
                    break;
            }
            return match;
        }

        /// <summary>
        /// Негативный паттерн не пустой
        /// </summary>
        /// <param name="negativePattern"></param>
        /// <param name="positivePattern"></param>
        /// <returns></returns>
        private string NegativePatternIsNotNull(string input, string negativePattern, string positivePattern)
        {
            var excluded = Regex.IsMatch(input, negativePattern, RegexOptions.IgnoreCase);
            var included = Regex.IsMatch(input, positivePattern, RegexOptions.IgnoreCase);

            if (!excluded & included)
            {
                var val = Regex.Match(input, positivePattern, RegexOptions.IgnoreCase).Value;
                return val;
            }
            return "";
        }

        /// <summary>
        /// Негативный паттерн пустой
        /// </summary>
        /// <param name="positivePattern"></param>
        /// <returns></returns>
        private string NegativePatternIsNull(string input, string positivePattern)
        {
            var included = Regex.IsMatch(input, positivePattern, RegexOptions.IgnoreCase);

            if (included)
            {
                var val = Regex.Match(input, positivePattern, RegexOptions.IgnoreCase).Value;
                return val;
            }
            return "";
        }

    }

    
}
