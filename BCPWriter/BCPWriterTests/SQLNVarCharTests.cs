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
    class SQLNVarCharTests
    {
        private void WriteNVarChar(string text, uint length, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLNVarChar sqlNVarChar = new SQLNVarChar(length);
            writer.Write(sqlNVarChar.ToBCP(text));

            writer.Close();
        }

        [Test]
        public void TestNVarChar50()
        {
            string myFileName = "nvarchar(50).bcp";
            WriteNVarChar("KIKOO", 50, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNVarCharMax()
        {
            string myFileName = "nvarchar(max).bcp";
            WriteNVarChar("KIKOO", SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNVarCharMaxEmpty()
        {
            string myFileName = "nvarchar(max)_empty.bcp";
            WriteNVarChar("", SQLVarChar.MAX, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}