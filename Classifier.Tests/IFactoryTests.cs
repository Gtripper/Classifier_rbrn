﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class IFactoryTests
    {
        #region TestCases
        #region Type 100
        [TestCase("(3.1)", 4, "3.1.2", false, false, false, "3.1", 100, 1001)]
        [TestCase("(3.1) , (4.1)", 4, "3.1.2", false, false, false, "3.1, 4.1", 100, 1001)]
        [TestCase("(3.1) , (4.1)", 4, "", false, false, false, "3.1, 4.1", 100, 1001)]
        [TestCase("эксплуатации прогулочной площадки детского сада", 1416, "", false, false, false, "3.5.1, 12.0.2", 100, 2003)]
        [TestCase(@"для учебно-производст. базы", 4, "", false, false, false, "3.5.2", 100, 1002)]
        [TestCase("объекты размещения помещений и технических устройств  крытых спортивных сооружений массового посещения (1.2.17); объекты размещения досугов", 37181, "3.1.1", false, false, false, "3.6.1, 5.1.1", 100, 1000)]
        [TestCase("(7.1) , (4.1)", 4, "", false, false, false, "4.1, 7.1", 100, 1001)]
        [TestCase("(7.2) , (4.1)", 4, "", false, false, false, "4.1, 7.2", 100, 1001)]
        [TestCase("Коммунальное обслуживание (3.1). Охрана природных территорий (9.1).", 4, "", false, false, false, "3.1", 100, 1001)]
        #endregion
        #region Type 200        
        [TestCase("земельные участки, предназначенные для размещения домов многоэтажной жилой застройки (1.2.1); земельные участки, занятые городскими лесами,", 4, "", false, false, false, "2.5, 2.6", 200, 2002)]
        [TestCase(@"для ведения садового хозяйства", 4, "", false, false, false, "13.2", 200, 2004)]
        [TestCase("Для ведения садоводства", 4, "", false, false, false, "13.2", 200, 2004)]
        #endregion
        #region Type 300
        [TestCase("земельные участки, предназначенные для размещения газопроводов (1.2.13), земельные участки, занятые особо охраняемыми территориями и объект", 4, "", false, false, false, "3.1.1", 300, 3004)]
        #endregion
        #region Type 400
        [TestCase("Объекты размещения помещений и технических устройств общественных туалетов (1.2.9)", 4, "",
            false, false, false, "12.0.2", 400, 4001)]
        #endregion
        #region Type 500
        [TestCase("автомобильный транспорт (7.2) (земельные участки, предназначенные для разработки полезных ископаемых, размещения железнодорожных путей, ав", 76, "6.7.0, 7.1.2", false, false, false, "7.2", 500, 5000)]
        #endregion
        #region Type 600
        [TestCase("Размещение железнодорожных путей (7.1.1), Размещение, зданий и сооружений, в том числе железнодорожных вокзалов и станций, а также устройств", 4, "", false, false, false, "7.1.1", 600, 6000)]
        #endregion
        #region Type 999
        [TestCase("земельные участки, предназначенные для сельскохозяйственного использования (1.2.15), занятые водными объектами (береговая полоса), ограниче", 5, "", false, false, false, "1.1, 11.0", 999, 999)]
        #endregion
        #endregion
        public void IFactory_FullDataTest(string _vri_doc, int _area, string _btiVri, bool _lo, bool _mid, bool _hi, string vri_list, int type, int kind)
        {
            IInputData data = new InputData(_vri_doc, _area, _btiVri, _lo, _mid, _hi);
            IFactory factory = new Factory(data);

            factory.Execute();

            Assert.AreEqual(vri_list, factory.outputData.VRI_List);
            Assert.AreEqual(type, factory.outputData.Type);
            Assert.AreEqual(kind, factory.outputData.Kind);
        }

        #region TestCases
        #region Type 100       
        
        
        
        
 
        [TestCase(@"участки размещения учебно-восспитательных объектов: объекты размещения учреждений кружковой деятельности и учреждений для организации", "3.5.1")]
        [TestCase("осуществления учебно-воспитательной деятельности (прогулочная площадка)", "3.5.1, 12.0.2")]
        [TestCase(@"Размещение объектов капитального строительства, предназначенных для профессионального образования и просвещения (профессиональные тех", "3.5.2")]
        [TestCase(@"эксплуатация зданий и сооружений дома аспиранта и студента", "3.5.2")]
        [TestCase(@"эксплуатации здания спортивного интерната ЦСКА", "3.5.1")]
        [TestCase(@"эксплуатации зданий кафедры оперативной хирургии.", "3.5.2")]
        [TestCase(@"Для размещения временной площадки для проведения занятий по контраварийной подготовке водителей", "3.5.2")]
        [TestCase(@"эксплуатации здания центра творчества", "3.6.1")]
        [TestCase(@"участки размещения спортивно-рекреационных объектов: объекты размещения помещений и технических устройств крытых спортивных сооружений ограниченного посещения (1.2.17); участки размещения учебно-воспитательных объектов: объекты размещения кружковой деятельности и учреждений для организации досуговой работы с населением по месту жительства, в т.ч. детского творчества (1.2.17)", "3.6.1, 5.1.2")]
        [TestCase(@"РАЗВИТИЯ И ЭКСПЛУАТАЦИИ РЕЗИДЕНЦИИ ПАТРИАРХОВ МОСКОВСКИХ И ВСЕЯ РУСИ, ЦЕНТРА ПРАВОСЛАВНОГО НАСЛЕДИЯ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ ЭКСПЛУА", "3.7.1")]
        [TestCase(@"для проведения комплекса ремонтно-реставрационных работ, приспособления ансамбля Марфо-Мариинской Обители и дальнейшей эксплуатации зд", "3.7.1")]
        [TestCase(@"строительства и последующей эксплуатации комплекса дипломатического представительства республики корея и других связанных с ним сооруж", "3.8.2")]
        [TestCase(@"РАЗРАБОТКИ ИСХОДНО-РАЗРЕШИТЕЛЬНОЙ И ПРОЕКТНОЙ ДОКУМЕНТАЦИИ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ  ЭКСПЛУАТАЦИИ МНОГОФУНКЦИОНАЛЬНОГО АДМИНИСТРАТ", "4.0")]
        [TestCase(@"предпринимательство", "4.0")]
        [TestCase(@"эксплуатации имущественного комплекса дирекции", "4.1")]
        [TestCase("участки размещения административно-деловых объектов: объекты размещения офисных помещений (1.2.7), участки размещения жилищно-коммунальных", "4.1")]
        [TestCase("проектирования, строительства и дальнейшей эксплуатации торгово-коммерческого комплекса и трансформаторной подстанции (11 обособленных", "4.2")]
        [TestCase(@"под размещение комплекса под реализацию сельскохозяйственной продукции,продовольственных продуктов и иных необходимых товаров с обустр", "4.2")]
        [TestCase(@"ЗАВЕРШЕНИЯ РАЗРАБОТКИ АКТА РАЗРЕШЕННОГО ИСПОЛЬЗОВАНИЯ, ПРОЕКТНОЙ ДОКУМЕНТАЦИИ И СТРОИТЕЛЬСТВА СОЦИАЛЬНОГО ОБЪЕКТА ШАГОВОЙ ДОСТУПНОСТИ", "4.2, 4.4")]
        [TestCase(@"Для эксплуатации временного объекта по реализации питьевой воды", "4.4")]
        [TestCase(@"Под установку продовольственного павильона", "4.4")]
        [TestCase(@"Под пункт общепита", "4.6")]
        [TestCase(@"Для размещения пункта питания", "4.6")]
        [TestCase(@"Под пивбар", "4.6")]
        [TestCase(@"Подпункт общепита", "4.6")]
        [TestCase(@"Для эксплуатации пункта питания", "4.6")]
        [TestCase(@"для проектирования и строительства объекта: ""Строительство центра единоборств, г.Уфа""", "5.1.2")]
        [TestCase(@"для проектирования и строительства крытого катка", "5.1.2")]
        [TestCase(@"занимаемый футбольным полем", "5.1.3")]
        [TestCase(@"занимаемый базой маломерных учебных судов", "5.1.5")]
        [TestCase(@"Для эксплуатации турбазы", "5.2")]
        [TestCase(@"эксплуатации зданий школы высшего спортивного мастерства", "5.1.2")]
        [TestCase(@"на период разработки исходно-разрешительной и проектной документации на размещение центра всесезонных видов спорта и проведения первооч", "5.1.3")]
        [TestCase(@"эксплуатации здания центра восстановительного лечения", "9.2.1")]
        [TestCase(@"Занимаемый объектами незавершенного строительства оздоровительной базы", "9.2.1")]
        #endregion
        #region Type 200
        [TestCase(@"размещение жилых помещений различного вида и обеспечение проживания в них Содержание данного вида разрешенного использования включает в", "2.0")]
        [TestCase(@"квартал жилого типа низкоплотной застройки с высотой не более 15 м", "2.1, 2.2")]
        [TestCase(@"Для строительства жилого дома", "2.1, 2.2")]
        [TestCase(@"Индивидуальная одноэтажная застройка", "2.1, 2.2")]
        [TestCase(@"Ведомственная одноэтажная застройка", "2.1, 2.2")]
        [TestCase(@"Инивидуальная одноэтажная застройка", "2.1, 2.2")]
        [TestCase(@"Под застройку индивидуальную", "2.1, 2.2")]
        [TestCase(@"Малоэтажная ведомственная застройка", "2.1, 2.2")]
        [TestCase(@"Под индивидуальную застройку", "2.1, 2.2")]
        [TestCase(@"Для среднеэтажной застройки", "2.5")]
        [TestCase(@"Многоквартирный дом", "2.1.1, 2.5, 2.6")]
        [TestCase(@"Под многоэтажную ведомственную застройку", "2.5, 2.6")]
        [TestCase("Многоквартирный дом с подземной автопарковкой", "2.1.1, 2.5, 2.6")]
        [TestCase(@"участки размещения многоквартирных жилых домов: объекты размещения жилых и нежилых помещений, инженерного оборудования многоквартирных", "2.5, 2.6")]
        [TestCase(@"Для строительства14-ти этаж. жил. дома с объектом обслуж.насел.в1 эт. в виде прист.", "2.1.1, 2.5, 2.6")]
        [TestCase(@"Под размещение 9-10 этажного дома", "2.1.1, 2.5, 2.6")]
        [TestCase("организации отдыха, культурного проведения свободного времени, укрепления здоровья, а так же для выращивания плодовых, ягодных, овощных и", "13.2")]
        [TestCase("Размещение жилого дачного дома 13.3.0 (не предназначенного для раздела на " +
            "квартиры, пригодного для отдыха и проживания, высотой не выше трех", "13.2")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗЕМЕЛЬНЫХ УЧАСТКОВ САДОВ.", "13.2")]
        [TestCase(@"Для ведения садоводства", "13.2")]
        [TestCase(@"Под коллективный сад", "13.2")]
        [TestCase(@"Под коллективный сад ""Озон""", "13.2")]
        #endregion
        #region Type 300
        [TestCase("эксплуатации гаражного бокса для хранения индивидуального транспортного средства", "2.7.1")]
        [TestCase("ЭКСПЛУАТАЦИИ ВРЕМЕННОГО МЕТАЛЛИЧЕСКОГО СБОРНО-РАЗБОРНОГО ГАРАЖА.", "2.7.1")]
        [TestCase(@"ПОД УСТАНОВКУ МЕТАЛЛИЧЕСКОГО УКРЫТИЯ ТИПА ""ПЕНАЛ"" ДЛЯ ХРАНЕНИЯ АВТОМАШИНЫ ВАЗ-21053", "2.7.1")]
        [TestCase(@"проектирования, строительства и последующей эксплуатации одно- двухэтажных гаражей боксового типа для машин индивидуального пользовани", "2.7.1")]
        [TestCase(@"дальнейшей эксплуатации гаражей боксового типа (железобетонные гаражи) на 59 (пятьдесят девять) машиномест с целью хранения личного автотр", "2.7.1")]
        [TestCase(@"ПОД УСТАНОВКУ МЕТАЛЛИЧЕСКОГО УКРЫТИЯ ТИПА ""РАКУШКА"" ДЛЯ ХРАНЕНИЯ АВТОМАШИНЫ МАРКИ ""ОКА - 11113-02""", "2.7.1")]
        [TestCase(@"Под металл. гараж", "2.7.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ИНДИВИДУАЛЬНОГО КИРПИЧНОГО ГАРАЖА", "2.7.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ гаража-тента типа ""Ракушка""", "2.7.1")]
        [TestCase(@"СТРОИТЕЛЬСТВА И ПОСЛЕДУЮЩЕЙ ЭКСПЛУАТАЦИИ ПЛОЩАДКИ ДЛЯ ХРАНЕНИЯ АВТОМОБИЛЕЙ НА 90 М/М, ОБОРУДОВАННОЙ СБОРНО-РАЗБОРНЫМИ МЕТАЛЛИЧЕСКИМИ ТЕН", "2.7.1")]
        [TestCase(@"проектирования, строительства и последующей эксплуатации одно- двухэтажных гаражей боксового типа для машин индивидуального пользовани", "2.7.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ СУЩЕСТВУЮЩИХ КИРПИЧНЫХ ГАРАЖЕЙ БОКСОВОГО ТИПА НА 380 МАШИНОМЕСТ ДЛЯ ХРАНЕНИЯ ЛЕГКОВОГО АВТОТРАНСПОРТА ИНДИВИДУАЛЬНОГО ПОЛЬ", "2.7.1")]
        [TestCase(@"Занимаемый подземными боксами", "2.7.1")]
        [TestCase(@"для эксплуатации временных металлических и капитальных гаражей", "2.7.1")]
        [TestCase(@"Для эксплуатации автокооператива", "2.7.1")]
        [TestCase(@"Для строительства гаражей для индивидуального транспорта", "2.7.1")]
        [TestCase(@"Для обслуживания  метал.гаража", "2.7.1")]
        [TestCase(@"Под гараж металлический", "2.7.1")]
        [TestCase(@"для установки и обслуживания металлического минигаража типа ""ракушка""", "2.7.1")]
        [TestCase(@"Для эксплуатации метал. гаража", "2.7.1")]
        [TestCase(@"эксплуатации территории под временное размещение гостевого автотранспорта ТК ""Ярославский"" и проведения  проектно-изыскательских работ", "2.7.1")]
        [TestCase(@"Под общественную застройку", "3.0, 4.0")]
        [TestCase(@"для ВЗУ № 2-5", "3.1.1")]
        [TestCase(@"для размещения узла регулирования питьевой воды", "3.1.1")]
        [TestCase(@"Под магистральный водовод", "7.5")]
        [TestCase(@"для проектирования и строительства объекта: ""Водовод в Кировском и Демском районах городского округа""", "7.5")]
        [TestCase(@"объекты газоснабжения", "3.1.1")]
        [TestCase(@"Под объекты газоснабжения", "3.1.1")]
        [TestCase(@"под объекты газообеспечения", "3.1.1")]
        [TestCase(@"Под площадку напорных резервуаров", "3.1.1")]
        [TestCase(@"для строительства инженерной сети ливневой канализации по объекту ""Территория квартала, ограниченног""", "3.1.1")]
        [TestCase(@"Для проектирования и строительства инженерной сети ливневой канализации по объекту: ""Территория квар", "3.1.1")]
        [TestCase(@"Для проектирования и строительства инженерной сети водоотведения по объекту: ""Территория квартала, о", "3.1.1")]
        [TestCase(@"Под распределительную подстанцию № 202", "3.1.1")]
        [TestCase(@"Под объекты инженерного оборудования", "3.1.1")]
        [TestCase(@"Для эксплуатации КТПН", "3.1.1")]
        [TestCase(@"для эксплуатации распределительной подстанции - 128", "3.1.1")]
        [TestCase(@"Для строительства пульпопроводов, водосборной канавы, сборных водоводов", "3.1.1")]
        [TestCase(@"занимаемый  КТПН- 7115", "3.1.1")]
        [TestCase(@"Для эксплуатации ГРШП №36", "3.1.1")]
        [TestCase(@"Для размещения объектов электросетевого хозяйства", "3.1.1")]
        [TestCase(@"для размещения оборудования базовых станций подвижной радиосвязи сотовых операторов", "3.1.1")]
        [TestCase(@"Под объекты инженерного оборудования Газоснабжения", "3.1.1")]
        [TestCase(@"Для эксплуатации насосной", "3.1.1")]
        [TestCase(@"ПРОКЛАДКА дождевой канализации, в том числе состоящий из двенадцати обособленных участков: Р1-79 кв.м; Р2- 104 кв.м; Р3-136 кв.м; Р4-106 кв.м; Р5-142 кв.м;", "3.1.1")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств технических служб обеспеч", "3.1.1")]
        [TestCase(@"дальнейшей эксплуатации существующих зданий и сооружений 5-го района водопроводной сети, без права возведения капитальных строений и соо", "3.1.1")]
        [TestCase(@"эксплуатации базы по хранению противогололедных материалов", "3.1.1")]
        [TestCase(@"участки размещения специальных объектов: объекты размещения помещений и технических устройств водопроводных регулирующих узлов, водоза", "3.1.1")]
        [TestCase(@"участки размещения специальных объектов: объекты размещения помещений и технических устройств городских канализационных очистных соору", "3.1.1")]
        [TestCase(@"участки размещения специальных объектов: объекты размещения помещений и технических устройств пунктов перехода ВЛЭП (1.2.13), земельные уча", "3.1.1")]
        [TestCase(@"эксплуатация базы строительной и дорожной техники, являющейся некапитальным объектом (движимым имуществом) (земельные участки, предназна", "3.1.1")]
        [TestCase(@"эксплуатация зоны коммунальных предприятий", "3.1.1")]
        [TestCase(@"под инженерно-техническими объектами жилого поселка", "3.1.1")]
        [TestCase(@"Для обслуживания ВЛ", "3.1.1")]
        [TestCase(@"для эксплуатации ВЛ-110 кВ", "3.1.1")]
        [TestCase(@"Для размещения коммуникаций", "3.1.1")]
        [TestCase(@"Для обустройства и содержания коммуникаций", "3.1.1")]
        [TestCase(@"Для обустройства и содержания инженерно-технических сооружений и заграждений", "3.1.1")]
        [TestCase(@"для установки блочного модульного контейнера с оборудованием частотного регулирования насосной № 4", "3.1.1")]
        [TestCase(@"временного размещения автоматической станции контроля загрязнения атмосферного воздуха, являющегося нестационарным объектом, движимым", "3.9.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ПОСТА ПО ОТБОРУ И АНАЛИЗУ ПРОБ АТМОСФЕРНОГО ВОЗДУХА", "3.9.1")]
        [TestCase(@"СТРОИТЕЛЬСТВА И ПОСЛЕДУЮЩЕЙ ЭКСПЛУАТАЦИИ СТАЦИОНАРНОГО ПОСТА ЭКОЛОГИЧЕСКОГО КОНТРОЛЯ АВТОТРАНСПОРТА", "3.9.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ КРЫТАЯ А/С №59 НА 8 МАШИНОМЕСТ", "4.9")]
        [TestCase(@"для строительства и размещения объектов автохозяйства", "4.9")]        
        [TestCase(@"для организации временного разворотного кольца и отстоя общественного транспорта с временными объект", "4.9, 7.2.3")]        
        [TestCase(@"Для размещения временной площадки для хранения грузового автотранспорта", "4.9")]
        [TestCase(@"для размещения временной площадки межрейсового отстоя общественного транспорта", "4.9")]
        [TestCase(@"разработки разрешительной документации для реконструкции и строительства диагностического корпуса технического осмотра автотранспорт", "4.9.1.4")]
        [TestCase(@"для размещения мастерской сервисного обслуживания и подъездной автодороги", "4.9.1.4, 12.0.1")]
        [TestCase(@"для строительства геофизического шурфа испытательной станции", "6.1")]
        [TestCase(@"Для добычи строительного грунта", "6.1")]
        [TestCase(@"для пользования недрами с целью добычи песка и песчано-гравийной смеси", "6.1")]
        [TestCase(@"Под карьер", "6.1")]
        [TestCase(@"занимаемый карьером", "6.1")]
        [TestCase(@"Разработка карьера", "6.1")]
        [TestCase(@"Основной вид разрешенного использования: 6.2.0 - Размещение объектов капитального строительства горно-обогатительной и горно-перерабатыва", "6.2")]
        [TestCase(@"Размещение объектов капитального строительства горно-обогатительной и горно-перерабатывающей, металлургической, машиностроительной пр", "6.2")]
        [TestCase(@"занимаемый хлебопекарней", "6.4")]
        [TestCase(@"Для размещения объектов нефтедобычи", "6.5")]
        [TestCase(@"Базы столярного цеха", "6.6")]
        [TestCase(@"Под базу столярного цеха", "6.6")]
        [TestCase(@"занимаемый иными сооружениями (""Электрическая сеть 10 кВ и 0, 4 кВ с целью электроснабжения застройки", "6.7")]
        [TestCase(@"Земли энергосистем", "6.7")]
        [TestCase(@"Земли тепловой энергетики", "6.7")]
        [TestCase("для размещения комплекса зданий объекта Узел связи с инженерно - техническим центром в п.Газопровод", "6.8")]
        [TestCase("Связь", "6.8")]
        [TestCase("Строительство волоконно-оптической линии передачи", "6.8")]
        [TestCase("Строительство волоконно-оптической линии передачи", "6.8")]
        [TestCase(@"земельные участки, предназначенные для размещения зданий, строений, сооружений материально-технического, продовольственного снабжения,", "6.9")]
        [TestCase(@"для проектирования, строительства и дальнейшей эксплуатации базы хранения нерудных материалов со зданием весовой (общая площадь объекта", "6.9")]
        [TestCase(@"Под площадку для хранения стройматериалов", "6.9")]
        [TestCase(@"размещение сооружений, имеющих назначение по временному хранению, распределению и перевалке грузов (за исключением хранения стратегичес", "6.9")]
        [TestCase(@"земельные участки предназначенные для размещения зданий, строений, сооружений материально-технического, продовольственного снабжения, с", "6.9")]
        [TestCase(@"эксплуатация зданий и сооружений существующей дорожно-транспортной базы", "6.9")]
        [TestCase(@"РАЗМЕЩЕНИЯ   ХРАНЕНИЯ МЕДИКАМЕНТОВ", "6.9")]
        [TestCase("эксплуатации издательства", "6.11")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств линейных объектов железн", "7.1.2")]
        [TestCase(@"объекты размещения помещений, технических устройств и сооружений технической инфраструктуры железнодорожного транспорта, грузовых и со", "7.1.2")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств конечных станций", "7.2.2")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗДАНИЯ АВТОСТАНЦИИ ""ВЫХИНО""", "7.2.2")]
        [TestCase(@"для размещения диспетчерского пункта по обслуживанию общественного транспорта", "7.2.2")]
        [TestCase(@"Для ожидания временного навеса для ожидания транспорта", "7.2.2")]
        [TestCase(@"Для установки временного навеса для ожидания транспорта", "7.2.2")]
        [TestCase(@"Под площадку ожидания транспорта", "7.2.2")]
        [TestCase(@"Под навес площадку для ожидания транспорта", "7.2.2")]
        [TestCase(@"Для размещения площадки ожидания транспорта", "7.2.2")]
        [TestCase(@"Для размещения диспетчерского пункта общественного транспорта г. Уфы в составе временного остановочн", "7.2.2")]
        [TestCase(@"Под установку навеса для ожидания транспорта", "7.2.2")]
        [TestCase(@"Для размещения навеса для ожидания транспорта, совмещенного с временным объектом мелкорозничной торг", "7.2.2")]
        [TestCase(@"ДЛЯ ЭКСПЛУАТАЦИИ ЗДАНИЙ И СООРУЖЕНИЙ 147 АВТОМОБИЛЬНОЙ БАЗЫ И ГОСУДАРСТВЕННОЙ РЕГИСТРАЦИИ ИМУЩЕСТВЕННЫХ ПРАВ В УСТАНОВЛЕННОМ ПОРЯДКЕ.", "7.2.3")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств аэропортов, аэродромов", "7.4")]
        [TestCase(@"воздушный транспорт (земельные участки, предназначенные для размещения портов, водных, железнодорожных вокзалов, автодорожных вокзалов,", "7.4")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств, связанных с эксплуатацией воздушного транспорта (1.2.11)", "7.4")]
        [TestCase(@"размещение объектов капитального строительства, необходимых для подготовки и поддержания в боевой готовности Вооруженных Сил Российско", "8.0")]
        [TestCase(@"эксплуатации здания мобилизационного назначения", "8.0")]
        [TestCase(@"для размещения в/ч", "8.0")]
        [TestCase(@"Под промплощадку, жилые и иные помещения, необходимые для содержания заключенных", "8.4")]
        [TestCase(@"гидротехнические сооружения (11.3) (земельные участки улиц, проспектов, площадей, шоссе, аллей, бульваров, застав, переулков, проездов, тупико", "11.3")]
        [TestCase(@"на период разработки исходно-разрешительной документации на строительство здания 19 пожарной части, в том числе участок площадью 0,1413 га по", "8.3")]
        [TestCase(@"объекты размещения организаций и учреждений обеспечения безопасности (1.2.17)", "8.3")]
        [TestCase(@"ПОД ЭКСПЛУАТАЦИЮ ПОДЗЕМНОГО БЛОКА ДВОЙНОГО НАЗНАЧЕНИЯ", "8.3")]
        [TestCase(@"ПОД ЭКСПЛУАТАЦИЮ ФИЛИАЛОМ УВО ПРИ ГУВД Г.МОСКВЫ - 3 МЕЖРАЙОННЫМ ОТДЕЛОМ ВНЕВЕДОМСТВЕННОЙ ОХРАНЫ ОТДЕЛА ПО РУКОВОДСТВУ СЛУЖБАМИ ВНЕВЕДОМСТ", "8.3")]
        [TestCase(@"РАЗРАБОТКИ АРИ УЧАСТКА ТЕРРИТОРИИ ГО, ВОЗВЕДЕНИЯ И ПОСЛЕДУЮЩЕЙ ЭКСПЛУАТАЦИИ ПРЕДПРИЯТИЯ ПО РЕМОНТУ И ТЕХ. ОБСЛУЖИВАНИЮ ИТС ОБЩЕЙ ПЛОЩАДЬ", "8.3")]
        [TestCase(@"на период разработки исходно-разрешительной документации на строительство здания 19 пожарной части, в том числе участок площадью 0,1413 га по", "8.3")]
        [TestCase(@"эксплуатации зданий автохозяйства № 6 ГУВД.", "8.3")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗДАНИЯ И СООРУЖЕНИЯ МЧС", "8.3")]
        [TestCase(@"Под пожарную часть", "8.3")]
        [TestCase(@"эксплуатации существующих зданий, строений и сооружений МЧС", "8.3")]
        [TestCase(@"Для размещения пульта централизованной охраны", "8.3")]
        [TestCase(@"ЭКСПЛУАТАЦИИ СУЩЕСТВУЮЩИХ ЗДАНИЙ И СООРУЖЕНИЙ С ЦЕЛЬЮ СОДЕРЖАНИЯ РОТЫ ОХРАНЫ СПЕЦКОНТИНГЕНТА", "8.4")]
        [TestCase(@"Для лесоразведения", "10.0")]
        [TestCase(@"гидротехнические сооружения (11.3) (земельные участки улиц, проспектов, площадей, шоссе, аллей, бульваров, застав, переулков, проездов, тупико", "11.3")]
        [TestCase(@"Для установки нестационарного объекта по оказанию ритуальных услуг (типа павильон)", "12.1")]
        [TestCase(@"ПРОЕКТИРОВАНИЯ, СТРОИТЕЛЬСТВА И ПОСЛЕДУЮЩЕЙ ЭКСПЛУАТАЦИИ СТАЦИОНАРНОГО ТЕХНИЧЕСКОГО ПОСТА ПО ЗАМЕНЕ И СБОРУ ОТРАБОТАННЫХ АВТОМОБИЛЬНЫХ", "12.2")]
        [TestCase(@"на период проектирования, строительства и дальнейшей эксплуатации временного технического поста по замене и сбору отработанных автомоби", "12.2")]
        [TestCase(@"под площадку для сбора и хранения брошенного и разукомплектованного автотранспорта (площадь 0,9507га) и под площадку для разбора и первичной", "12.2")]
        [TestCase(@"разработки Акта разрешенного использования Москомархитектуры на размещение площадки централизованного сбора, сортировки и временного х", "12.2")]
        [TestCase(@"По продаже и сбору отработанных моторных масел", "12.2")]
        [TestCase(@"Под кап. мусоросборник", "12.2")]
        [TestCase(@"Установка временного объекта по приему стеклопосуды (типа ""павильон"")", "12.2")]
        [TestCase(@"занимаемый нестационарным объектом (бункер-накопитель ТБО, типа ""павильон"")", "12.2")]
        [TestCase(@"Для эксплуатации мусоросборника", "12.2")]
        #endregion
        #region Type 400
        [TestCase(@"земельные участки, занятые особо охраняемыми природными территориями и объектами (1.2.14)", "9.0")]
        [TestCase(@"эксплуатация Природного комплекса №37 ""Коробовские сады(памятник природы)""", "9.0")]
        [TestCase(@"Для размещения особо охраняемых природных объектов (территорий)", "9.0")]
        [TestCase(@"Памятники природы", "9.0")]
        [TestCase(@"защинтые леса", "9.1")]
        [TestCase(@"Природные заказники", "9.1")]
        [TestCase(@"Защитные леса", "9.1")]
        [TestCase(@"Земли общего назначения", "12.0")]
        [TestCase("Благоустройство территории", "12.0.2")]
        #endregion
        #region Type 500
        [TestCase(@"Под объекты транспорта", "7.0")]
        [TestCase(@"Для эксплуатации полосы отвода железнодорожной магистрали", "7.1.1")]
        [TestCase(@"Под объекты транспорта Автомобильного", "7.2")]
        [TestCase(@"размещения и последующей эксплуатации площадки досмотра автотранспорта", "7.2.1")]
        [TestCase(@"организации площадки досмотра въезжающего автотранспорта", "7.2.1")]
        [TestCase(@"Для размещения и эксплуатации объектов автомобильного транспорта и объектов дорожного хозяйства", "3.1.1, 7.2")]
        [TestCase(@"Для строительства подъездной дороги и разворотной площадки", "12.0.1")]
        [TestCase(@"Для строительства подъездной автодороги и разворотной площадки", "12.0.1")]
        [TestCase(@"Для строительства подъездной автомобильной дороги и разворотной площадки", "12.0.1")]
        [TestCase(@"СТРОИТЕЛЬСТВА ОБЪЕКТА ГОРОДСКОГО ЗАКАЗА № 01-038 ""СТР - ВО ТРАНСПОРТНОЙ РАЗВЯЗКИ НА ПР.МИРА С РЕКОНСТРУКЦИЕЙ СЕВЕРЯНИНСКОГО ПУТЕПРОВОДА"" (Р1-58", "7.2.1, 12.0.1")]
        [TestCase("Для размещения дорожных сооружений", "12.0.1")]
        [TestCase("Для эксплуатации дороги", "12.0.1")]
        [TestCase("Для размещения внутрихозяйственных дорог и коммуникаций", "12.0.1")]
        [TestCase("Для эксплуатации эстакады", "12.0.1")]
        #endregion
        #region Type 600
        [TestCase(@"Земли железнодорожного транспорта.", "7.1")]
        [TestCase(@"занимаемый оборудованием (натуральными образцами) железнодорожного транспорта, используемыми в учебн", "7.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ СУЩЕСТВУЮЩИХ ЖЕЛЕЗНОДОРОЖНЫХ ПОДЪЕЗДНЫХ ПУТЕЙ", "7.1.1")]
        [TestCase(@"объекты размещения помещений и технических устройств линейных объектов железнодорожного и скоростного внеуличного транспорта, тяговых", "7.1.1, 7.6")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств линейных объектов железнодорожного и скоростного внеуличного транспорта (1.2.13)", "7.1.1, 7.6")]
        [TestCase(@"объекты размещения технических устройств линейных объектов железнодорожного транспорта (1.2.13); участки смешанного размещения общественн", "7.1.1")]
        [TestCase(@"объекты размещения полос отвода железных дорог, объектов, необходимых для эксплуатации, содержания, строительства устройств транспорта (1", "7.1.1")]
        [TestCase(@"ДЛЯ ИСПОЛЬЗОВАНИЯ ПОДЪЕЗДНЫХ ПУТЕЙ НАХОДЯЩИХСЯ В СОВМЕСТНОМ ПОЛЬЗОВАНИИ С ОАО ""МОСКОВСКАЯ ТОПЛИВНАЯ КОМПАНИЯ"" (2 УЧАСТКА); ИСПОЛЬЗОВАНИЯ П", "7.1.1")]
        [TestCase(@"Для размещения и эксплуатации объектов жележнодорожного транспорта", "7.1")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств линейных объектов скорос", "7.6")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств скоростного внеуличного транспорта", "7.6")]
        #endregion
        #region Type 700        
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств речных портов", "7.3")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств причалов", "7.3")]
        [TestCase(@"Для эксплуатации базы прогулочно-водного транспорта", "7.3")]
        [TestCase(@"Для эксплуатации водной станции", "7.3")]
        [TestCase(@"земельные участки, предназначенные для размещения искусственно созданных внутренних водных путей (1.2.13), земельные участки, занятые сквер", "11.0")]
        [TestCase(@"Под водными объектами", "11.0")]
        [TestCase(@"Для размещения объектов водного фонда", "11.0")]
        [TestCase(@"Для размещения водных объектов", "11.0")]
        #endregion
        #region Type 800
        [TestCase(@"для иных сельскохозяйственных целей", "1.0")]
        [TestCase(@"Для использования в качестве сельскохозяйственных угодий", "1.0")]
        [TestCase(@"Для голубятни", "1.10")]
        [TestCase(@"для производства сельскохозяйственной продукции", "1.18")]
        [TestCase(@"Под сарай", "13.1")]
        [TestCase(@"Для сарая", "13.1")]
        [TestCase(@"Для эксплуатации сараев", "13.1")]
        [TestCase(@"Под хозяйственные постройки", "13.1")]
        [TestCase(@"Для обслуживания хоз.постройки", "13.1")]
        [TestCase(@"Хоз. постройка", "13.1")]
        #endregion
        #region Type 900
        [TestCase(@"земельные участки, занятые особо охраняемыми природными территориями и объектами (1.2.14)", "9.0")]
        [TestCase(@"для иных целей", "12.3")]
        [TestCase(@"", "12.3")]
        [TestCase(@"-", "12.3")]
        [TestCase(@".", "12.3")]
        [TestCase(@"Для размещения временной двусторонней щитовой рекламной установки", "12.3")]
        [TestCase(@"Для размещения рекламной установки", "12.3")]
        [TestCase(@"Для рекламной установки", "12.3")]
        [TestCase(@"Под рекламные щиты", "12.3")]
        [TestCase(@"Под рекламную установку", "12.3")]
        [TestCase(@"Для размещения временной рекламной установки", "12.3")]
        [TestCase(@"Для размещения рекламных установок", "12.3")]
        [TestCase(@"Для размещения временной трехсторонней рекламной установки типа ""Тумба""", "12.3")]
        [TestCase(@"для размещения отдельно стоящей двусторонней щитовой рекламной установки", "12.3")]
        [TestCase(@"Для размещения временной двусторонней рекламно-информационной установки типа ""Пилон""", "12.3")]
        [TestCase(@"временное размещение нестационарной рекламной установки-электронного табло", "12.3")]
        [TestCase(@"Реклам. щит", "12.3")]
        [TestCase(@"Под реклам. щит", "12.3")]
        [TestCase(@"Для установки павильона", "12.3")]
        [TestCase(@"Под павильон", "12.3")]
        [TestCase(@"Земельный участок находится в государственной собственности и не закреплен за конкретными лицами", "12.3")]
        [TestCase(@"Участок госсобственности, не закрепленный за конкретными лицами", "12.3")]
        #endregion
        #region Type 130
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗДАНИЯ ВАКЦИННОГО КОРПУСА", "3.4.1, 3.9.3")]
        [TestCase(@"эксплуатации зданий инженерно-технического комплекса", "3.9.3")]
        [TestCase(@"сплуатация и дальнейшая работа исследовательского центра", "3.9.3")]
        #endregion
        #region Type 999
        [TestCase("земельные участки, предназначенные для сельскохозяйственного использования (1.2.15), занятые водными объектами (береговая полоса), ограниче", "1.1, 11.0")]
        #endregion
        #endregion     
        public void IFactory_OnlyVRYTest(string _vri_doc, string exceptedCodes)
        {
            IInputData data = new InputData(_vri_doc, 350, "", false, false, false);
            IFactory factory = new Factory(data);

            factory.Execute();

            Assert.AreEqual(exceptedCodes, factory.outputData.VRI_List);
        }


        [TestCase(@"эксплуатации парка", "12.0.2")]
        public void IFactory_FastTest(string _vri_doc, string exceptedCodes)
        {
            IInputData data = new InputData(_vri_doc, 0, "", false, false, false);
            IFactory factory = new Factory(data);

            factory.Execute();

            Assert.AreEqual(exceptedCodes, factory.outputData.VRI_List);
        }
    }
}
