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

            SQLXML sqlXML = new SQLXML(xml);
            sqlXML.ToBCP(writer);

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
    }
}