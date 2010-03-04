using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

            SQLXML sqlXML = new SQLXML();
            writer.Write(sqlXML.ToBCP(xml));

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
            string myFileName = "xml_empty.bcp";
            WriteXML("", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_SQLXMLTest1()
        {
            StreamReader stream = new StreamReader("../../SQLXMLTest1.xml");
            string xml = stream.ReadToEnd();

            string myFileName = "SQLXMLTest1.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_SQLXMLTest2()
        {
            StreamReader stream = new StreamReader("../../SQLXMLTest2.xml");
            string xml = stream.ReadToEnd();

            string myFileName = "SQLXMLTest2.bcp";
            WriteXML(xml, myFileName);
            BCPTests.CheckFile(myFileName);
        }

    }
}