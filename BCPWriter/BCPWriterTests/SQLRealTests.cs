using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using NUnit.Framework;

using BCPWriter;

namespace BCPWriter.Tests
{
    /// <summary>
    /// Tests for SQLReal.
    /// </summary>
    /// <see cref="SQLReal"/>
    [TestFixture]
    class SQLRealTests
    {
        private void WriteReal(float? value, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLReal.Write(writer, value);

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

        [Test]
        public void TestRealNull()
        {
            string myFileName = "real_null.bcp";
            WriteReal(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
