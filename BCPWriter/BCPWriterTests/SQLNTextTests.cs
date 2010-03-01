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
    class SQLNTextTests
    {
        private void WriteNText(string text, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLNText sqlNText = new SQLNText();
            writer.Write(sqlNText.ToBCP(text));

            writer.Close();
        }

        [Test]
        public void Test()
        {
            string myFileName = "ntext.bcp";
            WriteNText("KIKOO éùà", myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}