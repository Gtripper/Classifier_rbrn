using Classifier.Nodes;

namespace Classifier
{
    public interface IBTI
    {
        ICodes Codes { get; }
        bool LoLvl { get; }
        bool MidLvl { get; }
        bool HiLvl { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    class BTI : IBTI
    {

        public INodesCollection mf = new NodesCollection();

        public ICodes Codes { get; }
        public bool LoLvl { get; }
        public bool MidLvl { get; }
        public bool HiLvl { get; }

        public BTI()
        {
            Codes = new Codes(mf);
            LoLvl = false;
            MidLvl = false;
            HiLvl = false;
        }
        /// <summary>
        /// Конструктор объекта БТИ
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="lo"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        public BTI(string _codes, bool _lo, bool _mid, bool _hi)
        {
            Codes = new Codes(mf);
            Codes.AddNodes(_codes);
            LoLvl = _lo;
            MidLvl = _mid;
            HiLvl = _hi;
        }
    }
}
