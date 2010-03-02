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
    class SQLDateTests
    {
        private void WriteDate(DateTime date, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLDate sqlDate = new SQLDate();
            writer.Write(sqlDate.ToBCP(date));

            writer.Close();
        }

        [Test]
        public void TestDate()
        {
            DateTime dateTime = DateTime.Parse(
                                    "2004-05-23",
                                    System.Globalization.CultureInfo.InvariantCulture
                                );

            string myFileName = "date.bcp";
            WriteDate(dateTime, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}