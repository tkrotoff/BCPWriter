using System;
using System.IO;

using NUnit.Framework;

namespace BCPWriter.Tests
{
    /// <summary>
    /// Tests for SQLNChar.
    /// </summary>
    /// <see cref="SQLNChar"/>
    [TestFixture]
    class SQLNCharTests
    {
        private static void WriteNChar(string text, ushort length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLNChar sql = new SQLNChar(length);
            sql.Write(writer, text);

            writer.Close();
        }

        [Test]
        public void TestNChar2()
        {
            const ushort length = 2;

            string myFileName = string.Format("nchar({0}).bcp", length);
            WriteNChar("KI", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNChar10()
        {
            const ushort length = 10;

            string myFileName = string.Format("nchar({0}).bcp", length);
            WriteNChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNChar1000()
        {
            const ushort length = 1000;

            string myFileName = string.Format("nchar({0}).bcp", length);
            WriteNChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNChar4000Null()
        {
            const ushort length = 4000;

            string myFileName = string.Format("nchar({0})_null.bcp", length);
            WriteNChar(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNCharArgumentException()
        {
            const string text = "KIKOO";

            ushort length = SQLNChar.MIN_LENGTH - 1;
            string myFileName = string.Format("nchar({0})_argumentexception.bcp", length);
            try
            {
                WriteNChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = SQLNChar.MAX_LENGTH + 1;
            myFileName = string.Format("nchar({0})_argumentexception.bcp", length);
            try
            {
                WriteNChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = (ushort)(text.Length - 1);
            myFileName = string.Format("nchar({0})_argumentexception.bcp", length);
            try
            {
                WriteNChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
