namespace BCPWriter.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for SQLVarBinary.
    /// </summary>
    /// <see cref="SQLVarBinary"/>
    [TestFixture]
    class SQLVarBinaryTests
    {
        private static void WriteVarBinary(byte[] data, uint length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLVarBinary sql = new SQLVarBinary(length);
            sql.Write(writer, data);

            writer.Close();
        }

        [Test]
        public void TestVarBinary50()
        {
            byte[] data = Util.StringToByteArray("KIKOO");

            const string myFileName = "varbinary(50).bcp";
            WriteVarBinary(data, 50, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarBinaryMax()
        {
            byte[] data = Util.StringToByteArray("KIKOO");

            const string myFileName = "varbinary(max).bcp";
            WriteVarBinary(data, SQLVarBinary.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarBinaryMaxEmpty()
        {
            byte[] data = { };
            const string myFileName = "varbinary(max)_empty.bcp";
            WriteVarBinary(data, SQLVarBinary.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinaryMaxNull()
        {
            byte[] data = null;

            const string myFileName = "varbinary(max)_null.bcp";
            WriteVarBinary(data, SQLVarBinary.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary50Null()
        {
            byte[] data = null;

            const string myFileName = "varbinary(50)_null.bcp";
            WriteVarBinary(data, 50, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinaryArgumentException()
        {
            byte[] data = Util.StringToByteArray("KIKOO");

            uint length = SQLVarBinary.MIN_LENGTH - 1;
            string myFileName = string.Format("varbinary({0})_argumentexception.bcp", length);
            try
            {
                WriteVarBinary(data, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = SQLVarBinary.MAX_LENGTH + 1;
            myFileName = string.Format("varbinary({0})_argumentexception.bcp", length);
            try
            {
                WriteVarBinary(data, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = (uint)data.Length - 1;
            myFileName = string.Format("varbinary({0})_argumentexception.bcp", length);
            try
            {
                WriteVarBinary(data, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
