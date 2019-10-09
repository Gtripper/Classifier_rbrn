﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifier.Nodes;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class ICodeHandlerTests
    {
        INodesCollection mf = new NodesCollection();

        public ICodeHandler Processing(string vri, string input)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vri);

            return new CodeHandler(codes, new BTI(), input, 0, mf);
        }

        public ICodeHandler Processing(string vri, IBTI bti, string input)
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
            var processing = Processing(Codes, "");
            var excepted = exceptedCodes(_excepted);

            processing.FullProcessing();

            Assert.AreEqual(excepted, processing.Codes.Show);
        }


        [TestCase("2.1.1.0, 2.5.0, 2.6.0", "2.6.0")]
        [TestCase("2.0.0, 2.1.1.0", "2.6.0")]
        [TestCase("2.0.0, 2.1.1.0, 2.5.0, 2.6.0, 4.4.0", "2.6.0, 4.4.0")]
        public void NumberDeterminant_BTI_HiLvlIsTrue_ReturnsCorrectResult(string Codes, string _excepted)
        {            
            IBTI bti = new BTI("2.5.0, 2.6.0, 2.7.1.0" , false, false, true);
            var processing = Processing(Codes, bti, "");
            var excepted = exceptedCodes(_excepted);

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
            var processing = Processing("12.3.0", "");

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
            var processing = Processing("2.0, 2.7, 3.1.1, 2.7.1, 4.9, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1", "");
            var result = exceptedCodes("2.0");

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [Test]
        public void Type230Fix_3004IsNotExist_DoNothing()
        {
            var processing = Processing("2.0.0", "");
            var result = exceptedCodes("2.0.0");

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [Test]
        public void Type230Fix_HousingCodesIsNotExist_DoNothing()
        {
            var processing = Processing("3.1.1, 2.7.1.0, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4", "");
            var result = exceptedCodes("3.1.1, 2.7.1.0, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("4.1, 12.0.2", "4.1", "благоустройство")]
        [TestCase("4.1, 3.6.1, 12.0.2", "4.1, 3.6.1", "благоустройство")]
        [TestCase("4.1, 3.6.1, 12.0.2", "4.1, 3.6.1, 12.0.2", "")]
        public void LanscapingFix_MoreThenOneIndexes_Delete12_0_1(string vri, string excepted, string input)
        {
            var processing = Processing(vri, input);
            var result = exceptedCodes(excepted);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("12.0.1", "12.0.1", "благоустройство")]
        [TestCase("3.1.1", "3.1.1", "благоустройство")]
        public void LandscapingFix_RandomvSoloIndex_DoNothing(string vri, string excepted, string input)
        {
            var processing = Processing(vri, input);
            var result = exceptedCodes(excepted);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        //[TestCase("3.1.1, 3.1.2, 3.1.3", "3.1.1", "3.1.1")]
        //[TestCase("3.1.1, 3.1.2, 3.1.3", "3.1.1, 3.1.2", "3.1.1, 3.1.2")]
        //[TestCase("3.1.1, 3.1.2, 3.1.3", "3.1.2", "3.1.2")]
        //[TestCase("3.1.1, 3.1.2, 3.1.3", "4.1.0", "3.1.1, 3.1.2, 3.1.3")]
        //[TestCase("3.1.1, 3.1.2, 3.1.3", "", "3.1.1, 3.1.2, 3.1.3")]
        //[TestCase("3.1.1, 3.1.2, 3.1.3", "", "3.1.1, 3.1.2, 3.1.3")]
        //[TestCase("3.5.1.0, 3.5.2.0", "3.5.1.0", "3.5.1.0")]
        //[TestCase("3.5.1.0, 3.5.2.0", "", "3.5.1.0, 3.5.2.0")]
        //[TestCase("3.5.1.0, 3.5.2.0", "3.3.0", "3.5.1.0, 3.5.2.0")]
        //[TestCase("2.7.0", "", "2.7.0")]
        //[TestCase("2.7.0", "3.3.0", "2.7.0")]
        //[TestCase("7.2.1", "6.7.0, 7.1.2", "7.2.1")]
        //[Ignore("miss method")]
        public void FederalToFewPZZCodesFix_ChooseCorrectIndexAndDeleteOther(string vriCodes, string btiCodes, string excepted)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vriCodes);
            var result = exceptedCodes(excepted);
            IBTI bti = new BTI(btiCodes, false, false, false);
            ICodeHandler processing = new CodeHandler(codes, bti, "", 0, mf);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("2.1.1.0, 5.0.1", "2.1.1.0", "рекреац")]
        [TestCase("2.1.1.0, 5.0.1", "2.1.1.0, 5.0.1", "")]
        public void HousingAndRecreationFix_DeleteRecreationIndex(string vriCodes, string excepted, string input)
        {
            var result = exceptedCodes(excepted);
            var processing = Processing(vriCodes, input);

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
            var processing = Processing(vri, input);
            var result = exceptedCodes(excepted);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }

        [TestCase("4.1.0, 9.0.0", "особо охраняемые природные территории", "4.1.0")]
        [TestCase("4.1.0, 9.0.0", "земельные участки, занятые особо охраняемыми территориями и объектами, городскими лесами", "4.1.0")]
        [TestCase("4.1.0, 9.0.0", "", "4.1.0, 9.0.0")]
        public void SpeciallyProtectedAreasFix_FewCodes_DoNothing(string vri, string input, string excepted)
        {
            var processing = Processing(vri, input);
            var result = exceptedCodes(excepted);

            processing.FullProcessing();

            Assert.AreEqual(result, processing.Codes.Show);
        }
    }
}
