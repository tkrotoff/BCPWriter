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

            string myFileName = "datetime.bcp";
            WriteDateTime(dateTime, myFileName);
            //FIXME Don't understand bcp datetime file format
            //BCPTests.CheckFile(myFileName);
        }
    }
}