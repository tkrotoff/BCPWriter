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
    class SQLBigIntTests
    {
        private void WriteBigInt(long? value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLBigInt sqlBigInt = new SQLBigInt();
            writer.Write(sqlBigInt.ToBCP(value));

            writer.Close();
        }

        [Test]
        public void TestBigIntMin()
        {
            long value = -9223372036854775808;

            string myFileName = "bigint(min).bcp";
            WriteBigInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        public void TestBigIntMax()
        {
            long value = 9223372036854775807;

            string myFileName = "bigint(max).bcp";
            WriteBigInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        public void TestBigIntNull()
        {
            string myFileName = "bigint_null.bcp";
            WriteBigInt(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}