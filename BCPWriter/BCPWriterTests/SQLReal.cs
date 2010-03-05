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
    class SQLRealTests
    {
        private void WriteReal(float value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLReal sql = new SQLReal();
            sql.Write(writer, value);

            writer.Close();
        }

        [Test]
        public void TestReal()
        {
            float value = 1234.5678f;

            string myFileName = "real.bcp";
            WriteReal(value, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
