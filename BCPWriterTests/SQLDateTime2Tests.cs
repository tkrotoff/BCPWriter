using System;
using System.IO;

using NUnit.Framework;

namespace BCPWriter.Tests
{
    /// <summary>
    /// Tests for SQLDateTime2.
    /// </summary>
    /// <see cref="SQLDateTime2"/>
    [TestFixture]
    class SQLDateTime2Tests
    {
        private void WriteDateTime2(DateTime? dateTime, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLDateTime2.Write(writer, dateTime);

            writer.Close();
        }

        [Test]
        public void TestDateTime2Seconds()
        {
            DateTime dateTime = DateTime.Parse(
                                    "2004-05-23T14:25:10",
                                    System.Globalization.CultureInfo.InvariantCulture
                                );

            const string myFileName = "datetime2_seconds.bcp";
            WriteDateTime2(dateTime, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestDateTime2Milliseconds()
        {
            DateTime dateTime = DateTime.Parse(
                                    "2004-05-23T14:25:10.123456789",
                                    System.Globalization.CultureInfo.InvariantCulture
                                );

            const string myFileName = "datetime2_milliseconds.bcp";
            WriteDateTime2(dateTime, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestDateTime2Min()
        {
            DateTime dateTime = new DateTime(0001, 01, 01, 00, 00, 00);

            const string myFileName = "datetime2_min.bcp";
            WriteDateTime2(dateTime, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestDateTime2Null()
        {
            const string myFileName = "datetime2_null.bcp";
            WriteDateTime2(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
