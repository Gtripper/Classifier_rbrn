using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Classifier
{
    public interface ICodeSeeker
    {
        void Seek();

        string Matches { get; }
        ICodes Codes { get; }
        bool IsFederalSearch { get; }
        bool IsPZZSearch { get; }
        bool IsMainSearch { get; }

        event Action FederalCodesDetected;
    }


    /// <summary>
    /// Определние кодов ПЗЗ по строке ВРИ по документу
    /// </summary>
    /// <remark>
    /// Выполняется поиск в три этапа:
    /// 1. Проверка на наличие федерального кода в строке вида (fCode)
    /// 1.1 В случае нахождения такого кода: 
    ///     - Отчистить Codes и Matches 
    ///     - Выполнить поиск по федеральным кодам
    /// 2. Проверка на наличие кодов ПЗЗ в начале строки
    /// 2.2 В случае успешной проверки - остановка цикла поиска
    /// 3. Поиск совпадений с регулярными выражениями regexpPatterns
    /// </remark>
    public class CodeSeeker : ICodeSeeker
    {
        private readonly string input; // ВРИ по документу
        private StringBuilder _matches;
        private INodesCollection mf;

        public string Matches { get { return _matches.ToString(); } }
        public ICodes Codes { get; }
        public bool IsFederalSearch { get; private set; }
        public bool IsPZZSearch { get; private set; }
        public bool IsMainSearch { get; private set; }

        #region event
        public event Action FederalCodesDetected;
        #endregion

        public CodeSeeker(string Input, ICodes codes, INodesCollection mf)
        {
            input = BullshitCleaner(Input);
            _matches = new StringBuilder("");
            Codes = codes;
            this.mf = mf;

            IsFederalSearch = false;
            IsPZZSearch = false;
            IsMainSearch = false;
        }

        public void Seek()
        {
            var nodes = mf.Nodes;
            var match = "";

            if (String.IsNullOrEmpty(input))
                Codes.AddNodes("12.3");
            else
            {
                foreach (var node in nodes)
                {
                    // Search Codes
                    var reg = FederalSearchRegexp(node.Code);
                    if (reg.IsMatch(input))
                    {
                        IsFederalSearch = true;
                        IsPZZSearch = false;
                        IsMainSearch = false;
                        SearchFederalCodes(node);
                        break;
                    }

                    match = SimpleDescriptionSearch(node.SimpleDescription);
                    if (!Equals(match, ""))
                    {
                        ClearOutputFields();
                        AddCodesVriByNode(node);
                        AddMatches(match);
                        IsFederalSearch = false;
                        IsPZZSearch = false;
                        IsMainSearch = false;
                        break;
                    }

                    match = node.GetSearchResult(input);
                    if (!Equals(match, ""))
                    {
                        AddCodesVriByNode(node);
                        AddMatches(match);
                        IsFederalSearch = false;
                        IsPZZSearch = false;
                        IsMainSearch = true;
                    }
                }
            }
        }

        #region FederalSearch
        /// <summary>
        /// Создание регулярного выражения дла поиска в строке 
        /// кодов из федерального классификатора, заключенного
        /// в круглые скобки
        /// </summary>
        /// <param name="fCode"></param>
        /// <returns></returns>
        /// <remark>
        /// Строка afterReplace необходима для того, что бы разделяющие "." в 
        /// федеральных кодах не считались любым символом
        /// </remark>
        private static Regex FederalSearchRegexp(string fCode)
        {
            var afterReplace = Regex.Replace(fCode, @"[.]", @"\s*[.]\s*", RegexOptions.IgnoreCase);

            var pattern = @"[(]\s*" + afterReplace + @"\s*[)]";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Добавление в Codes кодов ПЗЗ из массива CodeMapping
        /// </summary>
        /// <param name="codes"></param>
        private void AddCodesFromCodeMapping(List<Node> codes)
        {
            Codes.AddNodes(codes);
        }

        /// <summary>
        /// Полный поиск федеральных кодов во входящей строке
        /// </summary>
        internal void SearchFederalCodes(INode node)
        {
            ClearOutputFields();
            var match = "";
            var cuttedNodesCodes = mf.Nodes.
                Where(p => mf.Nodes.IndexOf(p) >= mf.Nodes.IndexOf(node)).
                Select(p => p.Code);

            foreach (var val in cuttedNodesCodes)
            {
                var reg = FederalSearchRegexp(val);

                if (reg.IsMatch(input))
                {
                    FederalCodesDetected?.Invoke();
                    Codes.AddNodes(val);
                    match = reg.Match(input).Value;
                    AddMatches(match);
                }
            }
        }
        #endregion

        #region SampleDescriptionSearch
        private string SimpleDescriptionSearch(string simpleDescription)
        {
            simpleDescription = simpleDescription.Replace("(", "[(]");
            simpleDescription = simpleDescription.Replace(")", "[)]");
            simpleDescription = simpleDescription.Replace(".", "[.]");
            var pattern = @"^[-.\s\d]*" + simpleDescription + "$";
            return Regex.Match(input, pattern, RegexOptions.IgnoreCase).Value;
        }
        #endregion

        public static string BullshitCleaner(string input)
        {
            var pattern = @"участки\s*размещения\s*.+?объектов:";
            return String.IsNullOrEmpty(input)? "" : Regex.Replace(input, pattern, " ", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Добавление нового совпадения в строку Matches
        /// </summary>
        /// <param name="match"></param>
        internal void AddMatches(string match)
        {
            match = match.Replace(@"\", "");
            match = match.Replace(@"""", "");
            if (_matches.Length == 0)
                _matches.Append(match);
            else
                _matches.Append("\n" + match);
        }

        /// <summary>
        /// Добавление нового кода ПЗЗ в Codes
        /// </summary>
        /// <param name="node"></param>
        /// <remark>
        /// В случае, если нашлась node с пустым кодом ПЗЗ,
        /// добавляются все коды ПЗЗ с одинаковым федеральным кодом
        /// (смотри для примера 7.0 или 3.4)
        /// </remark>
        internal void AddCodesVriByNode(INode node)
        {
            Codes.Add(node);
        }

        /// <summary>
        /// Обнуление полей Codes и Matches
        /// </summary>
        internal void ClearOutputFields()
        {
            Codes.Clear();
            _matches.Clear();
        }
    }
}
