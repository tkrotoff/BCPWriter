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
        public void TestBinary(byte[] data, ushort length)
        {
            string bcpFileName = string.Format("../../bcp_tests/binary({0}).bcp", length);
            string myFileName = string.Format("binary({0}).bcp", length);

            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLBinary sqlBinary = new SQLBinary(data, length);
            sqlBinary.ToBCP(writer);

            writer.Close();

            byte[] myFile = BCPTests.ReadBinaryFile(myFileName);
            byte[] bcpFile = BCPTests.ReadBinaryFile(bcpFileName);

            Assert.AreEqual(bcpFile, myFile);
        }

        [Test]
        public void TestBinary2()
        {
            string test = "KI";
            byte[] data = new byte[test.Length];
            int i = 0;
            foreach (char c in test.ToCharArray())
            {
                data[i] = (byte)c;
                i++;
            }

            TestBinary(data, 2);
        }

        [Test]
        public void TestBinary50()
        {
            string test = "KIKOO";
            byte[] data = new byte[test.Length];
            int i = 0;
            foreach (char c in test.ToCharArray())
            {
                data[i] = (byte)c;
                i++;
            }

            TestBinary(data, 50);
        }

        [Test]
        public void TestBinary1000()
        {
            string test = "KIKOO";
            byte[] data = new byte[test.Length];
            int i = 0;
            foreach (char c in test.ToCharArray())
            {
                data[i] = (byte)c;
                i++;
            }

            TestBinary(data, 1000);
        }
    }
}