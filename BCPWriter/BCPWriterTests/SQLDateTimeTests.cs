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
    class SQLDateTimeTests
    {
        private void WriteDateTime(DateTime dateTime, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLDateTime sqlDateTime = new SQLDateTime();
            writer.Write(sqlDateTime.ToBCP(dateTime));

            writer.Close();
        }

        [Test]
        public void TestDateTime()
        {
            DateTime dateTime = DateTime.Parse(
                                    "2004-05-23T14:25:10",
                                    System.Globalization.CultureInfo.InvariantCulture
                                );


            DateTime d1 = DateTime.Parse("0001-01-01", System.Globalization.CultureInfo.InvariantCulture);
            DateTime d2 = DateTime.Parse("0002-01-01", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan span = d2 - d1;
            Console.WriteLine
                     ("There're {0} days between {1} and {2}", span.TotalDays, d1.ToString(), d2.ToString());

            string myFileName = "datetime.bcp";
            WriteDateTime(dateTime, myFileName);
            //FIXME Don't understand bcp datetime file format
            //BCPTests.CheckFile(myFileName);
        }
    }
}