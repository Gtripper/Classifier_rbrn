using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class NodeRegExpTests
    {
        [Test]
        public void Search_CorrectInput_ReturnsMatch()
        {
            string[] regexp = new string[] {
                    @"",
                    @"\b(радио\s*)?метеороло\w*(\s*центр\w*)?\b|\bметеостанц\w*\b",

                    @"",
                    @"\bэкологич\w*\s*пост\w*\b",

                    @"",
                    @"\bконтрольно-измерит\w*\s*пункт\w*\b|\bконтрольн\w*\s*пункт\w*\s*№\d*",

                    @"",
                    @"\b(монитор\w*\s*качест\w*|контро\w*\s*загрязне\w*|анализ\w*\s*проб\w*)\s*" +
                    @"(атмосферн\w*\s*)?воздух\w*\b"};
            NodeRegExp regExp = new NodeRegExp(regexp);

            var result = regExp.GetSearchResult("экологический пост");

            Assert.AreEqual("экологический пост", result);
        }
    }
}
