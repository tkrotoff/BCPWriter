using System.IO;

using NUnit.Framework;

namespace BCPWriter.Tests
{
    /// <summary>
    /// Tests for SQLText.
    /// </summary>
    /// <see cref="SQLText"/>
    [TestFixture]
    class SQLTextTests
    {
        private static void WriteText(string text, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLText.Write(writer, text);

            writer.Close();
        }

        [Test]
        public void TestText()
        {
            const string myFileName = "text.bcp";
            WriteText("KIKOO éùà", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestTextEmpty()
        {
            const string myFileName = "text_empty.bcp";
            WriteText("", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestTextNull()
        {
            const string myFileName = "text_null.bcp";
            WriteText(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
