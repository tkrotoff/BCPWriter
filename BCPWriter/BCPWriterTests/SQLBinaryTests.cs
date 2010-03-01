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
            string test = "KI";
            byte[] data = new byte[test.Length];
            int i = 0;
            foreach (char c in test.ToCharArray())
            {
                data[i] = (byte)c;
                i++;
            }

            ushort length = 2;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
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

            ushort length = 50;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
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

            ushort length = 1000;

            string myFileName = string.Format("binary({0}).bcp", length);
            WriteBinary(data, length, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}