namespace BCPWriter.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for SQLVarChar.
    /// </summary>
    /// <see cref="SQLVarChar"/>
    [TestFixture]
    internal class SQLVarCharTests
    {
        private static void WriteVarChar(string text, uint length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLVarChar sql = new SQLVarChar(length);
            sql.Write(writer, text);

            writer.Close();
        }

        [Test]
        public void TestVarChar10OEMCodePage()
        {
            const ushort length = 10;

            string myFileName = string.Format("varchar({0})_oemcodepage.bcp", length);
            WriteVarChar("KIKOO éùà", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarChar50()
        {
            const ushort length = 50;

            string myFileName = string.Format("varchar({0}).bcp", length);
            WriteVarChar("KIKOO", length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMax()
        {
            const string myFileName = "varchar(max).bcp";
            WriteVarChar("KIKOO", SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMaxEmpty()
        {
            const string myFileName = "varchar(max)_empty.bcp";
            WriteVarChar("", SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMaxNull()
        {
            const string myFileName = "varchar(max)_null.bcp";
            WriteVarChar(null, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test1_xml()
        {
            StreamReader stream = new StreamReader("../../test1.xml");
            string text = stream.ReadToEnd();

            const string myFileName = "SQLVarCharTest1.bcp";
            WriteVarChar(text, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test2_xml()
        {
            StreamReader stream = new StreamReader("../../test2.xml");
            string text = stream.ReadToEnd();

            const string myFileName = "SQLVarCharTest2.bcp";
            WriteVarChar(text, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test3_xml()
        {
            StreamReader stream = new StreamReader("../../test3.xml");
            string text = stream.ReadToEnd();

            const string myFileName = "SQLVarCharTest3.bcp";
            WriteVarChar(text, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test4_xml()
        {
            StreamReader stream = new StreamReader("../../test4.xml");
            string text = stream.ReadToEnd();

            const string myFileName = "SQLVarCharTest4.bcp";
            WriteVarChar(text, SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNVarCharArgumentException()
        {
            const string text = "KIKOO";

            uint length = SQLVarChar.MIN_LENGTH - 1;
            string myFileName = string.Format("varchar({0})_argumentexception.bcp", length);
            try
            {
                WriteVarChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = SQLVarChar.MAX_LENGTH + 1;
            myFileName = string.Format("varchar({0})_argumentexception.bcp", length);
            try
            {
                WriteVarChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = (uint)text.Length - 1;
            myFileName = string.Format("varchar({0})_argumentexception.bcp", length);
            try
            {
                WriteVarChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
