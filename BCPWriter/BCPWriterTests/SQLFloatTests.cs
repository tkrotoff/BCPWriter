using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using NUnit.Framework;

using BCPWriter;

namespace BCPWriter.Tests
{
    /// <summary>
    /// Tests for SQLFloat.
    /// </summary>
    /// <see cref="SQLFloat"/>
    [TestFixture]
    class SQLFloatTests
    {
        private void WriteFloat(float? value, ushort nbBits, string myFileName)
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

        private void WriteFloat(double? value, ushort nbBits, string myFileName)
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
            ushort nbBits = 1;
            float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat4()
        {
            ushort nbBits = 4;
            float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat24()
        {
            ushort nbBits = 24;
            float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat25()
        {
            ushort nbBits = 25;
            double value = 1234.5678;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat53()
        {
            ushort nbBits = 53;
            double value = 1234.5678;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(value, nbBits, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloatNull()
        {
            ushort nbBits = 53;

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
            double value = 1234.5678;

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