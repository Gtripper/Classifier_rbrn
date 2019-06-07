using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Classifier.Nodes;

namespace Classifier.Tests
{
    [TestFixture]
    class CodesTests
    {
        INodesCollection mf = new NodesCollection();

        [TestCase("3.1.1, 2.6", "2.6, 3.1.1")]
        [TestCase("3.1.1, 4.6", "3.1.1, 4.6")]
        public void Sort_unSortedList_toSortedResult(string unsortedCodes, string sortedCodes)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(unsortedCodes);

            codes.Sort();

            Assert.AreEqual(codes.Show, sortedCodes);
        }

        [Test]
        public void Sort_CodesIsEmpty_CorrectWorks()
        {
            ICodes codes = new Codes(mf);

            codes.Sort();

            Assert.AreEqual(codes.Show, "");
        }

        [TestCase("2.5, 2.6, 3.1.1, 2.7", "3.1.1, 2.7", "2.5, 2.6")]
        [TestCase("", "3.1.1, 2.7", "")]
        [TestCase("2.5, 2.6, 3.1.1, 2.7", "qwerty", "2.5, 2.6, 2.7, 3.1.1")]
        public void RevmoveAll_StringInput_CorrectRemove(string codesVri, string codesRem, string expected)
        {            
            ICodes codes = new Codes(mf);
            codes.AddNodes(codesVri);

            codes.RemoveAll(codesRem);

            Assert.AreEqual(expected, codes.Show);
        }

        [TestCase("2.5, 2.6, 3.1.1", "200")]
        [TestCase("2.5, 2.6, 3.1.1", "100, 200")]
        [TestCase("2.5, 2.6, 3.1.1", "100, 200, 300")]
        public void ExistsType_StringInput_CorrectResult(string input, string types)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(input);
            var result = codes;

            var res = result.ExistsType(types);

            Assert.AreEqual(res, true);
        }

        [TestCase("2.5, 2.6, 3.1.1", "2.5, 2.6", "3.1.1")]
        public void Except_NotEmptyArgument_correctResult(string vri, string result, string except)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vri);
            var q = codes;            

            var res = q.Except(except);

            Assert.AreEqual(result, res);
        }
    }
}
