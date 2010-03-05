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
        private void WriteInt(int value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLInt sql = new SQLInt();
            sql.Write(writer, value);

            writer.Close();
        }

        [Test]
        public void TestInt2()
        {
            int value = 2;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestInt10()
        {
            int value = 10;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestInt1000()
        {
            int value = 1000;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestIntMin()
        {
            int value = SQLInt.MIN_VALUE;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(SQLInt.MIN_VALUE, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestIntMax()
        {
            int value = SQLInt.MAX_VALUE;

            string myFileName = string.Format("int({0}).bcp", value);
            WriteInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}