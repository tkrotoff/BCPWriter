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
    class SQLCharTests
    {
        private void WriteChar(string text, ushort length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLChar sqlChar = new SQLChar(length);
            writer.Write(sqlChar.ToBCP(text));

            writer.Close();
        }

        [Test]
        public void TestChar2()
        {
            ushort length = 2;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("KI", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestChar10()
        {
            ushort length = 10;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestChar10OEMCodePage()
        {
            ushort length = 10;

            string myFileName = string.Format("char({0})_oemcodepage.bcp", length);
            WriteChar("KIKOO éùà", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestChar1000()
        {
            ushort length = 1000;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestCharMin()
        {
            ushort length = SQLChar.MIN_LENGTH;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestCharMax()
        {
            ushort length = SQLChar.MAX_LENGTH;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}