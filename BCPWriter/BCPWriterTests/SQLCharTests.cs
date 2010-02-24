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
        public void TestChar(string text, ushort length)
        {
            string bcpFileName = string.Format("../../bcp_tests/char({0}).bcp", length);
            string myFileName = string.Format("char({0}).bcp", length);

            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLChar sqlChar = new SQLChar(text, length);
            sqlChar.ToBCP(writer);

            writer.Close();

            byte[] myFile = BCPTests.ReadBinaryFile(myFileName);
            byte[] bcpFile = BCPTests.ReadBinaryFile(bcpFileName);

            Assert.AreEqual(bcpFile, myFile);
        }

        [Test]
        public void TestChar2()
        {
            TestChar("KI", 2);
        }

        [Test]
        public void TestChar10()
        {
            TestChar("KIKOO", 10);
        }

        [Test]
        public void TestChar1000()
        {
            TestChar("KIKOO", 1000);
        }

        [Test]
        public void TestCharMinimum()
        {
            TestChar("", SQLChar.MIN_LENGTH);
        }

        [Test]
        public void TestCharMaximum()
        {
            TestChar("KIKOO", SQLChar.MAX_LENGTH);
        }
    }
}