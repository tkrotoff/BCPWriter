using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using NUnit.Framework;

using BCPWriter;

namespace BCPWriter.Tests
{
    [TestFixture]
    class SQLXMLTests
    {
        private void WriteXML(string xml, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            SQLXML sql = new SQLXML();
            sql.Write(writer, xmlDoc);

            writer.Close();
        }

        [Test]
        public void TestXML()
        {
            string myFileName = "xml.bcp";
            WriteXML("<content>KIKOO</content>", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestXMLEmpty()
        {
            //Will throw an XmlException : Root element is missing
            //so no need to test

            /*
            string myFileName = "xml_empty.bcp";
            WriteXML("", myFileName);
            BCPTests.CheckFile(myFileName);
            */
        }

        [Test]
        public void TestXMLNull()
        {
            string myFileName = "xml_null.bcp";

            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLXML sql = new SQLXML();
            sql.Write(writer, null);

            writer.Close();

            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test1_xml()
        {
            StreamReader stream = new StreamReader("../../test1.xml");
            string xml = stream.ReadToEnd();

            string myFileName = "SQLXMLTest1.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test2_xml()
        {
            StreamReader stream = new StreamReader("../../test2.xml");
            string xml = stream.ReadToEnd();

            string myFileName = "SQLXMLTest2.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test3_xml()
        {
            StreamReader stream = new StreamReader("../../test3.xml");
            string xml = stream.ReadToEnd();

            string myFileName = "SQLXMLTest3.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test4_xml()
        {
            StreamReader stream = new StreamReader("../../test4.xml");
            string xml = stream.ReadToEnd();

            string myFileName = "SQLXMLTest4.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestWithXMLDeclaration1()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Header Table=\"HoldingData\" User=\"CRED-TS\" Name=\"90GIFD\" Date=\"20060329\" Type=\"HOLDINGS\" ContentsType=\"XML\" Modifier=\"GB\" Action=\"ADD\" />";

            string myFileName = "xmldeclaration1.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestWithXMLDeclaration2()
        {
            string xml = "<?xml version=\"1.0\"?><Header Table=\"HoldingData\" User=\"CRED-TS\" Name=\"90GIFD\" Date=\"20060329\" Type=\"HOLDINGS\" ContentsType=\"XML\" Modifier=\"GB\" Action=\"ADD\" />";

            string myFileName = "xmldeclaration2.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestZeroLastChunk()
        {
            string xml = "<holdings name=\"T1\" date=\"20081029\"><record date=\"20081029\" symbol=\"B1FRNH0\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3106340.9\" uamt=\"3106340.9\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"679206\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3753500\" uamt=\"3753500\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"4640424\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"12982298.36\" uamt=\"12982298.36\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"4190350\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3575185\" uamt=\"3575185\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"3384251\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1687002.44\" uamt=\"1687002.44\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B02MTY0\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4012385.78\" uamt=\"4012385.78\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"340524\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3104198.63\" uamt=\"3104198.63\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B0CSXF7\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2483843\" uamt=\"2483843\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B00VY77\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2277383.78\" uamt=\"2277383.78\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B0R2HC9\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3547995.4\" uamt=\"3547995.4\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B0VLML6\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3892439.05\" uamt=\"3892439.05\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B031S38\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3429904.22\" uamt=\"3429904.22\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B13WKL0\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3121778.06\" uamt=\"3121778.06\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1GN7W0\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1748134.29\" uamt=\"1748134.29\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1H9SK2\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4290721.46\" uamt=\"4290721.46\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B0WFT83\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2609677.1\" uamt=\"2609677.1\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B142G66\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"642395.59\" uamt=\"642395.59\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"3072879\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3627080.5\" uamt=\"3627080.5\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"3132115\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2380000\" uamt=\"2380000\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"5642805\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2175657.4\" uamt=\"2175657.4\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B02FG04\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2927962.78\" uamt=\"2927962.78\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B0LNDC0\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3086652.09\" uamt=\"3086652.09\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B13V222\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4226340.9\" uamt=\"4226340.9\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B197TD4\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1496881.9\" uamt=\"1496881.9\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1BPHP4\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3061367.45\" uamt=\"3061367.45\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1FH1C1\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3276506.68\" uamt=\"3276506.68\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1G6GV1\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1548076.92\" uamt=\"1548076.92\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1GGNN2\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1478832.06\" uamt=\"1478832.06\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1HCJ08\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3839159.05\" uamt=\"3839159.05\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B18TJB9\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3591871.12\" uamt=\"3591871.12\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1DTSG2\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1844782.75\" uamt=\"1844782.75\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"7375530\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"5002330.43\" uamt=\"5002330.43\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B06FPT2\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2760592.15\" uamt=\"2760592.15\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B0NCQB5\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2158121.2\" uamt=\"2158121.2\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1935K1\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1922323.3\" uamt=\"1922323.3\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B19ZYK0\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2341292.45\" uamt=\"2341292.45\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1PF6S6\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2394577.43\" uamt=\"2394577.43\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1274B4\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3677600.88\" uamt=\"3677600.88\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1TX1V4\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4304016.58\" uamt=\"4304016.58\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1VYQW1\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1956002.55\" uamt=\"1956002.55\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1W7Q30\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2067227.88\" uamt=\"2067227.88\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1XBH05\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2759320.09\" uamt=\"2759320.09\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1Z57D8\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2251981.43\" uamt=\"2251981.43\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1Z71S7\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1905133.21\" uamt=\"1905133.21\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1Y42R5\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4199659.46\" uamt=\"4199659.46\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"0BG8LW4\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2844789.34\" uamt=\"2844789.34\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B2838Q6\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3124519.29\" uamt=\"3124519.29\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B28J4P5\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1381767.39\" uamt=\"1381767.39\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B28KB02\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4964773.97\" uamt=\"4964773.97\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"7636873\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2968832.59\" uamt=\"2968832.59\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B2PZMG3\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4660638.79\" uamt=\"4660638.79\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B2Q5PP2\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3349159.85\" uamt=\"3349159.85\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B28G2G3\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4891701.92\" uamt=\"4891701.92\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B28J0L3\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3421152.28\" uamt=\"3421152.28\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B2QQN84\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2708834.91\" uamt=\"2708834.91\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"7059555\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3937140.6\" uamt=\"3937140.6\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"7174221\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"671386.34\" uamt=\"671386.34\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B0X6BG7\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1665429.87\" uamt=\"1665429.87\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B14MHK7\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3346056.75\" uamt=\"3346056.75\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1GYJG1\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3140775.18\" uamt=\"3140775.18\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1NFC35\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"990157.13\" uamt=\"990157.13\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1TR3Y3\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"839375.79\" uamt=\"839375.79\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1W73P1\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3883595.08\" uamt=\"3883595.08\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1YSVH0\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3508513.87\" uamt=\"3508513.87\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B23KKM2\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3455260.42\" uamt=\"3455260.42\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B28BXW1\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3122668.22\" uamt=\"3122668.22\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1TZMJ5\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"527955.8\" uamt=\"527955.8\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1S4SS4\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2704457.65\" uamt=\"2704457.65\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1XFLD8\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2160774.79\" uamt=\"2160774.79\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B236019\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2245937.51\" uamt=\"2245937.51\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1CB2W3\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3628519.91\" uamt=\"3628519.91\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1YGYX1\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2359415.07\" uamt=\"2359415.07\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B2QZFS5\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4613122.61\" uamt=\"4613122.61\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B2R5J93\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4352079.38\" uamt=\"4352079.38\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B2R85R2\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2735303.77\" uamt=\"2735303.77\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B2RHKV8\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1161202.18\" uamt=\"1161202.18\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B39GR86\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4294168.13\" uamt=\"4294168.13\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B39XZ05\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4952220.46\" uamt=\"4952220.46\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B39ZMQ6\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"5277137\" uamt=\"5277137\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B14TB40\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1878420.28\" uamt=\"1878420.28\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B193JS7\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3492854.28\" uamt=\"3492854.28\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B3CX353\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2972968.77\" uamt=\"2972968.77\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B3D0643\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"2880185.27\" uamt=\"2880185.27\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B3D3RP0\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"4218318.27\" uamt=\"4218318.27\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"7042726\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"1259594.11\" uamt=\"1259594.11\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"9934711\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"-11235000\" uamt=\"0\" hpref=\"\" assetcls=\"FUTURE\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"B1VZ033\" symtype=\"SEDOL\" tcode=\"EMV\" xamt=\"3835591.39\" uamt=\"3332378.51\" hpref=\"\" assetcls=\"BOND\" facet=\"HOLDINGS\" /><record date=\"20081029\" symbol=\"9934711\" symtype=\"SEDOL\" tcode=\"SALE\" xamt=\"112335.2\" uamt=\"150\" hpref=\"\" assetcls=\"FUTURE\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"9934711\" symtype=\"SEDOL\" tcode=\"SALE\" xamt=\"112326.8\" uamt=\"150\" hpref=\"\" assetcls=\"FUTURE\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"9934711\" symtype=\"SEDOL\" tcode=\"PRCH\" xamt=\"112410\" uamt=\"7630\" hpref=\"\" assetcls=\"FUTURE\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"9934711\" symtype=\"SEDOL\" tcode=\"PRCH\" xamt=\"0\" uamt=\"2320\" hpref=\"\" assetcls=\"FUTURE\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"B0CSXF7\" symtype=\"SEDOL\" tcode=\"SALE\" xamt=\"301144.125\" uamt=\"315232.31\" hpref=\"\" assetcls=\"BOND\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"*\" xamt=\"\" uamt=\"\" hpref=\"\" assetcls=\"BOND\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"*\" xamt=\"\" uamt=\"\" hpref=\"\" assetcls=\"BOND\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"-6.37\" uamt=\"-6.37\" hpref=\"\" assetcls=\"BOND\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"-7988.12\" uamt=\"-7988.12\" hpref=\"\" assetcls=\"BOND\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"-3780.98\" uamt=\"-3780.98\" hpref=\"\" assetcls=\"BOND\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"-940.47\" uamt=\"-940.47\" hpref=\"\" assetcls=\"BOND\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"*\" xamt=\"\" uamt=\"\" hpref=\"\" assetcls=\"BOND\" facet=\"TRANSACT\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"\" tcode=\"EMV\" xamt=\"17998219.5499999\" uamt=\"7266432.42999992\" hpref=\"\" assetcls=\"CASH\" facet=\"CASH\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"PRCH\" xamt=\"112335.2\" uamt=\"150\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"PRCH\" xamt=\"112326.8\" uamt=\"150\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"SALE\" xamt=\"112410\" uamt=\"7630\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"SALE\" xamt=\"0\" uamt=\"2320\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"PRCH\" xamt=\"301144.125\" uamt=\"315232.31\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"*\" xamt=\"\" uamt=\"\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"*\" xamt=\"\" uamt=\"\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"0.0637\" uamt=\"6.37\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"79.8812\" uamt=\"7988.12\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"37.8098\" uamt=\"3780.98\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"9.4047\" uamt=\"940.47\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /><record date=\"20081029\" symbol=\"CASH_GBP\" symtype=\"SEDOL\" tcode=\"INC\" xamt=\"-1168768.45\" uamt=\"-1168768.45\" hpref=\"\" assetcls=\"CASH\" facet=\"MIRROR\" /></holdings>";

            string myFileName = "xmlzerolastchunk.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}