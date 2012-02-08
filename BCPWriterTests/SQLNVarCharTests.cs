using System;
using System.IO;

using NUnit.Framework;

namespace BCPWriter.Tests
{
    /// <summary>
    /// Tests for SQLNVarChar.
    /// </summary>
    /// <see cref="SQLNVarChar"/>
    [TestFixture]
    class SQLNVarCharTests
    {
        private static void WriteNVarChar(string text, uint length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLNVarChar sql = new SQLNVarChar(length);
            sql.Write(writer, text);

            writer.Close();
        }

        [Test]
        public void TestNVarChar50()
        {
            const string myFileName = "nvarchar(50).bcp";
            WriteNVarChar("KIKOO", 50, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNVarCharMax()
        {
            const string myFileName = "nvarchar(max).bcp";
            WriteNVarChar("KIKOO", SQLNVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNVarCharMaxEmpty()
        {
            const string myFileName = "nvarchar(max)_empty.bcp";
            WriteNVarChar("", SQLNVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNVarCharMaxNull()
        {
            const string myFileName = "nvarchar(max)_null.bcp";
            WriteNVarChar(null, SQLNVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNVarChar50Null()
        {
            const string myFileName = "nvarchar(50)_null.bcp";
            WriteNVarChar(null, 50, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test1_xml()
        {
            StreamReader stream = new StreamReader("../../test1.xml");
            string text = stream.ReadToEnd();

            const string myFileName = "SQLNVarCharTest1.bcp";
            WriteNVarChar(text, SQLNVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test2_xml()
        {
            StreamReader stream = new StreamReader("../../test2.xml");
            string text = stream.ReadToEnd();

            const string myFileName = "SQLNVarCharTest2.bcp";
            WriteNVarChar(text, SQLNVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test3_xml()
        {
            StreamReader stream = new StreamReader("../../test3.xml");
            string text = stream.ReadToEnd();

            const string myFileName = "SQLNVarCharTest3.bcp";
            WriteNVarChar(text, SQLNVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test_test4_xml()
        {
            StreamReader stream = new StreamReader("../../test4.xml");
            string text = stream.ReadToEnd();

            const string myFileName = "SQLNVarCharTest4.bcp";
            WriteNVarChar(text, SQLNVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNVarCharArgumentException()
        {
            const string text = "KIKOO";

            uint length = SQLNVarChar.MIN_LENGTH - 1;
            string myFileName = string.Format("char({0})_argumentexception.bcp", length);
            try
            {
                WriteNVarChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = SQLNVarChar.MAX_LENGTH + 1;
            myFileName = string.Format("char({0})_argumentexception.bcp", length);
            try
            {
                WriteNVarChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = (uint)text.Length - 1;
            myFileName = string.Format("char({0})_argumentexception.bcp", length);
            try
            {
                WriteNVarChar(text, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
