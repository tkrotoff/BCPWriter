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
    /// Tests for BCPWriterStatic.
    /// </summary>
    /// <see cref="BCPWriterStatic"/>
    [TestFixture]
    class BCPWriterStaticTests
    {
        [Test]
        public void TestStaticApi()
        {
            BCPWriter writer2 = new BCPWriter();
            writer2.Mode = BCPWriter.BackendMode.Debug;

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
            writer2.AddColumns(columns);

            string myFileName1 = "staticapi1.bcp";
            BinaryWriter stream1 = BCPTests.CreateBinaryFile(myFileName1);
            BCPWriterStatic writer1 = new BCPWriterStatic(stream1);

            List<object> rows = new List<object>();
            const int nbRows = 10;
            for (int i = 0; i < nbRows; i++)
            {
                rows.Add(9999999999);
                writer1.WriteBigInt(9999999999);

                rows.Add(Util.StringToByteArray("binary"));
                writer1.WriteBinary(Util.StringToByteArray("binary"), 50);

                rows.Add("char");
                writer1.WriteChar("char", 10);

                rows.Add(DateTime.Parse("2010-03-03", System.Globalization.CultureInfo.InvariantCulture));
                writer1.WriteDate(DateTime.Parse("2010-03-03", System.Globalization.CultureInfo.InvariantCulture));

                rows.Add(DateTime.Parse("2010-03-03T14:52:00", System.Globalization.CultureInfo.InvariantCulture));
                writer1.WriteDateTime(DateTime.Parse("2010-03-03T14:52:00", System.Globalization.CultureInfo.InvariantCulture));

                rows.Add(DateTime.Parse("2010-03-03T14:52:00", System.Globalization.CultureInfo.InvariantCulture));
                writer1.WriteDateTime2(DateTime.Parse("2010-03-03T14:52:00", System.Globalization.CultureInfo.InvariantCulture));

                rows.Add(9999999999.9);
                writer1.WriteFloat(9999999999.9);

                rows.Add(9999999999.9f);
                writer1.WriteFloat(9999999999.9f, SQLFloat.MAX_FLOAT_NBBITS);

                rows.Add(9999999999.9);
                writer1.WriteFloat(9999999999.9, SQLFloat.MIN_DOUBLE_NBBITS);

                rows.Add(9999);
                writer1.WriteInt(9999);

                rows.Add("nchar");
                writer1.WriteNChar("nchar", 10);

                rows.Add("ntext");
                writer1.WriteNText("ntext");

                rows.Add("nvarchar");
                writer1.WriteNVarChar("nvarchar", 50);

                rows.Add("nvarchar");
                writer1.WriteNVarChar("nvarchar", SQLNVarChar.MAX);

                rows.Add(9999999999.9f);
                writer1.WriteReal(9999999999.9f);

                rows.Add("text");
                writer1.WriteText("text");

                rows.Add(DateTime.Parse("14:52:00", System.Globalization.CultureInfo.InvariantCulture));
                writer1.WriteTime(DateTime.Parse("14:52:00", System.Globalization.CultureInfo.InvariantCulture));

                Guid guid = new Guid("936DA01F-9ABD-4d9d-80C7-02AF85C822A8");
                rows.Add(guid);
                writer1.WriteUniqueIdentifier(guid);

                rows.Add(Util.StringToByteArray("varbinary"));
                writer1.WriteVarBinary(Util.StringToByteArray("varbinary"), 50);

                rows.Add(Util.StringToByteArray("varbinary(max)"));
                writer1.WriteVarBinary(Util.StringToByteArray("varbinary(max)"), SQLVarBinary.MAX);

                rows.Add("varchar");
                writer1.WriteVarChar("varchar", 50);

                rows.Add("varchar(max)");
                writer1.WriteVarChar("varchar(max)", SQLVarChar.MAX);

                XmlDocument xml = new XmlDocument();
                xml.LoadXml("<content>XML</content>");
                rows.Add(xml);
                writer1.WriteXML(xml);
            }

            stream1.Close();

            string myFileName2 = "staticapi2.bcp";
            BinaryWriter stream2 = BCPTests.CreateBinaryFile(myFileName2);
            writer2.WriteRows(stream2, rows);
            stream2.Close();

            byte[] bcpFile1 = BCPTests.ReadBinaryFile(myFileName1);
            byte[] bcpFile2 = BCPTests.ReadBinaryFile(myFileName2);

            Assert.AreEqual(bcpFile1, bcpFile2);
        }
    }
}