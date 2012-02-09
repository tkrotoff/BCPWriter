using System.IO;

using NUnit.Framework;

namespace BCPWriter.Tests
{
    /// <summary>
    /// Tests for SQLBigInt.
    /// </summary>
    /// <see cref="SQLBigInt"/>
    [TestFixture]
    class SQLBigIntTests
    {
        private static void WriteBigInt(long? value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLBigInt.Write(writer, value);

            writer.Close();
        }

        [Test]
        public void TestBigIntMin()
        {
            const long value = -9223372036854775808;

            const string myFileName = "bigint(min).bcp";
            WriteBigInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBigIntMax()
        {
            const long value = 9223372036854775807;

            const string myFileName = "bigint(max).bcp";
            WriteBigInt(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestBigIntNull()
        {
            const string myFileName = "bigint_null.bcp";
            WriteBigInt(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
