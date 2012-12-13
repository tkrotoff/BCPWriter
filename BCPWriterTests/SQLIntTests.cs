namespace BCPWriter.Tests
{
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for SQLInt.
    /// </summary>
    /// <see cref="SQLInt"/>
    [TestFixture]
    internal class SQLIntTests
    {
        private static void WriteInt(int? value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLInt.Write(writer, value);

            writer.Close();
        }

        [Test]
        public void TestInt2()
        {
            const int value = 2;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestInt10()
        {
            const int value = 10;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestInt1000()
        {
            const int value = 1000;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestIntMin()
        {
            const int value = SQLInt.MIN_VALUE;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(SQLInt.MIN_VALUE, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestIntMax()
        {
            const int value = SQLInt.MAX_VALUE;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestIntNull()
        {
            const string myFileName = "int_null.bcp";
            WriteInt(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
