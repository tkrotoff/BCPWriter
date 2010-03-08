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
    class SQLVarCharTests
    {
        private void WriteVarChar(string text, uint length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLVarChar sql = new SQLVarChar(length);
            sql.Write(writer, text);

            writer.Close();
        }

        [Test]
        public void TestVarChar10OEMCodePage()
        {
            ushort length = 10;

            string myFileName = string.Format("varchar({0})_oemcodepage.bcp", length);
            WriteVarChar("KIKOO éùà", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarChar50()
        {
            ushort length = 50;

            string myFileName = string.Format("varchar({0}).bcp", length);
            WriteVarChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMax()
        {
            string myFileName = "varchar(max).bcp";
            WriteVarChar("KIKOO", SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMaxEmpty()
        {
            string myFileName = "varchar(max)_empty.bcp";
            WriteVarChar("", SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMaxNull()
        {
            string myFileName = "varchar(max)_null.bcp";
            WriteVarChar(null, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test1_xml()
        {
            StreamReader stream = new StreamReader("../../test1.xml");
            string text = stream.ReadToEnd();

            string myFileName = "SQLVarCharTest1.bcp";
            WriteVarChar(text, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test2_xml()
        {
            StreamReader stream = new StreamReader("../../test2.xml");
            string text = stream.ReadToEnd();

            string myFileName = "SQLVarCharTest2.bcp";
            WriteVarChar(text, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test3_xml()
        {
            StreamReader stream = new StreamReader("../../test3.xml");
            string text = stream.ReadToEnd();

            string myFileName = "SQLVarCharTest3.bcp";
            WriteVarChar(text, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test4_xml()
        {
            StreamReader stream = new StreamReader("../../test4.xml");
            string text = stream.ReadToEnd();

            string myFileName = "SQLVarCharTest4.bcp";
            WriteVarChar(text, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}