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
    class SQLVarBinaryTests
    {
        private void WriteVarBinary(byte[] data, uint length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLVarBinary sqlVarBinary = new SQLVarBinary(data, length);
            sqlVarBinary.ToBCP(writer);

            writer.Close();
        }

        [Test]
        public void TestVarBinary50()
        {
            byte[] data = SQLBinary.StringToByteArray("KIKOO");

            string myFileName = "varbinary(50).bcp";
            WriteVarBinary(data, 50, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarBinaryMax()
        {
            byte[] data = SQLBinary.StringToByteArray("KIKOO");

            string myFileName = "varbinary(max).bcp";
            WriteVarBinary(data, SQLVarBinary.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarBinaryMaxEmpty()
        {
            byte[] data = {};
            string myFileName = "varbinary(max)_empty.bcp";
            WriteVarBinary(data, SQLVarBinary.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}