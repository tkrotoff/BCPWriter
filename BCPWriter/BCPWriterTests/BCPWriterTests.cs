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
    /// <summary>
    /// Tests for BCPWriter.
    /// </summary>
    /// <see cref="BCPWriter"/>
    [TestFixture]
    class BCPWriterTests
    {
        [Test]
        public void TestAllSQLTypes()
        {
            string myFileName = "allsqltypes.bcp";

            BCPWriter writer = new BCPWriter();
            writer.Mode = BCPWriter.BackendMode.Debug;

            List<IBCPSerialization> columns = new List<IBCPSerialization>();
            columns.Add(new SQLBigInt());
            columns.Add(new SQLBinary(50));
            columns.Add(new SQLChar(10));
            columns.Add(new SQLDate());
            columns.Add(new SQLDateTime());
            columns.Add(new SQLDateTime2());

            columns.Add(new SQLFloat());
            columns.Add(new SQLFloat(SQLFloat.MAX_FLOAT_NBBITS));
            columns.Add(new SQLFloat(SQLFloat.MIN_DOUBLE_NBBITS));

            columns.Add(new SQLInt());
            columns.Add(new SQLNChar(10));
            columns.Add(new SQLNText());
            columns.Add(new SQLNVarChar(50));
            columns.Add(new SQLNVarChar(SQLNVarChar.MAX));
            columns.Add(new SQLReal());
            columns.Add(new SQLText());
            columns.Add(new SQLTime());
            columns.Add(new SQLUniqueIdentifier());
            columns.Add(new SQLVarBinary(50));
            columns.Add(new SQLVarBinary(SQLVarBinary.MAX));
            columns.Add(new SQLVarChar(50));
            columns.Add(new SQLVarChar(SQLVarChar.MAX));
            columns.Add(new SQLXml());
            writer.AddColumns(columns);

            List<object> rows = new List<object>();
            rows.Add(9999999999);
            rows.Add(Util.StringToByteArray("binary"));
            rows.Add("char");
            rows.Add(DateTime.Parse("2010-03-03", System.Globalization.CultureInfo.InvariantCulture));
            rows.Add(DateTime.Parse("2010-03-03T14:52:00", System.Globalization.CultureInfo.InvariantCulture));
            rows.Add(DateTime.Parse("2010-03-03T14:52:00", System.Globalization.CultureInfo.InvariantCulture));

            rows.Add(9999999999.9);
            rows.Add(9999999999.9f);
            rows.Add(9999999999.9);

            rows.Add(9999);
            rows.Add("nchar");
            rows.Add("ntext");
            rows.Add("nvarchar");
            rows.Add("nvarchar");
            rows.Add(9999999999.9f);
            rows.Add("text");
            rows.Add(DateTime.Parse("14:52:00", System.Globalization.CultureInfo.InvariantCulture));
            rows.Add(new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"));
            rows.Add(Util.StringToByteArray("varbinary"));
            rows.Add(Util.StringToByteArray("varbinary(max)"));
            rows.Add("varchar");
            rows.Add("varchar(max)");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<content>XML</content>");
            rows.Add(xml);

            BinaryWriter stream = BCPTests.CreateBinaryFile(myFileName);
            writer.WriteRows(stream, rows);
            stream.Close();

            //BCPTests.CheckFile(myFileName);
        }

        [Test]
        public void TestAllSQLTypesNull()
        {
            string myFileName = "allsqltypes_null.bcp";

            BCPWriter writer = new BCPWriter();
            writer.Mode = BCPWriter.BackendMode.Debug;

            List<IBCPSerialization> columns = new List<IBCPSerialization>();
            columns.Add(new SQLBigInt());
            columns.Add(new SQLBinary(50));
            columns.Add(new SQLChar(10));
            columns.Add(new SQLDate());
            columns.Add(new SQLDateTime());
            columns.Add(new SQLDateTime2());

            columns.Add(new SQLFloat());
            columns.Add(new SQLFloat(SQLFloat.MAX_FLOAT_NBBITS));
            columns.Add(new SQLFloat(SQLFloat.MIN_DOUBLE_NBBITS));

            columns.Add(new SQLInt());
            columns.Add(new SQLNChar(10));
            columns.Add(new SQLNText());
            columns.Add(new SQLNVarChar(50));
            columns.Add(new SQLNVarChar(SQLNVarChar.MAX));
            columns.Add(new SQLReal());
            columns.Add(new SQLText());
            columns.Add(new SQLTime());
            columns.Add(new SQLUniqueIdentifier());
            columns.Add(new SQLVarBinary(50));
            columns.Add(new SQLVarBinary(SQLVarBinary.MAX));
            columns.Add(new SQLVarChar(50));
            columns.Add(new SQLVarChar(SQLVarChar.MAX));
            columns.Add(new SQLXml());
            writer.AddColumns(columns);

            double? valueDouble = null;
            float? valueFloat = null;

            List<object> rows = new List<object>();
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);

            rows.Add(valueDouble);
            rows.Add(valueFloat);
            rows.Add(valueDouble);

            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(valueFloat);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);
            rows.Add(null);

            BinaryWriter stream = BCPTests.CreateBinaryFile(myFileName);
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

            BinaryWriter stream = BCPTests.CreateBinaryFile(myFileName);
            writer.WriteRows(stream, rows);
            stream.Close();

            BCPTests.CheckFile(myFileName);
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
                BinaryWriter stream = BCPTests.CreateBinaryFile(myFileName);
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
                BinaryWriter stream = BCPTests.CreateBinaryFile(myFileName);
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

            const int nbRows = 100000;

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

            //string myFileName = "perf.bcp";
            //BinaryWriter stream = BCPTests.CreateBinaryFile(myFileName);
            //writer.WriteRows(stream, rows);
            //stream.Close();

            //BCPTests.CheckFile(myFileName);
        }
    }
}