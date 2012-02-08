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
    /// Tests for SQLUniqueIdentifier.
    /// </summary>
    /// <see cref="SQLUniqueIdentifier"/>
    [TestFixture]
    class SQLUniqueIdentifierTests
    {
        private void WriteUniqueIdentifier(Guid? guid, string myFileName)
        {
            BinaryWriter writer = BCPTests.CreateBinaryFile(myFileName);

            SQLUniqueIdentifier.Write(writer, guid);

            writer.Close();
        }

        [Test]
        public void TestUniqueIdentifier()
        {
            Guid guid = new Guid("6F9619FF-8B86-D011-B42D-00C04FC964FF");

            string myFileName = "uniqueidentifier.bcp";
            WriteUniqueIdentifier(guid, myFileName);
            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestUniqueIdentifierNull()
        {
            string myFileName = "uniqueidentifier_null.bcp";
            WriteUniqueIdentifier(null, myFileName);
            BCPTests.CheckFile(myFileName);
        }
    }
}