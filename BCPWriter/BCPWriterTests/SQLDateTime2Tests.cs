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
    class SQLDateTime2Tests
    {
        private void WriteDateTime2(DateTime dateTime, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLDateTime2 sqlDateTime2 = new SQLDateTime2();
            writer.Write(sqlDateTime2.ToBCP(dateTime));

            writer.Close();
        }

        [Test]
        public void TestDateTime2()
        {
            DateTime dateTime = DateTime.Parse(
                                    "2004-05-23T14:25:10",
                                    System.Globalization.CultureInfo.InvariantCulture
                                );

            string myFileName = "datetime2.bcp";
            WriteDateTime2(dateTime, myFileName);
            //FIXME Don't understand bcp datetime file format
            //BCPTests.CheckFile(myFileName);
        }
    }
}