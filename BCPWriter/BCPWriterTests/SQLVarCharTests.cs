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

            SQLVarChar sqlVarChar = new SQLVarChar(length);
            writer.Write(sqlVarChar.ToBCP(text));

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
    }
}