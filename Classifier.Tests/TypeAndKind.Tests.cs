using Classifier.Nodes;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class TypeAndKindTests
    {
        INodesCollection mf = new NodesCollection();

        public ITypeAndKind Maker(string vri)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vri);
            ITypeAndKind type = new TypeAndKind(codes);
            return type;
        }

        [TestCase("2.6, 4.4", 120)]
        [TestCase("3.2.1, 3.2.2, 3.2.3", 100)]
        [TestCase("2.6, 3.1.1", 230)]
        [TestCase("2.6, 9.3", 999)]
        [TestCase("7.4, 6.9", 300)]
        [TestCase("", 777)]
        public void ITypeAndKind_CheckCorrectType(string vri, int type)
        {
            var codes = Maker(vri);

            var res = codes.Type;

            Assert.AreEqual(type, res);
        }


        [TestCase("3.2.1, 3.2.2, 3.2.3", 1000)]
        [TestCase("3.6.1, 5.1.3", 1000)]
        [TestCase("3.6.1, 7.1.1", 999)]
        [TestCase("", 777)]
        public void ITypeAndKind_CheckCorrectKind(string vri, int kind)
        {
            var codes = Maker(vri);

            var res = codes.Kind;

            Assert.AreEqual(kind, res);
        }
    }
}
