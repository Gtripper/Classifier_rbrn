﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Classifier.Nodes;

namespace Classifier
{
    public interface ICodeHandler
    {
        bool Maintenance { get; }
        bool Landscaping { get; }
        ICodes Codes { get; }
        void FullProcessing();

        
        event Action<string> Cutter;
        void IsFederal(bool state, string msg);
    }

    /// <summary>
    /// Содержит методы обработки кодов ПЗЗ
    /// </summary>
    class CodeHandler : ICodeHandler
    {
        private string input;
        private int area; /// TODO: Пока так. Пока не готов полноценный интерфейс со всеми данными из MapInfo
        private bool isFederal;
        private List<string> node;
        private bool uncut;
        public ICodes Codes { get; private set; }
        private IBTI bti;
        private INodesCollection mf;

        public event Action<string> Cutter;

        /// <summary>
        /// Эксплуатация
        /// </summary>
        public bool Maintenance { get => _maintenance(); }

        /// <summary>
        /// Благоустройство
        /// </summary>
        public bool Landscaping { get => _landscaping(); }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="Codes"></param>
        /// <param name="bti"></param>
        /// <param name="input"></param>
        public CodeHandler(ICodes _Codes, IBTI _bti, string _input, int _area, INodesCollection mf)
        {
            // Проверка на null
            Codes = _Codes ?? new Codes(mf);
            bti = _bti ?? new BTI();
            input = _input;
            area = _area;
            uncut = true;
            isFederal = false;
            node = new List<string>();
            this.mf = mf;
        }
        #region Behavior
        /// <summary>
        /// Удаляет базовые коды при наличии уточняющих
        /// </summary>
        /// <remark>
        /// Например если есть коды {6.0.0, 6.2.0} на 
        /// выходе должен остаться только индекс {6.2.0}
        /// </remark>
        private void RemoveBaseCodes()
        {
            #region Dictionary
            var baseCodes = new Dictionary<string, List<string>>();
            baseCodes.Add("1.0", new List<string> { "1.1", "1.2", "1.3", "1.4", "1.5",
                "1.6", "1.7", "1.8", "1.9", "1.10", "1.11", "1.11", "1.12", "1.13",
                    "1.14", "1.15", "1.16", "1.17", "1.18", "1.19", "1.20"});
            baseCodes.Add("1.1", new List<string> { "1.2", "1.3", "1.4", "1.5", "1.6" });
            baseCodes.Add("1.7", new List<string> { "1.8", "1.9", "1.10", "1.11", "1.15", "1.19", "1.20" });
            baseCodes.Add("2.0", new List<string> { "2.1", "2.1.1", "2.2", "2.3", "2.5", "2.6"});
            baseCodes.Add("2.7", new List<string> { "3.1", "3.2", "3.3", "3.4", "3.4.1.0", "3.5.1.0",
                "3.6", "3.7", "3.10.1", "4.1", "4.3", "4.4", "4.6", "5.1.2", "5.1.3" });
            baseCodes.Add("3.0", new List<string> { "3.1", "3.1.1", "3.1.2", "3.2", "3.2.1", "3.2.2", "3.2.3",
                "3.2.4", "3.3", "3.4", "3.4.1", "3.4.2", "3.4.3", "3.5", "3.5.1", "3.5.2", "3.6", "3.6.1",
                    "3.6.2", "3.6.3", "3.7", "3.7.1", "3.7.2", "3.8", "3.8.1", "3.8.2", "3.9", "3.9.1",
                        "3.9.2", "3.9.3", "3.9", "3.10.1", "3.10.2"});
            baseCodes.Add("3.1", new List<string> { "3.1.1", "3.1.2" });
            baseCodes.Add("3.2", new List<string> { "3.2.1", "3.2.2", "3.2.3", "3.2.4" });
            baseCodes.Add("3.4", new List<string> { "3.4.1", "3.4.2", "3.4.3" });
            baseCodes.Add("3.5", new List<string> { "3.5.1", "3.5.2" });
            baseCodes.Add("3.6", new List<string> { "3.6.1", "3.6.2", "3.6.3" });
            baseCodes.Add("3.7", new List<string> { "3.7.1", "3.7.2" });
            baseCodes.Add("3.8", new List<string> { "3.8.1", "3.8.2" });
            baseCodes.Add("3.9", new List<string> { "3.9.1", "3.9.2", "3.9.3" });
            baseCodes.Add("3.10", new List<string> { "3.10.1", "3.10.2" });
            baseCodes.Add("4.0.0", new List<string> { "4.1", "4.2", "4.3", "4.4", "4.5",
                "4.6", "4.7", "4.8", "4.8.1", "4.8.2", "4.8.3", "4.9", "4.9.1", "4.9.1.1",
                    "4.9.1.2", "4.9.1.3", "4.9.1.4", "4.10" });
            baseCodes.Add("4.9.1", new List<string> { "4.9.1.1", "4.9.1.2", "4.9.1.3", "4.9.1.4" });
            baseCodes.Add("5.0", new List<string> { "5.1", "5.1.1", "5.1.2", "5.1.3", "5.1.4",
                "5.1.5", "5.1.6", "5.1.7", "5.2", "5.2.1", "5.3", "5.4", "5.5" });
            baseCodes.Add("5.1", new List<string> { "5.1.1", "5.1.2", "5.1.3", "5.1.4", "5.1.5", "5.1.6", "5.1.7" });
            baseCodes.Add("6.0", new List<string> { "6.1", "6.2", "6.2.1", "6.3", "6.3.1",
                "6.4", "6.5", "6.6", "6.7", "6.8", "6.11" });
            #endregion

            var listOfVri = Codes.Nodes.Select(p => p.Code);
            // Коллекция базовых кодов, присутствующая в Codes
            var intersect = baseCodes.Keys.ToList().Intersect(listOfVri);

            if (intersect.Count() > 0)
            {
                foreach (var val in intersect)
                {
                    // Пересечение производных от базовых кодов
                    // и Codes
                    if (baseCodes[val].Intersect(listOfVri).Count() > 0)
                    {
                        Codes.RemoveAll(val);
                    }
                }
            }
            Codes.Sort();
        }

