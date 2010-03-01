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
        [Test]
        public void Test()
        {
            string myFileName = "nchar(10)_char(10)_int.bcp";

            FileStream stream = new FileStream(myFileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);

            SQLNChar sqlNChar = new SQLNChar(10);
            writer.Write(sqlNChar.ToBCP("KIKOO"));

            SQLChar sqlChar = new SQLChar(10);
            writer.Write(sqlChar.ToBCP("KIKOO"));

            SQLInt sqlInt = new SQLInt();
            writer.Write(sqlInt.ToBCP(10));

            writer.Close();

            BCPTests.CheckFile(myFileName);
        }
    }
}