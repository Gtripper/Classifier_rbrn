﻿using Classifier.Nodes;
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
        [TestCase("участки размещения учебно-восспитательных объектов: объекты размещения учреждений кружковой деятельности и учреждений для организации", 1000)]
        public void IFactory_Kind_24(string vriDoc, int expectedKind)
        {
            int actual = IFactoryKindHelper(vriDoc);

            Assert.AreEqual(expectedKind, actual);
        }





        #endregion
    }
}
