using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifier.Nodes;


namespace Classifier
{
    internal sealed class CodeComparer : IComparer<INode>
    {
        int IComparer<INode>.Compare(INode x, INode y)
        {
            var list = new NodesCollection();
            var mf = list.Nodes;
            var intA = mf.FindIndex(p => p.Equals(x));
            var intB = mf.FindIndex(p => p.Equals(y));

            if (intA > intB) return 1;
            if (intA < intB) return -1;
            else return 0;

            throw new NotImplementedException();
        }
    }
}
