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
    /// Tests for SQLNText.
    /// </summary>
    /// <see cref="SQLNText"/>
    [TestFixture]
    class SQLNTextTests
    {
        private void WriteNText(string text, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLNText.Write(writer, text);

            writer.Close();
        }

        [Test]
        public void TestNText()
        {
            string myFileName = "ntext.bcp";
            WriteNText("KIKOO éùà", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNTextEmpty()
        {
            string myFileName = "ntext_empty.bcp";
            WriteNText("", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNTextNull()
        {
            string myFileName = "ntext_null.bcp";
            WriteNText(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}