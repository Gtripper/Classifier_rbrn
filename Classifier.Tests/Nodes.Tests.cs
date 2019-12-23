using NUnit.Framework;
using System.Collections.Generic;
using Classifier.Elemnents;

namespace Classifier.Tests
{
    [TestFixture]
    class NodesTests
    {
        [Test]
        public void CodesPZZ_Lenght_Returns134()
        {
            // количество кодов ПЗЗ
            int arrayLenght = 134;

            int realLength = Nodes.CodesPZZ.Count;

            Assert.AreEqual(arrayLenght, realLength);
        }

        [Test]
        public void CodesFederal_Lenght_Returns155()
        {
            // количество кодов ПЗЗ
            int arrayLenght = 155;

            int realLength = Nodes.CodesFederal.Count;

            Assert.AreEqual(arrayLenght, realLength);
        }

        [Test]
        public void Converter_InputCorrect_ReturnsCorrectlyResult()
        {
            string pzz = "2.6.0";
            var codes = Nodes.Search(pzz, false);
            string expected = "2.6";

            string actual = Nodes.Converter(codes);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Converter_BigDoubledInput_ReturnsWithoutDoublesAndInOrder()
        {
            string pzz = "6.0.0, 6.2.0, 6.2.1.0, 6.3.0, 6.5.0, 6.6.0, 6.9.0, 6.11.0, 4.9.0, 4.8.0";
            var codes = Nodes.Search(pzz, false);
            string expected = "4.8.1, 4.8.2, 4.9, 6.2, 6.2.1, 6.3, 6.3.1, 6.4, 6.5, 6.6, 6.7, 6.8, 6.9, 6.11";

            string actual = Nodes.Converter(codes, true);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("")]
        [TestCase("2.6.0.")]
        [TestCase(null)]
        [TestCase("qwerty")]
        public void ConverterIncorrectInput_ReturnsNull(string pzzCode)
        {
            string expected = null;
            var codes = Nodes.Search(pzzCode);

            string actual = Nodes.Converter(codes);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("")]
        [TestCase("2.6.0.")]
        [TestCase(null)]
        [TestCase("qwerty")]
        public void CheckPZZ_InputNotAPZZCode_ReturnsFalse(string pzzCode)
        {
            bool expected = false;

            bool actual = Nodes.CheckPZZ(pzzCode);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CheckPZZ_InputIsACode_ReturnsTrue()
        {
            string pzzCode = "2.6.0";
            bool expected = true;

            bool actual = Nodes.CheckPZZ(pzzCode);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("")]
        [TestCase("2.6.0")]
        [TestCase("2.6.")]
        [TestCase(null)]
        [TestCase("qwerty")]
        public void CheckFederal_InputNotAFederalCodes_ReturnsFalse(string fedCode)
        {
            bool expected = false;

            bool actual = Nodes.CheckFederal(fedCode);
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CheckFederal_InputIsACode_ReturnsFalse()
        {
            string code = "2.6";
            bool expected = true;

            bool actual = Nodes.CheckFederal(code);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Search_TextContainsACode_ReturnsCode()
        {
            string text = "2.6";
            IEnumerable<string> expected = new List<string> { "2.6" };

            IEnumerable<string> actual = Nodes.Search(text);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Search_StructureTextContainsFewCodes_ReturnsCollection()
        {
            string text = "2.6, 3.1.1, 4.5, 6.0";
            IEnumerable<string> expected = new List<string> { "2.6", "3.1.1", "4.5", "6.0" };

            IEnumerable<string> actual = Nodes.Search(text);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Search_AnyDefficultPoints_ReturnsCollection()
        {
            string text = "(2.6) + some word 3.1 and next 3.3. -other part of sentence\n4.2 не указанных в коде 2.7.1.0 (№58) 14.2.0 1.4.2.0";
            IEnumerable<string> expected = new List<string> { "2.6", "3.1", "3.3", "4.2" };

            IEnumerable<string> actual = Nodes.Search(text);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Search_NoCodesInputText_ReturnsNull()
        {
            string text = "this text don't have a codes"; 
            IEnumerable<string> expected = null;

            IEnumerable<string> actual = Nodes.Search(text);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Search_TestOrder_ReturnsCorrectlyOrder()
        {
            string text = "4.5, 3.1.1, 2.6, 6.0";
            IEnumerable<string> expected = new List<string> { "2.6", "3.1.1", "4.5", "6.0" };

            IEnumerable<string> actual = Nodes.Search(text);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
