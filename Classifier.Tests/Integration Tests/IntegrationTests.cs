using Classifier.Nodes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Classifier.Tests
{
    [TestFixture]
    class IntegrationTests
    {
        #region Helpers
        INodesCollection mf = new NodesCollection();

        private string ICodeSeekerHelper(string vriDoc)
        {
            ICodeSeeker Sample = new CodeSeeker(vriDoc, new Codes(mf), mf);

            Sample.Seek();
            return Sample.Codes.Show;
        }

        private bool ICodeSeekerFederalSearchHelper(string vriDoc)
        {
            ICodeSeeker Sample = new CodeSeeker(vriDoc, new Codes(mf), mf);

            Sample.Seek();

            return Sample.IsFederalSearch;
        }

        private string IFactoryVRYHelper(string vriDoc, int area = 350)
        {
            IInputData data = new InputData(vriDoc, area, "", false, false, false);
            IFactory factory = new Factory(data);

            factory.Execute();
            return factory.outputData.VRI_List;
        }

        private int IFactoryTypeHelper(string vriDoc, int area = 350)
        {
            IInputData data = new InputData(vriDoc, area, "", false, false, false);
            IFactory factory = new Factory(data);

            factory.Execute();
            return factory.outputData.Type;
        }

        private int IFactoryKindHelper(string vriDoc, int area = 350)
        {
            IInputData data = new InputData(vriDoc, area, "", false, false, false);
            IFactory factory = new Factory(data);

            factory.Execute();
            return factory.outputData.Kind;
        }
        #endregion


        #region Type 100
        [TestCase("Под входную группу", "2.7")]
        public void ICodeSeeker_VRI_1(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Под входную группу", "2.7")]
        public void IFactory_VRI_1(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Под входную группу", 100)]
        public void IFactory_Type_1(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Под входную группу", 1000)]
        public void IFactory_Kind_1(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase("Для обслуживания дома", "2.7")]
        public void ICodeSeeker_VRI_2(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для обслуживания дома", "2.7")]
        public void IFactory_VRI_2(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для обслуживания дома", 100)]
        public void IFactory_Type_2(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Для обслуживания дома", 1000)]
        public void IFactory_Kind_2(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase("Для устройства дополнительного входа в нежилые помещения", "2.7")]
        public void ICodeSeeker_VRI_3(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для устройства дополнительного входа в нежилые помещения", "2.7")]
        public void IFactory_VRI_3(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для устройства дополнительного входа в нежилые помещения", 100)]
        public void IFactory_Type_3(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Для устройства дополнительного входа в нежилые помещения", 1000)]
        public void IFactory_Kind_3(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase("Для обслуживания нежилого дома", "2.7")]
        public void ICodeSeeker_VRI_4(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для обслуживания нежилого дома", "2.7")]
        public void IFactory_VRI_4(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для обслуживания нежилого дома", 100)]
        public void IFactory_Type_4(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Для обслуживания нежилого дома", 1000)]
        public void IFactory_Kind_4(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }





        [TestCase("эксплуатация зоны общественного центра", "3.0, 4.0")]
        public void ICodeSeeker_VRI_5(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатация зоны общественного центра", "3.0, 4.0")]
        public void IFactory_VRI_5(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатация зоны общественного центра", 100)]
        public void IFactory_Type_5(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("эксплуатация зоны общественного центра", 1000)]
        public void IFactory_Kind_5(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase("Для реконструкции объектов нежилого фонда", "3.0, 4.0")]
        public void ICodeSeeker_VRI_6(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для реконструкции объектов нежилого фонда", "3.0, 4.0")]
        public void IFactory_VRI_6(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для реконструкции объектов нежилого фонда", 100)]
        public void IFactory_Type_6(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Для реконструкции объектов нежилого фонда", 1000)]
        public void IFactory_Kind_6(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase("(3.1) , (4.2)", "3.1, 4.2")]
        public void ICodeSeeker_VRI_7(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("(3.1) , (4.2)", true)]
        public void ICodeSeeker_FederalSearch_7(string vriDoc, bool expectedCondition)
        {
            bool actual = ICodeSeekerFederalSearchHelper(vriDoc);

            Assert.AreEqual(expectedCondition, actual);
        }
        [TestCase("(3.1) , (4.2)", "3.1, 4.2")]
        public void IFactory_VRI_7(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("(3.1) , (4.2)", 100)]
        public void IFactory_Type_7(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("(3.1) , (4.2)", 1004)]
        public void IFactory_Kind_7(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЙ ИНТЕРНАТА И ПРИЛЕГАЮЩЕЙ ТЕРРИТОРИИ", "3.2.1")]
        public void ICodeSeeker_VRI_8(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЙ ИНТЕРНАТА И ПРИЛЕГАЮЩЕЙ ТЕРРИТОРИИ", "3.2.1")]
        public void IFactory_VRI_8(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЙ ИНТЕРНАТА И ПРИЛЕГАЮЩЕЙ ТЕРРИТОРИИ", 100)]
        public void IFactory_Type_8(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЙ ИНТЕРНАТА И ПРИЛЕГАЮЩЕЙ ТЕРРИТОРИИ", 1007)]
        public void IFactory_Kind_8(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase("реконструкции здания на размещение наркодиспансера № 9", "3.2.1")]
        public void ICodeSeeker_VRI_9(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("реконструкции здания на размещение наркодиспансера № 9", "3.2.1")]
        public void IFactory_VRI_9(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("реконструкции здания на размещение наркодиспансера № 9", 100)]
        public void IFactory_Type_9(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("реконструкции здания на размещение наркодиспансера № 9", 1007)]
        public void IFactory_Kind_9(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase("объекты размещения некоммерческих организаций, связанные с обслуживанием проживающего населения (1.2.17); объекты размещения финансово-кре", "3.2.2")]
        public void ICodeSeeker_VRI_10(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("объекты размещения некоммерческих организаций, связанные с обслуживанием проживающего населения (1.2.17); объекты размещения финансово-кре", "3.2.2")]
        public void IFactory_VRI_10(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("объекты размещения некоммерческих организаций, связанные с обслуживанием проживающего населения (1.2.17); объекты размещения финансово-кре", 100)]
        public void IFactory_Type_10(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("объекты размещения некоммерческих организаций, связанные с обслуживанием проживающего населения (1.2.17); объекты размещения финансово-кре", 1001)]
        public void IFactory_Kind_10(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase("эксплуатации здания центра для несовершеннолетних", "3.2.2")]
        public void ICodeSeeker_VRI_11(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатации здания центра для несовершеннолетних", "3.2.2")]
        public void IFactory_VRI_11(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатации здания центра для несовершеннолетних", 100)]
        public void IFactory_Type_11(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("эксплуатации здания центра для несовершеннолетних", 1001)]
        public void IFactory_Kind_11(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase(@"объекты размещения предприятий по стирке, чистке, крашению, иной обработке бытовых изделий из ткани, кожи, меха и других материалов (1.2.5); об", "3.3")]
        public void ICodeSeeker_VRI_12(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"объекты размещения предприятий по стирке, чистке, крашению, иной обработке бытовых изделий из ткани, кожи, меха и других материалов (1.2.5); об", "3.3")]
        public void IFactory_VRI_12(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("объекты размещения предприятий по стирке, чистке, крашению, иной обработке бытовых изделий из ткани, кожи, меха и других материалов (1.2.5); об", 100)]
        public void IFactory_Type_12(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("объекты размещения предприятий по стирке, чистке, крашению, иной обработке бытовых изделий из ткани, кожи, меха и других материалов (1.2.5); об", 1004)]
        public void IFactory_Kind_12(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"эксплуатация здания, используемого для удовлетворения социально-бытовых нужд студентов и аспирантов МИФИ (земельные участки, предназнач", "3.3")]
        public void ICodeSeeker_VRI_13(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатация здания, используемого для удовлетворения социально-бытовых нужд студентов и аспирантов МИФИ (земельные участки, предназнач", "3.3")]
        public void IFactory_VRI_13(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатация здания, используемого для удовлетворения социально-бытовых нужд студентов и аспирантов МИФИ (земельные участки, предназнач", 100)]
        public void IFactory_Type_13(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("эксплуатация здания, используемого для удовлетворения социально-бытовых нужд студентов и аспирантов МИФИ (земельные участки, предназнач", 1004)]
        public void IFactory_Kind_13(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase(@"Для оздоровительной деятельности", "3.4")]
        public void ICodeSeeker_VRI_14(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"Для оздоровительной деятельности", "3.4")]
        public void IFactory_VRI_14(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для оздоровительной деятельности", 100)]
        public void IFactory_Type_14(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Для оздоровительной деятельности", 1005)]
        public void IFactory_Kind_14(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЯ ДЕТСКОЙ ГОРОДСКОЙ ПОЛИКЛИНИКИ № 58", "3.4.1")]
        public void ICodeSeeker_VRI_15(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЯ ДЕТСКОЙ ГОРОДСКОЙ ПОЛИКЛИНИКИ № 58", "3.4.1")]
        public void IFactory_VRI_15(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЯ ДЕТСКОЙ ГОРОДСКОЙ ПОЛИКЛИНИКИ № 58", 100)]
        public void IFactory_Type_15(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЯ ДЕТСКОЙ ГОРОДСКОЙ ПОЛИКЛИНИКИ № 58", 1005)]
        public void IFactory_Kind_15(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase("Медпункт", "3.4.1")]
        public void ICodeSeeker_VRI_16(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Медпункт", "3.4.1")]
        public void IFactory_VRI_16(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Медпункт", 100)]
        public void IFactory_Type_16(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Медпункт", 1005)]
        public void IFactory_Kind_16(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase("Под наркологический кабинет", "3.4.1")]
        public void ICodeSeeker_VRI_17(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Под наркологический кабинет", "3.4.1")]
        public void IFactory_VRI_17(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Под наркологический кабинет", 100)]
        public void IFactory_Type_17(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Под наркологический кабинет", 1005)]
        public void IFactory_Kind_17(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase("Для размещения терапевтического корпуса", "3.4.1")]
        public void ICodeSeeker_VRI_18(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для размещения терапевтического корпуса", "3.4.1")]
        public void IFactory_VRI_18(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для размещения терапевтического корпуса", 100)]
        public void IFactory_Type_18(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Для размещения терапевтического корпуса", 1005)]
        public void IFactory_Kind_18(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase("использование территории и эксплуатация зданий детской городской больницы", "3.4.2")]
        public void ICodeSeeker_VRI_19(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("использование территории и эксплуатация зданий детской городской больницы", "3.4.2")]
        public void IFactory_VRI_19(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("использование территории и эксплуатация зданий детской городской больницы", 100)]
        public void IFactory_Type_19(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("использование территории и эксплуатация зданий детской городской больницы", 1005)]
        public void IFactory_Kind_19(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase("участки размещения стационарно-профилактических учреждений (в т.ч. клинических) без специальных требований к размещению (1.2.17); участки раз", "3.4.2")]
        public void ICodeSeeker_VRI_20(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("участки размещения стационарно-профилактических учреждений (в т.ч. клинических) без специальных требований к размещению (1.2.17); участки раз", "3.4.2")]
        public void IFactory_VRI_20(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("участки размещения стационарно-профилактических учреждений (в т.ч. клинических) без специальных требований к размещению (1.2.17); участки раз", 100)]
        public void IFactory_Type_20(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("участки размещения стационарно-профилактических учреждений (в т.ч. клинических) без специальных требований к размещению (1.2.17); участки раз", 1005)]
        public void IFactory_Kind_20(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase("эксплуатации здания медико-генетического центра", "3.4.2, 3.9.2")]
        public void ICodeSeeker_VRI_21(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатации здания медико-генетического центра", "3.4.2, 3.9.2")]
        public void IFactory_VRI_21(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатации здания медико-генетического центра", 100)]
        public void IFactory_Type_21(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("эксплуатации здания медико-генетического центра", 1000)]
        public void IFactory_Kind_21(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }



        [TestCase(@"эксплуатации здания и территории под учебную деятельность", "3.5")]
        public void ICodeSeeker_VRI_22(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатации здания и территории под учебную деятельность", "3.5")]
        public void IFactory_VRI_22(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатации здания и территории под учебную деятельность", 100)]
        public void IFactory_Type_22(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("эксплуатации здания и территории под учебную деятельность", 1200)]
        public void IFactory_Kind_22(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"образование и просвещение", "3.5")]
        public void ICodeSeeker_VRI_23(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"образование и просвещение", "3.5")]
        public void IFactory_VRI_23(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("образование и просвещение", 100)]
        public void IFactory_Type_23(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("образование и просвещение", 1200)]
        public void IFactory_Kind_23(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"участки размещения учебно-восспитательных объектов: объекты размещения учреждений кружковой деятельности и учреждений для организации", "3.5.1")]
        public void ICodeSeeker_VRI_24(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"участки размещения учебно-восспитательных объектов: объекты размещения учреждений кружковой деятельности и учреждений для организации", "3.5.1")]
        public void IFactory_VRI_24(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("участки размещения учебно-восспитательных объектов: объекты размещения учреждений кружковой деятельности и учреждений для организации", 100)]
        public void IFactory_Type_24(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("участки размещения учебно-восспитательных объектов: объекты размещения учреждений кружковой деятельности и учреждений для организации", 2003)]
        public void IFactory_Kind_24(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"осуществления учебно-воспитательной деятельности (прогулочная площадка)", "3.5.1, 12.0.2")]
        public void ICodeSeeker_VRI_25(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"осуществления учебно-воспитательной деятельности (прогулочная площадка)", "3.5.1, 12.0.2")]
        public void IFactory_VRI_25(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("осуществления учебно-воспитательной деятельности (прогулочная площадка)", 100)]
        public void IFactory_Type_25(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("осуществления учебно-воспитательной деятельности (прогулочная площадка)", 2003)]
        public void IFactory_Kind_25(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"Размещение объектов капитального строительства, предназначенных для профессионального образования и просвещения (профессиональные тех", "3.5, 3.5.2")]
        public void ICodeSeeker_VRI_26(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"Размещение объектов капитального строительства, предназначенных для профессионального образования и просвещения (профессиональные тех", "3.5.2")]
        public void IFactory_VRI_26(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Размещение объектов капитального строительства, предназначенных для профессионального образования и просвещения (профессиональные тех", 100)]
        public void IFactory_Type_26(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Размещение объектов капитального строительства, предназначенных для профессионального образования и просвещения (профессиональные тех", 1002)]
        public void IFactory_Kind_26(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }





        [TestCase(@"эксплуатация зданий и сооружений дома аспиранта и студента", "3.5.2")]
        public void ICodeSeeker_VRI_27(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатация зданий и сооружений дома аспиранта и студента", "3.5.2")]
        public void IFactory_VRI_27(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатация зданий и сооружений дома аспиранта и студента", 100)]
        public void IFactory_Type_27(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("эксплуатация зданий и сооружений дома аспиранта и студента", 1002)]
        public void IFactory_Kind_27(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }





        [TestCase(@"эксплуатации здания спортивного интерната ЦСКА", "3.5.1")]
        public void ICodeSeeker_VRI_28(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатации здания спортивного интерната ЦСКА", "3.5.1")]
        public void IFactory_VRI_28(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатации здания спортивного интерната ЦСКА", 100)]
        public void IFactory_Type_28(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("эксплуатации здания спортивного интерната ЦСКА", 2003)]
        public void IFactory_Kind_28(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"эксплуатации зданий кафедры оперативной хирургии.", "3.5.2")]
        public void ICodeSeeker_VRI_29(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатации зданий кафедры оперативной хирургии.", "3.5.2")]
        public void IFactory_VRI_29(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("эксплуатации зданий кафедры оперативной хирургии.", 100)]
        public void IFactory_Type_29(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("эксплуатации зданий кафедры оперативной хирургии.", 1002)]
        public void IFactory_Kind_29(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"Для размещения временной площадки для проведения занятий по контраварийной подготовке водителей", "3.5.2")]
        public void ICodeSeeker_VRI_30(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"Для размещения временной площадки для проведения занятий по контраварийной подготовке водителей", "3.5.2")]
        public void IFactory_VRI_30(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("Для размещения временной площадки для проведения занятий по контраварийной подготовке водителей", 100)]
        public void IFactory_Type_30(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase("Для размещения временной площадки для проведения занятий по контраварийной подготовке водителей", 1002)]
        public void IFactory_Kind_30(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"эксплуатации здания центра творчества", "3.6.1")]
        public void ICodeSeeker_VRI_31(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатации здания центра творчества", "3.6.1")]
        public void IFactory_VRI_31(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатации здания центра творчества", 100)]
        public void IFactory_Type_31(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"эксплуатации здания центра творчества",  1003)]
        public void IFactory_Kind_31(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"участки размещения спортивно-рекреационных объектов: объекты размещения помещений и технических устройств крытых спортивных сооружений ограниченного посещения (1.2.17); участки размещения учебно-воспитательных объектов: объекты размещения кружковой деятельности и учреждений для организации досуговой работы с населением по месту жительства, в т.ч. детского творчества (1.2.17)", "3.5.1, 3.6.1, 5.1.2")]
        public void ICodeSeeker_VRI_32(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"участки размещения спортивно-рекреационных объектов: объекты размещения помещений и технических устройств крытых спортивных сооружений ограниченного посещения (1.2.17); участки размещения учебно-воспитательных объектов: объекты размещения кружковой деятельности и учреждений для организации досуговой работы с населением по месту жительства, в т.ч. детского творчества (1.2.17)", "3.5.1, 3.6.1, 5.1.2")]
        public void IFactory_VRI_32(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"участки размещения спортивно-рекреационных объектов: объекты размещения помещений и технических устройств крытых спортивных сооружений ограниченного посещения (1.2.17); участки размещения учебно-воспитательных объектов: объекты размещения кружковой деятельности и учреждений для организации досуговой работы с населением по месту жительства, в т.ч. детского творчества (1.2.17)", 100)]
        public void IFactory_Type_32(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"участки размещения спортивно-рекреационных объектов: объекты размещения помещений и технических устройств крытых спортивных сооружений ограниченного посещения (1.2.17); участки размещения учебно-воспитательных объектов: объекты размещения кружковой деятельности и учреждений для организации досуговой работы с населением по месту жительства, в т.ч. детского творчества (1.2.17)", 1200)]
        public void IFactory_Kind_32(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"размещения и эксплуатации открытой учебно-спортивной площадки в целях удовлетворения граждан в дополнительном образовании: курсов по подготовке водителей транспортных средств категории ", "3.5, 3.5.2")]
        public void ICodeSeeker_VRI_33(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"размещения и эксплуатации открытой учебно-спортивной площадки в целях удовлетворения граждан в дополнительном образовании: курсов по подготовке водителей транспортных средств категории ", "3.5.2")]
        public void IFactory_VRI_33(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"размещения и эксплуатации открытой учебно-спортивной площадки в целях удовлетворения граждан в дополнительном образовании: курсов по подготовке водителей транспортных средств категории ", 100)]
        public void IFactory_Type_33(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"размещения и эксплуатации открытой учебно-спортивной площадки в целях удовлетворения граждан в дополнительном образовании: курсов по подготовке водителей транспортных средств категории ", 1002)]
        public void IFactory_Kind_33(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"РАЗВИТИЯ И ЭКСПЛУАТАЦИИ РЕЗИДЕНЦИИ ПАТРИАРХОВ МОСКОВСКИХ И ВСЕЯ РУСИ, ЦЕНТРА ПРАВОСЛАВНОГО НАСЛЕДИЯ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ ЭКСПЛУА", "3.7.1")]
        public void ICodeSeeker_VRI_34(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"РАЗВИТИЯ И ЭКСПЛУАТАЦИИ РЕЗИДЕНЦИИ ПАТРИАРХОВ МОСКОВСКИХ И ВСЕЯ РУСИ, ЦЕНТРА ПРАВОСЛАВНОГО НАСЛЕДИЯ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ ЭКСПЛУА", "3.7.1")]
        public void IFactory_VRI_34(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"РАЗВИТИЯ И ЭКСПЛУАТАЦИИ РЕЗИДЕНЦИИ ПАТРИАРХОВ МОСКОВСКИХ И ВСЕЯ РУСИ, ЦЕНТРА ПРАВОСЛАВНОГО НАСЛЕДИЯ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ ЭКСПЛУА", 100)]
        public void IFactory_Type_34(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"РАЗВИТИЯ И ЭКСПЛУАТАЦИИ РЕЗИДЕНЦИИ ПАТРИАРХОВ МОСКОВСКИХ И ВСЕЯ РУСИ, ЦЕНТРА ПРАВОСЛАВНОГО НАСЛЕДИЯ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ ЭКСПЛУА", 1003)]
        public void IFactory_Kind_34(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"для проведения комплекса ремонтно-реставрационных работ, приспособления ансамбля Марфо-Мариинской Обители и дальнейшей эксплуатации зд", "3.7.1")]
        public void ICodeSeeker_VRI_35(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"для проведения комплекса ремонтно-реставрационных работ, приспособления ансамбля Марфо-Мариинской Обители и дальнейшей эксплуатации зд", "3.7.1")]
        public void IFactory_VRI_35(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"для проведения комплекса ремонтно-реставрационных работ, приспособления ансамбля Марфо-Мариинской Обители и дальнейшей эксплуатации зд", 100)]
        public void IFactory_Type_35(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"для проведения комплекса ремонтно-реставрационных работ, приспособления ансамбля Марфо-Мариинской Обители и дальнейшей эксплуатации зд", 1003)]
        public void IFactory_Kind_35(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"строительства и последующей эксплуатации комплекса дипломатического представительства республики корея и других связанных с ним сооруж", "3.8.2")]
        public void ICodeSeeker_VRI_36(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"строительства и последующей эксплуатации комплекса дипломатического представительства республики корея и других связанных с ним сооруж", "3.8.2")]
        public void IFactory_VRI_36(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"строительства и последующей эксплуатации комплекса дипломатического представительства республики корея и других связанных с ним сооруж", 100)]
        public void IFactory_Type_36(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"строительства и последующей эксплуатации комплекса дипломатического представительства республики корея и других связанных с ним сооруж", 1001)]
        public void IFactory_Kind_36(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"РАЗРАБОТКИ ИСХОДНО-РАЗРЕШИТЕЛЬНОЙ И ПРОЕКТНОЙ ДОКУМЕНТАЦИИ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ  ЭКСПЛУАТАЦИИ МНОГОФУНКЦИОНАЛЬНОГО АДМИНИСТРАТ", "4.0")]
        public void ICodeSeeker_VRI_37(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"РАЗРАБОТКИ ИСХОДНО-РАЗРЕШИТЕЛЬНОЙ И ПРОЕКТНОЙ ДОКУМЕНТАЦИИ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ  ЭКСПЛУАТАЦИИ МНОГОФУНКЦИОНАЛЬНОГО АДМИНИСТРАТ", "4.0")]
        public void IFactory_VRI_37(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"РАЗРАБОТКИ ИСХОДНО-РАЗРЕШИТЕЛЬНОЙ И ПРОЕКТНОЙ ДОКУМЕНТАЦИИ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ  ЭКСПЛУАТАЦИИ МНОГОФУНКЦИОНАЛЬНОГО АДМИНИСТРАТ", 100)]
        public void IFactory_Type_37(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"РАЗРАБОТКИ ИСХОДНО-РАЗРЕШИТЕЛЬНОЙ И ПРОЕКТНОЙ ДОКУМЕНТАЦИИ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ  ЭКСПЛУАТАЦИИ МНОГОФУНКЦИОНАЛЬНОГО АДМИНИСТРАТ", 1000)]
        public void IFactory_Kind_37(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"предпринимательство", "4.0")]
        public void ICodeSeeker_VRI_38(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"предпринимательство", "4.0")]
        public void IFactory_VRI_38(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"предпринимательство", 100)]
        public void IFactory_Type_38(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"предпринимательство", 1000)]
        public void IFactory_Kind_38(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"предпринимательство", "4.0")]
        public void ICodeSeeker_VRI_39(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"предпринимательство", "4.0")]
        public void IFactory_VRI_39(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"предпринимательство", 100)]
        public void IFactory_Type_39(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"предпринимательство", 1000)]
        public void IFactory_Kind_39(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"эксплуатации имущественного комплекса дирекции", "4.1")]
        public void ICodeSeeker_VRI_40(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатации имущественного комплекса дирекции", "4.1")]
        public void IFactory_VRI_40(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"эксплуатации имущественного комплекса дирекции", 100)]
        public void IFactory_Type_40(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"эксплуатации имущественного комплекса дирекции", 1001)]
        public void IFactory_Kind_40(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"участки размещения административно-деловых объектов: объекты размещения офисных помещений (1.2.7), участки размещения жилищно-коммунальных", "3.1.1, 4.1")]
        public void ICodeSeeker_VRI_41(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"участки размещения административно-деловых объектов: объекты размещения офисных помещений (1.2.7), участки размещения жилищно-коммунальных", "4.1")]
        public void IFactory_VRI_41(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"участки размещения административно-деловых объектов: объекты размещения офисных помещений (1.2.7), участки размещения жилищно-коммунальных", 100)]
        public void IFactory_Type_41(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"участки размещения административно-деловых объектов: объекты размещения офисных помещений (1.2.7), участки размещения жилищно-коммунальных", 1001)]
        public void IFactory_Kind_41(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }





        [TestCase("проектирования, строительства и дальнейшей эксплуатации торгово-коммерческого комплекса и трансформаторной подстанции (11 обособленных", "3.1.1, 4.2")]
        public void ICodeSeeker_VRI_42(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase("проектирования, строительства и дальнейшей эксплуатации торгово-коммерческого комплекса и трансформаторной подстанции (11 обособленных", "4.2")]
        public void IFactory_VRI_42(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"проектирования, строительства и дальнейшей эксплуатации торгово-коммерческого комплекса и трансформаторной подстанции (11 обособленных", 100)]
        public void IFactory_Type_42(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"проектирования, строительства и дальнейшей эксплуатации торгово-коммерческого комплекса и трансформаторной подстанции (11 обособленных", 1004)]
        public void IFactory_Kind_42(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }




        [TestCase(@"под размещение комплекса под реализацию сельскохозяйственной продукции,продовольственных продуктов и иных необходимых товаров с обустр", "4.2")]
        public void ICodeSeeker_VRI_43(string vriDoc, string expectedVri)
        {
            string actual = ICodeSeekerHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"под размещение комплекса под реализацию сельскохозяйственной продукции,продовольственных продуктов и иных необходимых товаров с обустр", "4.2")]
        public void IFactory_VRI_43(string vriDoc, string expectedVri)
        {
            string actual = IFactoryVRYHelper(vriDoc);

            Assert.AreEqual(expectedVri, actual);
        }
        [TestCase(@"под размещение комплекса под реализацию сельскохозяйственной продукции,продовольственных продуктов и иных необходимых товаров с обустр", 100)]
        public void IFactory_Type_43(string vriDoc, int expectedType)
        {
            int actual = IFactoryTypeHelper(vriDoc);

            Assert.AreEqual(expectedType, actual);
        }
        [TestCase(@"под размещение комплекса под реализацию сельскохозяйственной продукции,продовольственных продуктов и иных необходимых товаров с обустр", 1004)]
        public void IFactory_Kind_43(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }

        #endregion
    }
}
