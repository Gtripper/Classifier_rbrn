using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class ICodeHandlerTests
    {
        INodesCollection mf = new NodesCollection();

        public ICodeHandler Handler(string vri, string input)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vri);

            return new CodeHandler(codes, new BTI(), input, 0, mf);
        }

        public ICodeHandler Handler(string vri, IBTI bti, string input)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vri);

            return new CodeHandler(codes, bti, input, 0, mf);
        }

        public string exceptedCodes(string vri)
        {
            ICodes excepted = new Codes(mf);
            excepted.AddNodes(vri);

            return excepted.Show;
        }

        [TestCase("4.3, 6.2", "4.3, 6.2")]
        [TestCase("6.2, 6.0", "6.2")]
        [TestCase("3.5, 3.5.2", "3.5.2")]
        public void RemoveBaseCodes_Intersect_correctWork(string Codes, string _excepted)
        {
            var processing = Handler(Codes, "");
            var excepted = exceptedCodes(_excepted);

            processing.FullProcessing();

            Assert.AreEqual(excepted, processing.Codes.Show);
        }


        [TestCase("2.1.1, 2.5, 2.6", "2.6")]
        [TestCase("2.0, 2.1.1", "2.6")]
        [TestCase("2.0, 2.1.1, 2.5, 2.6, 4.4", "2.6, 4.4")]
        public void ResidentionalCodesIdentifier_BTI_HiLvlIsTrue_ReturnsCorrectResult(string Codes, string _excepted)
        {            
            IBTI bti = new BTI("2.5, 2.6, 2.7.1" , false, false, true);
            var processing = Handler(Codes, bti, "");
            var excepted = exceptedCodes(_excepted);

            processing.FullProcessing();

            Assert.AreEqual(excepted, processing.Codes.Show);
        }

        [Test]
        public void ResidentionalCodesIdentifier_BaseResidentionaCode_BTICodesContainsResidentionalCodes()
        {
            IBTI bti = new BTI("2.1, 2.2", false, false, false);
            var processing = Handler("2.0", bti, "");
            var excepted = exceptedCodes("2.1, 2.2");

            processing.FullProcessing();

            Assert.AreEqual(excepted, processing.Codes.Show);
        }

        [TestCase("эксплуатации части здания под медицинские цели", "2.1.1, 2.5, 2.6")]
        public void _Maintenance_RealZonesFromMaintenanceMap_ReturnsTrue(string input, string btiCodes)
        {
            ICodes codes = new Codes(mf);
            IBTI buiding = new BTI(btiCodes, false, false, true);
            ICodeSeeker searchResult = new CodeSeeker(input, codes, mf);
            ICodeHandler processing = new CodeHandler(searchResult.Codes, buiding, input, 0, mf);

            var result = processing.Maintenance;

            Assert.AreEqual(true, result);
        }

        [TestCase("части здания под медицинские цели", "2.1.1.0, 2.5.0, 2.6.0")]
        public void _Maintenance_InputStringIsNotMaintenance_ReturnsFalse(string input, string btiCodes)
        {
            ICodes codes = new Codes(mf);
            IBTI buiding = new BTI(btiCodes, false, false, true);
            ICodeSeeker searchResult = new CodeSeeker(input, codes, mf);
            ICodeHandler processing = new CodeHandler(searchResult.Codes, buiding, input, 0, mf);

            var result = processing.Maintenance;

            Assert.AreEqual(false, result);
        }

        [Test]
        public void FixCode_Other_SingleOtherCodeBTICodeNotNull_RetunsBTICodes()
        {
            IBTI buildings = new BTI("2.6.0", false, false, true);
            ICodes Codes = new Codes(mf);
            Codes.AddNodes("12.3.0");
            ICodeHandler processing = new CodeHandler(Codes, buildings, "", 0,  mf);
            var result = exceptedCodes("2.6.0");

            processing.FullProcessing();

            Assert.AreEqual(result, Codes.Show);
        }

        [Test]
        public void FixCode_Other_NotASingleOtherCode_RetunsCodesWithoutOther()
        {
            IBTI buildings = new BTI("2.6.0", false, false, true);
            ICodes Codes = new Codes(mf);
            Codes.AddNodes("4.9.0, 12.3.0");
            ICodeHandler processing = new CodeHandler(Codes, buildings, "", 0, mf);
            var result = exceptedCodes("4.9.0");

            processing.FullProcessing();

            Assert.AreEqual(result, Codes.Show);
        }

        [Test]
        public void FixCode_Other_SingleOtherCodeBtiCodesIsEmpty_DoNothing()
        {
            var result = exceptedCodes("12.3.0");
            var processing = Handler("12.3.0", "");

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [Test]
        public void Landscaping_SingleLandscapeCode_returnsTrue()
        {
            IBTI bti = new BTI();
            string input = "Благоустройство территории";
            ICodeSeeker searchCodes = new CodeSeeker(input, new Codes(mf), mf);
            searchCodes.Seek();
            ICodeHandler processing = new CodeHandler(searchCodes.Codes, bti, input, 0, mf);

            processing.FullProcessing();
            var result = processing.Landscaping;

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Landscaping_NotSingleLandscapeCode_returnsFalse()
        {
            IBTI bti = new BTI();
            string input = "Благоустройство территории и гараж";
            ICodeSeeker searchCodes = new CodeSeeker(input, new Codes(mf), mf);
            searchCodes.Seek();
            CodeHandler processing = new CodeHandler(searchCodes.Codes, bti, input, 0, mf);

            processing.FullProcessing();
            var result = processing.Landscaping;

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Type230Fix_3004Exist_RemoveCodes3004()
        { 
            var processing = Handler("2.0, 2.7, 3.1.1, 2.7.1, 4.9, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1", "");
            var result = exceptedCodes("2.0");

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [Test]
        public void Type230Fix_3004IsNotExist_DoNothing()
        {
            var processing = Handler("2.0.0", "");
            var result = exceptedCodes("2.0.0");

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [Test]
        public void Type230Fix_HousingCodesIsNotExist_DoNothing()
        {
            var processing = Handler("3.1.1, 2.7.1.0, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4", "");
            var result = exceptedCodes("3.1.1, 2.7.1.0, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("4.1, 12.0.2", "4.1", "благоустройство")]
        [TestCase("4.1, 3.6.1, 12.0.2", "4.1, 3.6.1", "благоустройство")]
        [TestCase("4.1, 3.6.1, 12.0.2", "4.1, 3.6.1, 12.0.2", "")]
        public void LanscapingFix_MoreThenOneIndexes_Delete12_0_1(string vri, string excepted, string input)
        {
            var processing = Handler(vri, input);
            var result = exceptedCodes(excepted);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("12.0.1", "12.0.1", "благоустройство")]
        [TestCase("3.1.1", "3.1.1", "благоустройство")]
        public void LandscapingFix_RandomvSoloIndex_DoNothing(string vri, string excepted, string input)
        {
            var processing = Handler(vri, input);
            var result = exceptedCodes(excepted);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        

        [TestCase("2.1.1.0, 5.0.1", "2.1.1.0", "рекреац")]
        [TestCase("2.1.1.0, 5.0.1", "2.1.1.0, 5.0.1", "")]
        public void HousingAndRecreationFix_DeleteRecreationIndex(string vriCodes, string excepted, string input)
        {
            var result = exceptedCodes(excepted);
            var processing = Handler(vriCodes, input);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("7.5", "3.1.1")]
        [TestCase("3.1.1, 7.5", "3.1.1")]
        [TestCase("3.3, 4.4, 7.5", "3.1.1, 3.3, 4.4")]
        public void GasPipeline_areaLessThan300_returnCommunalIndex(string vri, string excepted)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vri);
            var result = exceptedCodes(excepted);
            ICodeHandler processing = new CodeHandler(codes, new BTI(), "", 150, mf);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("7.5.0", "7.5.0")]
        [TestCase("3.1.1, 7.5.0", "3.1.1, 7.5.0")]
        [TestCase("3.3.0, 4.4.0, 7.5.0", "3.3.0, 4.4.0, 7.5.0")]
        public void GasPipeline_areaMoreThan300_returnPipeLineIndex(string vri, string excepted)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vri);
            var result = exceptedCodes(excepted);
            ICodeHandler processing = new CodeHandler(codes, new BTI(), "", 350, mf);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("9.0.0", "особо охраняемые природные территории", "9.0.0")]
        [TestCase("9.0.0", "земельные участки, занятые особо охраняемыми территориями и объектами, городскими лесами", "9.0.0")]
        public void SpeciallyProtectedAreasFix_SingleCode_DoNothing(string vri, string input, string excepted)
        {
            var processing = Handler(vri, input);
            var result = exceptedCodes(excepted);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("4.1.0, 9.0.0", "особо охраняемые природные территории", "4.1.0")]
        [TestCase("4.1.0, 9.0.0", "земельные участки, занятые особо охраняемыми территориями и объектами, городскими лесами", "4.1.0")]
        [TestCase("4.1.0, 9.0.0", "", "4.1.0, 9.0.0")]
        public void SpeciallyProtectedAreasFix_FewCodes_DoNothing(string vri, string input, string excepted)
        {
            var processing = Handler(vri, input);
            var result = exceptedCodes(excepted);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        #region Test Federal Behavior

        #region FederalType230Fix
        [Test]
        public void FederalType230Fix_Type300FromList_ReturnsType200()
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes("2.5, 4.9");
            var handler = new CodeHandler(codes, new BTI(), "", 0, mf);
            var types = new TypeAndKind(codes);
            handler.Cutter += types.CutterDelegate;
            handler.IsFederalEventHandler();

            handler.FullProcessing();

            Assert.AreEqual(200, types.Type);
        }

        [Test]
        public void FederalType230Fix_Type300FromListAndAnotherType300_ReturnsType230()
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes("2.5, 4.9, 6.4");
            var handler = new CodeHandler(codes, new BTI(), "", 0, mf);
            var types = new TypeAndKind(codes);
            handler.Cutter += types.CutterDelegate;
            handler.IsFederalEventHandler();

            handler.FullProcessing();

            Assert.AreEqual(230, types.Type);
        }

        [Test]
        public void FederalType230Fix_Type300NotFromAList_ReturnsType230()
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes("2.5, 6.4");
            var handler = new CodeHandler(codes, new BTI(), "", 0, mf);
            var types = new TypeAndKind(codes);
            handler.Cutter += types.CutterDelegate;
            handler.IsFederalEventHandler();

            handler.FullProcessing();

            Assert.AreEqual(230, types.Type);
        }
        #endregion

        [Test]
        public void FederalSpeciallyProtectedAreaFix_Codes9_0AndOtherTypes_ReturnsOtherTypes()
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes("4.5, 9.0");
            var handler = new CodeHandler(codes, new BTI(), "", 0, mf);
            var types = new TypeAndKind(codes);
            handler.Cutter += types.CutterDelegate;
            handler.IsFederalEventHandler();

            handler.FullProcessing();

            Assert.AreEqual(100, types.Type);
            Assert.AreEqual("4.5, 9.0", codes.Show);
        }

        [Test]
        public void FederalSpeciallyProtectedAreaFix_Codes9_1AndOtherTypes_ReturnsOtherTypes()
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes("4.5, 9.1");
            var handler = new CodeHandler(codes, new BTI(), "", 0, mf);
            var types = new TypeAndKind(codes);
            handler.Cutter += types.CutterDelegate;
            handler.IsFederalEventHandler();

            handler.FullProcessing();

            Assert.AreEqual(100, types.Type);
            Assert.AreEqual("4.5, 9.1", codes.Show);
        }

        public void FederalSpeciallyProtectedAreaFix_Codes9_1And9_3AndOtherTypes_ReturnsOtherTypes()
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes("4.5, 9.1, 9.3");
            var handler = new CodeHandler(codes, new BTI(), "", 0, mf);
            var types = new TypeAndKind(codes);
            handler.Cutter += types.CutterDelegate;
            handler.IsFederalEventHandler();

            handler.FullProcessing();

            Assert.AreEqual(100, types.Type);
            Assert.AreEqual("4.5, 9.1, 9.3", codes.Show);
        }

        #endregion
    }
}