        /// <summary>
        /// Определяет код исходя из этажности объекта БТИ
        /// </summary>
        /// <remark>
        /// Проперти lo, mid и hi для объекта БТИ проставляются только
        /// для многоэтажных многоквартирных домов. Эти индексы никак не касаются 
        /// общественной, деловой, промышленной и прочей застройки
        /// </remark>
        /// TODO: Посмотреть на реальных участках, где в ВРИ указано
        /// малоэтажные многоквартирные дома
        private void NumberDeterminant()
        {
            var list = new List<string> { "2.1.1", "2.5", "2.6" };
            bool IsApartmentOrBaseHouse = Codes.Exists(list);

            if (IsApartmentOrBaseHouse && bti.Lo_lvl)
            {
                Codes.RemoveAll(list);
                Codes.AddNodes("2.1.1");
            }
            if (IsApartmentOrBaseHouse && bti.Mid_lvl)
            {
                Codes.RemoveAll(list);
                Codes.AddNodes("2.5");
            }
            if (IsApartmentOrBaseHouse && bti.Hi_lvl)
            {
                Codes.RemoveAll(list);
                Codes.AddNodes("2.6");
            }
            Codes.Sort();
        }

        /// <summary>
        /// Маркирует участок как "Эксплуатация"
        /// </summary>
        /// <remark>
        /// Логика: в ВРИ пристуствет слово "эксплуатация", на участке есть строение бти с жилым
        /// кодом, а в полученных из ВРИ кодах жилые индексы отстуствуют
        /// </remark>
        /// TODO: логика для "обслуживание части жилого дома"
        private bool _maintenance()
        {
            //var pattern = @"\bэксплуатац\w*\s*(пристройк\w*|((в|при)строен\w*\s*)?помещен\w*|част\w*\s*здан\w*|нежилы\w*|" +
            //    @"(в\s*здан\w*)?служебн\w*\s*помещен\w*)\b";
            var pattern = @"\bэксплуатац\w*\b";
            bool IsMaintenanceInString = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
            bool IsCodesNotHousing = !IsHousingCodes(Codes);
            bool IsBTICodesHousing = IsHousingCodes(bti.Codes);
            return IsMaintenanceInString && IsCodesNotHousing && IsBTICodesHousing;
        }

