using System.Linq;

namespace Classifier
{
    public interface IInputData
    {
        string Vri_doc { get; }
        int Area { get; }

        string BtiVri { get; }
        bool Lo_lvl { get; }
        bool Mid_lvl { get; }
        bool Hi_lvl { get; }

        string VRI_KLASSI { get; }
    }


    public class InputData : IInputData
    {
        public string Vri_doc { get; }
        public int Area { get; }

        public string BtiVri { get; }
        public bool Lo_lvl { get; }
        public bool Mid_lvl { get; }
        public bool Hi_lvl { get; }

        public string VRI_KLASSI { get; }

        public InputData(string _vri_doc, int _area)
        {
            Vri_doc = _vri_doc;
            Area = _area;
            BtiVri = "";
            Lo_lvl = false;
            Mid_lvl = false;
            Hi_lvl = false;
            VRI_KLASSI = "";
        }

        public InputData(string _vri_doc, int _area, string _btiVri, bool _lo, bool _mid, bool _hi)
        {
            Vri_doc = _vri_doc;
            Area = _area;
            BtiVri = _btiVri;
            Lo_lvl = _lo;
            Mid_lvl = _mid;
            Hi_lvl = _hi;
            VRI_KLASSI = "";
        }

        public InputData(string _vri_doc, int _area, string _btiVri, bool _lo, bool _mid, bool _hi, string _vri_kl)
        {
            Vri_doc = _vri_doc;
            Area = _area;
            BtiVri = _btiVri;
            Lo_lvl = _lo;
            Mid_lvl = _mid;
            Hi_lvl = _hi;
            VRI_KLASSI = _vri_kl;
        }
    }

    public class InputDataDB : IInputData
    {
        public string Vri_doc { get; private set; }
        public int Area { get; private set; }

        public string BtiVri { get; private set; }
        public bool Lo_lvl { get; private set; }
        public bool Mid_lvl { get; private set; }
        public bool Hi_lvl { get; private set; }

        public string VRI_KLASSI { get; }

        public InputDataDB(DBLayer.Plot plot, int area = 0)
        {
            Vri_doc = plot.VriDoc;
            Area = area;
            SetBtiPart(plot);
            VRI_KLASSI = plot.VriClassfRr;
        }

        private void SetBtiPart(DBLayer.Plot plot)
        {
            BtiVri = "";
            Lo_lvl = false;
            Mid_lvl = false;
            Hi_lvl = false;

            if (plot.Buildings.Count > 0)
            {
                var vriCodes = plot.Buildings.Select(p => p.VRI).Distinct();
                foreach (var vri in vriCodes)
                {
                    BtiVri += string.IsNullOrEmpty(BtiVri) ? vri : ", " + vri;
                }

                var houses = plot.Buildings.Where(p => p.BuildingClass.Equals("многоквартирный дом"));
                if (houses.Count() > 0)
                {
                    var nStoreys = houses.Select(p =>
                        (p.NumberOfStoreys == 0 || p.NumberOfStoreys == null) ? p.MaxStoreysOKS ?? 0 : p.NumberOfStoreys);
                    foreach (var s in nStoreys)
                    {
                        if (s > 0 && s <= 4)
                            Lo_lvl = true;
                        if (s > 4 && s <= 8)
                            Mid_lvl = true;
                        if (s > 8)
                            Hi_lvl = true;
                    }
                }
            }
        }
    }
}
