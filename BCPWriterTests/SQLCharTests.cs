namespace BCPWriter.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for SQLChar.
    /// </summary>
    /// <see cref="SQLChar"/>
    [TestFixture]
    class SQLCharTests
    {
        private static void WriteChar(string text, ushort length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLChar sql = new SQLChar(length);
            sql.Write(writer, text);

            writer.Close();
        }

        [Test]
        public void TestChar2()
        {
            const ushort length = 2;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("KI", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestChar10()
        {
            const ushort length = 10;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestChar10OEMCodePage()
        {
            const ushort length = 10;

            string myFileName = string.Format("char({0})_oemcodepage.bcp", length);
            WriteChar("KIKOO éùà", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestChar1000()
        {
            const ushort length = 1000;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestCharMin()
        {
            const ushort length = SQLChar.MIN_LENGTH;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestCharMax()
        {
            const ushort length = SQLChar.MAX_LENGTH;

            string myFileName = string.Format("char({0}).bcp", length);
            WriteChar("", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestCharMinNull()
        {
            const ushort length = SQLChar.MIN_LENGTH;

            string myFileName = string.Format("char({0})_null.bcp", length);
            WriteChar(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestCharMaxNull()
        {
            const ushort length = SQLChar.MAX_LENGTH;

            string myFileName = string.Format("char({0})_null.bcp", length);
            WriteChar(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestCharArgumentException()
        {
            const string text = "KIKOO";

            ushort length = (ushort)SQLChar.MIN_LENGTH - 1;
            string myFileName = string.Format("char({0})_argumentexception.bcp", length);
            try
            {
                WriteChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = SQLChar.MAX_LENGTH + 1;
            myFileName = string.Format("char({0})_argumentexception.bcp", length);
            try
            {
                WriteChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = (ushort)(text.Length - 1);
            myFileName = string.Format("char({0})_argumentexception.bcp", length);
            try
            {
                WriteChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