        /// <summary>
        /// Обрабатывает коды 12.3.0
        /// </summary>
        /// TODO: refactoring this
        private void FixCode_Other()
        {
            bool IsOtherExist = Codes.Exists("12.3");

            if (IsOtherExist && Codes.Count > 1)
            {
                Codes.RemoveAll("12.3");
            }
            else if (IsOtherExist && Codes.Count == 1 && bti.Codes.Count > 0)
            {
                Codes.RemoveAll("12.3");
                Codes.AddNodes(bti.Codes.Nodes);
            }
        }

        /// <summary>
        /// Маркирует участок как "Благоустройство"
        /// </summary>
        /// <remark>
        /// Если участок имеет в ВРИ слово благоустройство (удовлетворяющее паттерну регулярки)
        /// и имеет единственный код ПЗЗ 12.0.1, то он маркируется как благоустройство.
        /// </remark>
        private bool _landscaping()
        {
            var pattern = @"\bблагоустр\w*\b";
            bool IsLandScaping = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
            bool CodesIsLandScapingOnly = Codes.Exists("12.0.2") && Codes.Count == 1;

            if (IsLandScaping && CodesIsLandScapingOnly)
                return true;
            else

                return false;
        }

        /// <summary>
        /// Меняет код ПЗЗ с 6.7.0 на 3.1.1 в случае, если площадь участка меньше 300
        /// </summary>
        private void ElectricityStationsWithAreaLessThan300()
        {
            bool IsElectricityStation = Codes.Exists("6.7");

            if (IsElectricityStation && area < 300)
            {
                Codes.RemoveAll("6.7");
                Codes.AddNodes("3.1.1");
            }
        }

        /// <summary>
        /// Удаляет коды с видом 3004 в случае, если есть жилые коды
        /// </summary>
        private void Type230Fix()
        {
            var isKindCode3004Exist = Codes.Exists("2.7, 2.7.1, 3.1.1, 4.9, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");

            if (IsHousingCodes() && isKindCode3004Exist)
            {
                Codes.RemoveAll("2.7, 2.7.1, 3.1.1, 4.9, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");
            }
        }

        /// <summary>
        /// Удаляет коды с видом 3004 в случае, если если есть тип 100
        /// </summary>
        private void Type130Fix()
        {
            var isKindCode3004Exist = Codes.Exists("2.7.1, 3.1.1, 4.9, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");
            var isType100 = Codes.Nodes.Exists(p => p.Type.Equals("100") && !p.Code.Equals("3.1.2"));

            if (isType100 && isKindCode3004Exist)
            {
                Codes.RemoveAll("2.7.1, 3.1.1, 4.9, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");
            }
        }

        /// <summary>
        /// Удаляет код 12.0.1, если присутствет слово "благоустройство"
        /// и любой другой индекс
        /// </summary>
        private void LandscapingFix()
        {
            var isCodesNeedToDelete = Codes.Exists("12.0.2") &&
                Regex.IsMatch(input, @"\bблагоустр\w*\b", RegexOptions.IgnoreCase);

            if (isCodesNeedToDelete && Codes.Count > 1)
                Codes.RemoveAll("12.0.2");
        }

        /// <summary>
        /// Удаляет код 5.0.1 в случае, если есть коды жилья и в строке
        /// ВРИ есть слово "рекреация"
        /// </summary>
        private void HousingAndRecreationFix()
        {
            var pattern = @"\bрекреац\w*\b|\bгородск\w*\s*лес\w*\b";
            bool isRecreationInText = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
            if (IsHousingCodes() && isRecreationInText && Codes.Exists("5.0"))
            {
                Codes.RemoveAll("5.0");
            }
        }

        /// <summary>
        /// Изменяет код с 7.5.0 на 3.1.1, если площадь объекта меньше 300 кв. м.
        /// </summary>
        private void GasPipelineFix()
        {
            bool isPipeLine = Codes.Exists("7.5");

            if (isPipeLine && area < 300)
            {
                Codes.RemoveAll("7.5");
                if (!Codes.Exists("3.1.1")) Codes.AddNodes("3.1.1");
            }
            Codes.Sort();
        }

