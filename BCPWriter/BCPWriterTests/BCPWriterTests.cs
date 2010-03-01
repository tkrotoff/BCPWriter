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
    class BCPWriterTests
    {
        [Test]
        public void Test4Columns()
        {
            string myFileName = "4columns.bcp";

            FileStream stream = new FileStream(myFileName, FileMode.Create);

            BCPWriter writer = new BCPWriter(new BinaryWriter(stream));

            //FirstName
            writer.AddColumn(new SQLNVarChar(SQLNVarChar.MAX));

            //LastName
            writer.AddColumn(new SQLNVarChar(SQLNVarChar.MAX));

            //Birth
            writer.AddColumn(new SQLInt());

            //Death
            writer.AddColumn(new SQLInt());

            List<object> rows = new List<object>();
            rows.Add("Frédéric François");
            rows.Add("Chopin");
            rows.Add(1810);
            rows.Add(1849);

            rows.Add("Franz");
            rows.Add("Liszt");
            rows.Add(1811);
            rows.Add(1886);

            rows.Add("George");
            rows.Add("Sand");
            rows.Add(1804);
            rows.Add(1876);

            writer.WriteRows(rows);

            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test0Columns()
        {
            string myFileName = "0columns.bcp";

            FileStream stream = new FileStream(myFileName, FileMode.Create);

            BCPWriter writer = new BCPWriter(new BinaryWriter(stream));

            List<object> rows = new List<object>();
            rows.Add("Frédéric François");
            rows.Add("Chopin");
            rows.Add(1810);
            rows.Add(1849);

            writer.WriteRows(rows);

            BCPTests.CheckFile(myFileName);
        }
    }
}