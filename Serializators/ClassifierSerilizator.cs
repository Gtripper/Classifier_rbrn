using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serializators
{
    [Serializable]
    public class ClassifierResults : ISerializable
    {
        public int ID { get; private set; }
        public string CadNum { get; private set; }
        public string VRI_List { get; private set; }
        public string Matches { get; private set; }

        public bool IsMainSearch { get; private set; }
        public bool IsPZZSearch { get; private set; }
        public bool IsFederalSearch { get; private set; }

        public bool IsLandscape { get; private set; }
        public bool IsMaintenance { get; private set; }

        public int Type { get; private set; }
        public int Kind { get; private set; }

        public ClassifierResults() { }

        public static ClassifierResults Create(int id, string cadNum
            , string vri_list, string matches, bool isMainSearch
            , bool isPZZSearch, bool isFederalSearch, bool isLandScape
            , bool isMaintenance, int type, int kind)
        {
            ClassifierResults results = new ClassifierResults();
            results.ID = id;
            results.CadNum = cadNum;
            results.VRI_List = vri_list;
            results.Matches = matches;
            results.IsMainSearch = isMainSearch;
            results.IsPZZSearch = isPZZSearch;
            results.IsFederalSearch = isFederalSearch;
            results.IsLandscape = isLandScape;
            results.IsMaintenance = isMaintenance;
            results.Type = type;
            results.Kind = kind;
            return results;
        }

        public ClassifierResults(SerializationInfo info, StreamingContext context)
        {
            this.ID = info.GetInt32("ID");
            this.CadNum = info.GetString("CadNum");
            this.VRI_List = info.GetString("VRI_List");
            this.Matches = info.GetString("Matches");
            this.IsMainSearch = info.GetBoolean("IsMainSearch");
            this.IsPZZSearch = info.GetBoolean("IsPZZSearch");
            this.IsFederalSearch = info.GetBoolean("IsFederalSearch");
            this.IsLandscape = info.GetBoolean("IsLandscape");
            this.IsMaintenance = info.GetBoolean("IsMaintenance");
            this.Type = info.GetInt32("Type");
            this.Kind = info.GetInt32("Kind");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", this.ID);
            info.AddValue("CadNum", this.CadNum);
            info.AddValue("VRI_List", this.VRI_List);
            info.AddValue("Matches", this.Matches);
            info.AddValue("IsMainSearch", this.IsMainSearch);
            info.AddValue("IsPZZSearch", this.IsPZZSearch);
            info.AddValue("IsFederalSearch", this.IsFederalSearch);
            info.AddValue("IsLandscape", this.IsLandscape);
            info.AddValue("IsMaintenance", this.IsMaintenance);
            info.AddValue("Type", this.Type);
            info.AddValue("Kind", this.Kind);
        }
    }
}
