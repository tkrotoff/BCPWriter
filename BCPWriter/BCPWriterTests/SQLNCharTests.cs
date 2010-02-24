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
    class SQLNCharTests
    {
        public void TestNChar(string text, short length)
        {
            string bcpFileName = string.Format("../../bcp_tests/nchar({0}).bcp", length);
            string myFileName = string.Format("nchar({0}).bcp", length);

            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLNChar sqlNChar = new SQLNChar(text, length);
            sqlNChar.ToBCP(writer);

            writer.Close();

            byte[] myFile = BCPTests.ReadBinaryFile(myFileName);
            byte[] bcpFile = BCPTests.ReadBinaryFile(bcpFileName);

            Assert.AreEqual(bcpFile, myFile);
        }

        [Test]
        public void TestNChar2()
        {
            TestNChar("KI", 2);
        }

        [Test]
        public void TestNChar10()
        {
            TestNChar("KIKOO", 10);
        }

        [Test]
        public void TestNChar1000()
        {
            TestNChar("KIKOO", 1000);
        }
    }
}