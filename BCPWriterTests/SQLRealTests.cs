namespace BCPWriter.Tests
{
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for SQLReal.
    /// </summary>
    /// <see cref="SQLReal"/>
    [TestFixture]
    class SQLRealTests
    {
        private static void WriteReal(float? value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLReal.Write(writer, value);

            writer.Close();
        }

        [Test]
        public void TestReal()
        {
            const float value = 1234.5678f;

            const string myFileName = "real.bcp";
            WriteReal(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestRealNull()
        {
            const string myFileName = "real_null.bcp";
            WriteReal(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
