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
    class SQLBinaryTests
    {
        private void WriteBinary(byte[] data, ushort length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLBinary sqlBinary = new SQLBinary(length);
            writer.Write(sqlBinary.ToBCP(data));

            writer.Close();
        }

        [Test]
        public void TestBinary2()
        {
            byte[] data = Util.StringToByteArray("KI");

            ushort length = 2;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary50()
        {
            byte[] data = Util.StringToByteArray("KIKOO");

            ushort length = 50;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary1000()
        {
            byte[] data = Util.StringToByteArray("KIKOO");

            ushort length = 1000;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary2Null()
        {
            ushort length = 2;

            string myFileName = string.Format("binary({0})_null.bcp", length);
            WriteBinary(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinary50Null()
        {
            ushort length = 50;

            string myFileName = string.Format("binary({0})_null.bcp", length);
            WriteBinary(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinaryMinNull()
        {
            ushort length = SQLBinary.MIN_LENGTH;

            string myFileName = string.Format("binary({0})_null.bcp", length);
            WriteBinary(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBinaryMaxNull()
        {
            ushort length = SQLBinary.MAX_LENGTH;

            string myFileName = string.Format("binary({0})_null.bcp", length);
            WriteBinary(null, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}