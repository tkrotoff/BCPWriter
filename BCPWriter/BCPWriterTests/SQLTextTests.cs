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
    class SQLTextTests
    {
        private void WriteText(string text, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLText sqlText = new SQLText();
            writer.Write(sqlText.ToBCP(text));

            writer.Close();
        }

        [Test]
        public void Test()
        {
            string myFileName = "text.bcp";
            WriteText("KIKOO éùà", myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}