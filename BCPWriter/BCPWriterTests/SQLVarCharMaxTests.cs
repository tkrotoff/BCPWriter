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
    class SQLVarCharMaxTests
    {
        private void WriteVarCharMax(string text, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLVarCharMax sqlVarCharMax = new SQLVarCharMax(text);
            sqlVarCharMax.ToBCP(writer);

            writer.Close();
        }

        [Test]
        public void TestVarCharMax()
        {
            string myFileName = "varcharmax.bcp";
            WriteVarCharMax("KIKOO", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestVarCharMaxExmpty()
        {
            string myFileName = "varcharmaxempty.bcp";
            WriteVarCharMax("", myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}