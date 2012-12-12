namespace BCPWriter.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for BCPBinary.
    /// </summary>
    /// <see cref="BCPBinary"/>
    [TestFixture]
    class SQLBinaryTests
    {
        private static void WriteBinary(byte[] data, ushort length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLBinary sql = new SQLBinary(length);
            sql.Write(writer, data);

            writer.Close();
        }

        [Test]
        public void TestBinary2()
        {
            byte[] data = Util.StringToByteArray("KI");

            const ushort length = 2;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary50()
        {
            byte[] data = Util.StringToByteArray("KIKOO");

            const ushort length = 50;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary1000()
        {
            byte[] data = Util.StringToByteArray("KIKOO");

            const ushort length = 1000;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary2Null()
        {
            const ushort length = 2;

            string myFileName = string.Format("binary({0})_null.bcp", length);
            WriteBinary(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary50Null()
        {
            const ushort length = 50;

            string myFileName = string.Format("binary({0})_null.bcp", length);
            WriteBinary(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinaryMinNull()
        {
            const ushort length = SQLBinary.MIN_LENGTH;

            string myFileName = string.Format("binary({0})_null.bcp", length);
            WriteBinary(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinaryMaxNull()
        {
            const ushort length = SQLBinary.MAX_LENGTH;

            string myFileName = string.Format("binary({0})_null.bcp", length);
            WriteBinary(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinaryArgumentException()
        {
            byte[] data = Util.StringToByteArray("KIKOO");

            ushort length = SQLBinary.MIN_LENGTH - 1;
            string myFileName = string.Format("binary({0})_argumentexception.bcp", length);
            try
            {
                WriteBinary(data, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = SQLBinary.MAX_LENGTH + 1;
            myFileName = string.Format("binary({0})_argumentexception.bcp", length);
            try
            {
                WriteBinary(data, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            length = (ushort)(data.Length - 1);
            myFileName = string.Format("binary({0})_argumentexception.bcp", length);
            try
            {
                WriteBinary(data, length, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
