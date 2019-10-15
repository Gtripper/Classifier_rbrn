using System.Diagnostics.Contracts;
using System.Linq;

namespace Classifier
{
    public interface IInputData
    {
        string VriDoc { get; }
        int Area { get; }

        string BtiVri { get; }
        bool LoLvl { get; }
        bool MidLvl { get; }
        bool HiLvl { get; }

        string VriKlass { get; }
    }


    public class InputData : IInputData
    {
        public string VriDoc { get; }
        public int Area { get; }

        public string BtiVri { get; }
        public bool LoLvl { get; }
        public bool MidLvl { get; }
        public bool HiLvl { get; }

        public string VriKlass { get; }

        public InputData(string vriDoc, int area)
        {
            VriDoc = vriDoc;
            Area = area;
            BtiVri = "";
            LoLvl = false;
            MidLvl = false;
            HiLvl = false;
            VriKlass = "";
        }

        public InputData(string vriDoc, int area, string btiVri, bool lo, bool mid, bool hi)
        {
            VriDoc = vriDoc;
            Area = area;
            BtiVri = btiVri;
            LoLvl = lo;
            MidLvl = mid;
            HiLvl = hi;
            VriKlass = "";
        }

        public InputData(string vriDoc, int area, string btiVri, bool lo, bool mid, bool hi, string vri_kl)
        {
            VriDoc = vriDoc;
            Area = area;
            BtiVri = btiVri;
            LoLvl = lo;
            MidLvl = mid;
            HiLvl = hi;
            VriKlass = vri_kl;
        }
    }

    public class InputDataDB : IInputData
    {
        public string VriDoc { get; private set; }
        public int Area { get; private set; }

        public string BtiVri { get; private set; }
        public bool LoLvl { get; private set; }
        public bool MidLvl { get; private set; }
        public bool HiLvl { get; private set; }

        public string VriKlass { get; }

        public InputDataDB(DBLayer.Plot plot, int area = 0)
        {
            Contract.Requires(plot != null);
            VriDoc = plot.VriDoc ?? "";
            Area = area;
            SetBtiPart(plot);
            VriKlass = plot.VriClassfRr;
        }

        private void SetBtiPart(DBLayer.Plot plot)
        {
            BtiVri = "";
            LoLvl = false;
            MidLvl = false;
            HiLvl = false;

            if (plot.Buildings != null && plot.Buildings.Any())
            {
                var vriCodes = plot.Buildings.Select(p => p.VRI).Distinct();
                foreach (var vri in vriCodes)
                {
                    var btiVri = vri ?? "";
                    BtiVri += string.IsNullOrEmpty(BtiVri) ? btiVri : ", " + btiVri;
                }

                var houses = plot.Buildings.Where(p => p.BuildingPurpose.Equals("многоквартирный дом"));
                if (houses.Any())
                {
                    var nStoreys = houses.Select(p =>
                        (p.NumberOfStoreys == 0 || p.NumberOfStoreys == null) ? p.MaxStoreysOKS ?? 0 : p.NumberOfStoreys);
                    foreach (var s in nStoreys)
                    {
                        if (s > 0 && s <= 4)
                            LoLvl = true;
                        if (s > 4 && s <= 8)
                            MidLvl = true;
                        if (s > 8)
                            HiLvl = true;
                    }
                }
            }
        }
    }
}
