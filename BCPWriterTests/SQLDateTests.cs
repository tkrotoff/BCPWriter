using System;
using System.IO;

using NUnit.Framework;

namespace BCPWriter.Tests
{
    /// <summary>
    /// Tests for SQLDate.
    /// </summary>
    /// <see cref="SQLDate"/>
    [TestFixture]
    class SQLDateTests
    {
        private static void WriteDate(DateTime? date, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLDate.Write(writer, date);

            writer.Close();
        }

        [Test]
        public void TestDate()
        {
            DateTime date = DateTime.Parse(
                                    "1753-01-01",
                                    System.Globalization.CultureInfo.InvariantCulture
                                );

            const string myFileName = "date.bcp";
            WriteDate(date, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestDateNull()
        {
            const string myFileName = "date_null.bcp";
            WriteDate(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
