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

            SQLXML sqlXML = new SQLXML();
            writer.Write(sqlXML.ToBCP(xmlDoc));

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

            SQLXML sqlXML = new SQLXML();
            writer.Write(sqlXML.ToBCP(null));

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
    }
}