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
    class SQLFloatTests
    {
        private void WriteFloat(uint nbBits, float value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLFloat sql = new SQLFloat(nbBits);
            sql.Write(writer, value);

            writer.Close();
        }

        private void WriteFloat(uint nbBits, double value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLFloat sql = new SQLFloat(nbBits);
            sql.Write(writer, value);

            writer.Close();
        }

        [Test]
        public void TestFloat1()
        {
            uint nbBits = 1;
            float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(nbBits, value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat4()
        {
            uint nbBits = 4;
            float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(nbBits, value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat24()
        {
            uint nbBits = 24;
            float value = 1234.5678f;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(nbBits, value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat25()
        {
            uint nbBits = 25;
            double value = 1234.5678;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(nbBits, value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestFloat53()
        {
            uint nbBits = 53;
            double value = 1234.5678;

            string myFileName = string.Format("float({0}).bcp", nbBits);
            WriteFloat(nbBits, value, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}