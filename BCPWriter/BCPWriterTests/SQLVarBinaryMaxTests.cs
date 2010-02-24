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
    class SQLVarBinaryMaxTests
    {
        private void WriteVarBinaryMax(byte[] data, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLVarBinaryMax sqlVarBinaryMax = new SQLVarBinaryMax(data);
            sqlVarBinaryMax.ToBCP(writer);

            writer.Close();
        }

        [Test]
        public void TestVarBinaryMax()
        {
            string test = "KIKOO";
            byte[] data = new byte[test.Length];
            int i = 0;
            foreach (char c in test.ToCharArray())
            {
                data[i] = (byte)c;
                i++;
            }

            string myFileName = "varbinarymax.bcp";
            WriteVarBinaryMax(data, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarBinaryMaxExmpty()
        {
            byte[] data = {};
            string myFileName = "varbinarymaxempty.bcp";
            WriteVarBinaryMax(data, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}