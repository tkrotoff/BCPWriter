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
    class SQLVarCharTests
    {
        private void WriteVarChar(string text, uint length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLVarChar sqlVarChar = new SQLVarChar(text, length);
            sqlVarChar.ToBCP(writer);

            writer.Close();
        }

        [Test]
        public void TestVarChar50()
        {
            string myFileName = "varchar(50).bcp";
            WriteVarChar("KIKOO", 50, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMax()
        {
            string myFileName = "varchar(max).bcp";
            WriteVarChar("KIKOO", SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMaxEmpty()
        {
            string myFileName = "varchar(max)_empty.bcp";
            WriteVarChar("", SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}