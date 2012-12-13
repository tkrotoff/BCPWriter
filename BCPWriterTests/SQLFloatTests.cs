namespace BCPWriter.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for SQLFloat.
    /// </summary>
    /// <see cref="SQLFloat"/>
    [TestFixture]
    internal class SQLFloatTests
    {
        private static void WriteFloat(float? value, ushort nbBits, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLFloat sql = null;
            if (nbBits == 53)
            {
                sql = new SQLFloat();
            }
            else
            {
                sql = new SQLFloat(nbBits);
            }
            sql.Write(writer, value);

            writer.Close();
        }

        private static void WriteFloat(double? value, ushort nbBits, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLFloat sql = null;
            if (nbBits == 53)
            {
                sql = new SQLFloat();
            }
            else
            {
                sql = new SQLFloat(nbBits);
            }
            sql.Write(writer, value);

            writer.Close();
        }

        [Test]
        public void TestFloat1()
        {
            const ushort nbBits = 1;
            const float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat4()
        {
            const ushort nbBits = 4;
            const float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat24()
        {
            const ushort nbBits = 24;
            const float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat25()
        {
            const ushort nbBits = 25;
            const double value = 1234.5678;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat53()
        {
            const ushort nbBits = 53;
            const double value = 1234.5678;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloatNull()
        {
            const ushort nbBits = 53;

            string myFileName = "float_null.bcp";
            WriteFloat((float?)null, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);

            myFileName = "float_null.bcp";
            WriteFloat((double?)null, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloatArgumentException()
        {
            const double value = 1234.5678;

            ushort nbBits = SQLFloat.MIN_NBBITS - 1;
            string myFileName = string.Format("float({0})_argumentexception.bcp", nbBits);
            try
            {
                WriteFloat(value, nbBits, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            nbBits = SQLFloat.MAX_NBBITS + 1;
            myFileName = string.Format("float({0})_argumentexception.bcp", nbBits);
            try
            {
                WriteFloat(value, nbBits, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            nbBits = SQLFloat.MIN_DOUBLE_NBBITS - 1;
            myFileName = string.Format("float({0})_argumentexception.bcp", nbBits);
            try
            {
                WriteFloat(value, nbBits, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }

            nbBits = SQLFloat.MAX_FLOAT_NBBITS + 1;
            myFileName = string.Format("float({0})_argumentexception.bcp", nbBits);
            try
            {
                WriteFloat((float)value, nbBits, myFileName);
                Assert.Fail("Expected an exception, but none was thrown");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
