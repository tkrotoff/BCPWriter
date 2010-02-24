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
    class SQLIntTests
    {
        public void TestInt(int value)
        {
            string bcpFileName = string.Format("../../bcp_tests/int({0}).bcp", value);
            string myFileName = string.Format("int({0}).bcp", value);

            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLInt sqlInt = new SQLInt(value);
            sqlInt.ToBCP(writer);

            writer.Close();

            byte[] myFile = BCPTests.ReadBinaryFile(myFileName);
            byte[] bcpFile = BCPTests.ReadBinaryFile(bcpFileName);

            Assert.AreEqual(bcpFile, myFile);
        }

        [Test]
        public void TestInt2()
        {
            TestInt(2);
        }

        [Test]
        public void TestInt10()
        {
            TestInt(10);
        }

        [Test]
        public void TestInt1000()
        {
            TestInt(1000);
        }
    }
}