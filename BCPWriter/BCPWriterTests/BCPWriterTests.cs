using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using NUnit.Framework;

using BCPWriter;

namespace BCPWriter.Tests
{
    [TestFixture]
    class BCPWriterTests
    {
        [Test]
        public void TestAllSQLTypes()
        {
            string myFileName = "allsqltypes.bcp";

            BCPWriter writer = new BCPWriter();
            writer.Mode = BCPWriter.BackendMode.Debug;

            writer.AddColumn(new SQLBigInt());
            writer.AddColumn(new SQLBinary(50));
            writer.AddColumn(new SQLChar(10));
            writer.AddColumn(new SQLDate());
            writer.AddColumn(new SQLDateTime());
            writer.AddColumn(new SQLDateTime2());
            writer.AddColumn(new SQLFloat());
            writer.AddColumn(new SQLInt());
            writer.AddColumn(new SQLNChar(10));
            writer.AddColumn(new SQLNText());
            writer.AddColumn(new SQLNVarChar(50));
            writer.AddColumn(new SQLReal());
            writer.AddColumn(new SQLText());
            writer.AddColumn(new SQLTime());
            writer.AddColumn(new SQLUniqueIdentifier());
            writer.AddColumn(new SQLVarBinary(50));
            writer.AddColumn(new SQLVarChar(50));
            writer.AddColumn(new SQLXML());

            List<object> rows = new List<object>();
            rows.Add(9999999999);
            rows.Add(Util.StringToByteArray("binary"));
            rows.Add("char");
            rows.Add(DateTime.Parse("2010-03-03", System.Globalization.CultureInfo.InvariantCulture));
            rows.Add(DateTime.Parse("2010-03-03T14:52:00", System.Globalization.CultureInfo.InvariantCulture));
            rows.Add(DateTime.Parse("2010-03-03T14:52:00", System.Globalization.CultureInfo.InvariantCulture));
            rows.Add(9999999999.9);
            rows.Add(9999);
            rows.Add("nchar");
            rows.Add("ntext");
            rows.Add("nvarchar");
            rows.Add(9999999999.9f);
            rows.Add("text");
            rows.Add(DateTime.Parse("14:52:00", System.Globalization.CultureInfo.InvariantCulture));
            rows.Add(new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"));
            rows.Add(Util.StringToByteArray("varbinary"));
            rows.Add("varchar");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<content>XML</content>");
            rows.Add(xml);

            BinaryWriter stream = new BinaryWriter(new FileStream(myFileName, FileMode.Create));
            writer.WriteRows(stream, rows);
            stream.Close();

            //BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void Test4Columns()
        {
            string myFileName = "4columns.bcp";

            BCPWriter writer = new BCPWriter();
            writer.Mode = BCPWriter.BackendMode.Debug;

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

            BinaryWriter stream = new BinaryWriter(new FileStream(myFileName, FileMode.Create));
            writer.WriteRows(stream, rows);
            stream.Close();

            BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestSQLAPI()
        {
            string myFileName = "sqlapi.bcp";
            BinaryWriter writer = new BinaryWriter(new FileStream(myFileName, FileMode.Create));

            SQLNVarChar firstName = new SQLNVarChar(SQLNVarChar.MAX);
            SQLNVarChar lastName = new SQLNVarChar(SQLNVarChar.MAX);
            SQLInt birth = new SQLInt();
            SQLInt death = new SQLInt();

            firstName.Write(writer, "Frédéric François");
            lastName.Write(writer, "Chopin");
            birth.Write(writer, 1810);
            death.Write(writer, 1849);

            firstName.Write(writer, "Franz");
            lastName.Write(writer, "Liszt");
            birth.Write(writer, 1811);
            death.Write(writer, 1886);

            firstName.Write(writer, "George");
            lastName.Write(writer, "Sand");
            birth.Write(writer, 1804);
            death.Write(writer, 1876);
        }

        [Test]
        public void Test0Columns()
        {
            string myFileName = "0columns.bcp";

            BCPWriter writer = new BCPWriter();
            writer.Mode = BCPWriter.BackendMode.Debug;

            List<object> rows = new List<object>();
            rows.Add("Frédéric François");
            rows.Add("Chopin");
            rows.Add(1810);
            rows.Add(1849);

            try
            {
                BinaryWriter stream = new BinaryWriter(new FileStream(myFileName, FileMode.Create));
                writer.WriteRows(stream, rows);
                stream.Close();
            }
            catch (ArgumentException)
            {
            }
        }

        [Test]
        public void Test3Columns()
        {
            string myFileName = "3columns.bcp";

            BCPWriter writer = new BCPWriter();
            writer.Mode = BCPWriter.BackendMode.Debug;

            //FirstName
            writer.AddColumn(new SQLNVarChar(SQLNVarChar.MAX));

            //LastName
            writer.AddColumn(new SQLNVarChar(SQLNVarChar.MAX));

            //Birth
            writer.AddColumn(new SQLInt());

            //Try with the wrong number of columns
            //Death
            //writer.AddColumn(new SQLInt());

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

            try
            {
                BinaryWriter stream = new BinaryWriter(new FileStream(myFileName, FileMode.Create));
                writer.WriteRows(stream, rows);
                stream.Close();
            }
            catch (InvalidCastException)
            {
            }
        }

        [Test]
        public void TestPerformance()
        {
            string myFileName = "perf.bcp";

            BCPWriter writer = new BCPWriter();
            writer.Mode = BCPWriter.BackendMode.Debug;

            //FirstName
            writer.AddColumn(new SQLNVarChar(SQLNVarChar.MAX));

            //LastName
            writer.AddColumn(new SQLNVarChar(SQLNVarChar.MAX));

            //Birth
            writer.AddColumn(new SQLInt());

            //Death
            writer.AddColumn(new SQLInt());

            //           100,000
            int nbRows = 100000;

            List<object> rows = new List<object>();
            for (int i = 0; i < nbRows; i++)
            {
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
            }

            //BinaryWriter stream = new BinaryWriter(new FileStream(myFileName, FileMode.Create));
            //writer.WriteRows(stream, rows);
            //stream.Close();

            //BCPTests.CheckFile(myFileName);
        }
    }
}