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
    class SQLTimeTests
    {
        private void WriteTime(DateTime? time, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

             SQLTime.Write(writer, time);

            writer.Close();
        }

        [Test]
        public void TestTime()
        {
            DateTime time = DateTime.Parse(
                                    "12:35:29.1234567",
                                    System.Globalization.CultureInfo.InvariantCulture
                                );

            string myFileName = "time.bcp";
            WriteTime(time, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestTimeNull()
        {
            string myFileName = "time_null.bcp";
            WriteTime(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}