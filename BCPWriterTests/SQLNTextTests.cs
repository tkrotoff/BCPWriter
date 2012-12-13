namespace BCPWriter.Tests
{
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for SQLNText.
    /// </summary>
    /// <see cref="SQLNText"/>
    [TestFixture]
    internal class SQLNTextTests
    {
        private static void WriteNText(string text, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLNText.Write(writer, text);

            writer.Close();
        }

        [Test]
        public void TestNText()
        {
            const string myFileName = "ntext.bcp";
            WriteNText("KIKOO éùà", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNTextEmpty()
        {
            const string myFileName = "ntext_empty.bcp";
            WriteNText("", myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestNTextNull()
        {
            const string myFileName = "ntext_null.bcp";
            WriteNText(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