        /// <summary>
        /// Удаляет код 9.0.0, если присутствет словосочетание "особо охраняемая природная территория"
        /// и любой другой индекс
        /// </summary>
        private void SpeciallyProtectedAreaFix()
        {
            var isCodesNeedToDelete = Codes.Exists("9.0") &&
                Regex.IsMatch(input, @"\bособ\w*\s*охран\w*\s*(природ\w*\s*)?терр\w*\b", RegexOptions.IgnoreCase);

            var isBuildingsExist = Codes.Nodes.Exists(p => p.Type.Equals("100") || p.Type.Equals("200") || p.Type.Equals("300"));

            if (isCodesNeedToDelete && Codes.Count > 1)
                Codes.RemoveAll("9.0");

            if (Codes.Exists("9.1, 9.3") && Codes.Count > 1)
            {
                Codes.RemoveAll("9.1");
                Codes.RemoveAll("9.3");
            }
        }

        /// <summary>
        /// Проверяет коллекцию кодов на наличие жилых кодов
        /// </summary>
        /// <param name="Codes"></param>
        /// <returns></returns>
        private bool IsHousingCodes(ICodes _codes)
        {
            return _codes.Exists("2.0, 2.1, 2.2, 2.3, 2.1.1, 2.5, 2.6");
        }

        /// <summary>
        /// Проверяет коллекцию Codes на наличие жилых кодов
        /// </summary>
        /// <returns></returns>
        private bool IsHousingCodes()
        {
            return Codes.Exists("2.0, 2.1, 2.2, 2.3, 2.1.1, 2.5, 2.6");
        }

        private void SomeCodesCut(string codes, string types)
        {
            bool isCodesExist = Codes.Exists(codes);
            bool isTypesExist = Codes.ExistsType(types);

            if (isCodesExist && isTypesExist)
            {
                CutterFix(codes);
            }
        }

        
        private void SomeCodesCut(string codes, string types, Func<bool> foo )
        {
            bool isCodesExist = Codes.Exists(codes);
            bool isTypesExist = Codes.ExistsType(types);

            if (isCodesExist && isTypesExist && foo())
            {
                CutterFix(codes);
            }
        }

        private void SomeCodesFix()
        {
            SomeCodesCut("7.1, 7.1.1, 7.2, 7.2.1", "100, 200, 300", () => { return !Codes.ExistsKind("3005"); });
            SomeCodesCut("13.1", "200");
            SomeCodesCut("12.0.1, 12.0.2", "100, 200, 300, 800");
            SomeCodesCut("9.3", "100, 200, 300");
        }

        #region FederalCodesBehavior
        private void FederalBehavior()
        {
            //FederalToFewPZZCodesFix();
            CommunalFix();
        }

        /// <summary>
        /// Осуществляет выбор конкретного кода ПЗЗ в федеральных кодах
        /// </summary>
        //private void FederalToFewPZZCodesFix()
        //{
        //    var map = new CodesMapping().Map.Where(p => p.Value.Count > 1)
        //        .Select(p => p.Value).Where(p => p.Intersect(Codes.Nodes.Select(v => v.vri)).Count() > 0);

        //    if (map.Count() > 0)
        //    {
        //        var list = map.ElementAt(0);

        //        bool bl = Codes.Exists(bti.Codes.Show) &&
        //            bti.Codes.Exists(list);

        //        if (bl)
        //        {
        //            Codes.Nodes.RemoveAll(p => !bti.Codes.Exists(p.vri) && list.Contains(p.vri));
        //            uncut = false;
        //        }
        //    }       
        //}

        private void CommunalFix()
        {
            if (node.Exists(p => Equals(p, "3.1")) && uncut)
                CutterFix("3.1.2, 3.1.3");
        }

        private void CutterFix(string except)
        {
            Cutter?.Invoke(except);
        }
        #endregion

        #region
        public void IsFederal(bool state, string msg)
        {
            if (state)
            {
                isFederal = state;
                node.Add(msg);
            }
        }
        #endregion

        #endregion
        public void FullProcessing()
        {
            FederalBehavior();
            RemoveBaseCodes();
            NumberDeterminant();
            FixCode_Other();
            ElectricityStationsWithAreaLessThan300();
            Type230Fix();
            Type130Fix();
            LandscapingFix();
            HousingAndRecreationFix();
            GasPipelineFix();
            SpeciallyProtectedAreaFix();
            SomeCodesFix();
        }
    }
}
