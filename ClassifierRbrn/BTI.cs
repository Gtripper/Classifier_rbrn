using Classifier.Nodes;

namespace Classifier
{
    public interface IBTI
    {
        ICodes Codes { get; }
        bool Lo_lvl { get; }
        bool Mid_lvl { get; }
        bool Hi_lvl { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    class BTI : IBTI
    {

        public INodesCollection mf = new NodesCollection();

        public ICodes Codes { get; }
        public bool Lo_lvl { get; }
        public bool Mid_lvl { get; }
        public bool Hi_lvl { get; }

        public BTI()
        {
            Codes = new Codes(mf);
            Lo_lvl = false;
            Mid_lvl = false;
            Hi_lvl = false;
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
            Lo_lvl = _lo;
            Mid_lvl = _mid;
            Hi_lvl = _hi;
        }
    }
}
