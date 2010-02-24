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
    class NCharCharIntTest
    {
        public void CheckFile(string myFileName)
        {
            string bcpFileName = "../../bcp_tests/" + myFileName;

            byte[] myFile = BCPTests.ReadBinaryFile(myFileName);
            byte[] bcpFile = BCPTests.ReadBinaryFile(bcpFileName);

            Assert.AreEqual(bcpFile, myFile);
        }

        [Test]
        public void Test()
        {
            string myFileName = "nchar(10)_char(10)_int.bcp";

            FileStream stream = new FileStream(myFileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);

            SQLNChar sqlNChar = new SQLNChar("KIKOO", 10);
            sqlNChar.ToBCP(writer);

            SQLChar sqlChar = new SQLChar("KIKOO", 10);
            sqlChar.ToBCP(writer);

            SQLInt sqlInt = new SQLInt(10);
            sqlInt.ToBCP(writer);

            writer.Close();

            CheckFile(myFileName);
        }
    }
}