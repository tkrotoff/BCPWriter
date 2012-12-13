namespace BCPWriter.Tests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests for SQLUniqueIdentifier.
    /// </summary>
    /// <see cref="SQLUniqueIdentifier"/>
    [TestFixture]
    internal class SQLUniqueIdentifierTests
    {
        private static void WriteUniqueIdentifier(Guid? guid, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLUniqueIdentifier.Write(writer, guid);

            writer.Close();
        }

        [Test]
        public void TestUniqueIdentifier()
        {
            Guid guid = new Guid("6F9619FF-8B86-D011-B42D-00C04FC964FF");

            const string myFileName = "uniqueidentifier.bcp";
            WriteUniqueIdentifier(guid, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestUniqueIdentifierNull()
        {
            const string myFileName = "uniqueidentifier_null.bcp";
            WriteUniqueIdentifier(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}
