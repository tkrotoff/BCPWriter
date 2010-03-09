using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace BCPWriter
{
    /// <summary>
    /// Writes a table (contains columns and rows) to a .bcp (Bulk-copy Data) file.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms162802.aspx">bcp Utility</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// The bcp utility bulk copies data between an instance of Microsoft SQL Server
    /// and a data file in a user-specified format.
    /// The bcp utility can be used to import large numbers of new rows into SQL Server
    /// tables or to export data out of tables into data files.<br/>
    /// <br/>
    /// bcp native (binary) format (-n option) is simple:<br/>
    /// <br/>
    /// <code>
    /// |--------------------|
    /// | Data Length | Data |
    /// |--------------------|
    /// </code>
    /// <br/>
    /// Example for a char SQL data type with length 10 (char(10)) and text KIKOO:<br/>
    /// <code>
    /// 0A 00 4B 49 4B 4F 4F 20
    /// 
    /// |-----------------------------------------------|
    /// | Data Length = 10 | Data = KIKOO               |
    /// | 0A 00            | 4B 49 4B 4F 4F 20 20 20 20 |
    /// |-----------------------------------------------|
    /// 
    /// 4B = K
    /// 49 = I
    /// 4B = K
    /// 4F = O
    /// 4F = O
    /// 20 * 4 = 4 * Space, KIKOO is of length 4 so bcp append spaces until to reach length = 10
    /// </code>
    /// <br/>
    /// SQL types are prefixed with 'SQL' in order to avoid clash name with C# types,
    /// i.e SQLInt and Int, SQLChar and Char...<br/>
    /// Not all SQL types have been added to BCPWriter, feel free to add them if needed.
    /// 
    /// <example>
    /// BCPWrite example of use:
    /// <code>
    /// <![CDATA[
    /// BCPWriter writer = new BCPWriter();
    /// 
    /// //FirstName
    /// writer.AddColumn(new SQLNVarChar(SQLNVarChar.MAX));
    /// 
    /// //LastName
    /// writer.AddColumn(new SQLNVarChar(SQLNVarChar.MAX));
    /// 
    /// //Birth
    /// writer.AddColumn(new SQLInt());
    /// 
    /// //Death
    /// writer.AddColumn(new SQLInt());
    /// 
    /// List<object> rows = new List<object>();
    /// rows.Add("Frédéric François");
    /// rows.Add("Chopin");
    /// rows.Add(1810);
    /// rows.Add(1849);
    /// 
    /// rows.Add("Franz");
    /// rows.Add("Liszt");
    /// rows.Add(1811);
    /// rows.Add(1886);
    /// 
    /// rows.Add("George");
    /// rows.Add("Sand");
    /// rows.Add(1804);
    /// rows.Add(1876);
    /// 
    /// BinaryWriter stream = new BinaryWriter(new FileStream("data.bcp", FileMode.Create));
    /// writer.WriteRows(stream, rows);
    /// stream.Close();
    /// ]]>
    /// </code>
    /// </example>
    /// 
    /// <example>
    /// You can also use the SQL classes directly (then you will have to be consistent, no fishnet):
    /// <code>
    /// string myFileName = "sqlapi.bcp";
    /// BinaryWriter writer = new BinaryWriter(new FileStream(myFileName, FileMode.Create));
    /// 
    /// SQLNVarChar firstName = new SQLNVarChar(SQLNVarChar.MAX);
    /// SQLNVarChar lastName = new SQLNVarChar(SQLNVarChar.MAX);
    /// SQLInt birth = new SQLInt();
    /// SQLInt death = new SQLInt();
    /// 
    /// firstName.Write(writer, "Frédéric François");
    /// lastName.Write(writer, "Chopin");
    /// birth.Write(writer, 1810);
    /// death.Write(writer, 1849);
    /// 
    /// firstName.Write(writer, "Franz");
    /// lastName.Write(writer, "Liszt");
    /// birth.Write(writer, 1811);
    /// death.Write(writer, 1886);
    /// 
    /// firstName.Write(writer, "George");
    /// lastName.Write(writer, "Sand");
    /// birth.Write(writer, 1804);
    /// death.Write(writer, 1876);
    /// </code>
    /// </example>
    /// </remarks>
    public class BCPWriter
    {
        /// <summary>
        /// List of SQL types = columns.
        /// </summary>
        private List<IBCPSerialization> _columns = new List<IBCPSerialization>();

        /// <summary>
        /// Creates a bcp file format writer.
        /// </summary>
        public BCPWriter()
        {
            Mode = BackendMode.Normal;
        }

        /// <summary>
        /// How BCPWriter should act.
        /// </summary>
        public enum BackendMode
        {
            /// <summary>
            /// Normal backend mode: BCPWriter will only write .bcp files.
            /// This is the default mode.
            /// </summary>
            Normal,

            /// <summary>
            /// Debug backend mode.
            /// 
            /// BCPWriter will write the .bcp files (same as normal mode)
            /// + BCPWriter will create a table named BCPTest and insert all the data
            /// into it. Then BCPWriter will run bcp out in order to get the .bcp files
            /// from MS SQL Server.
            /// The idea is to compare the .bcp files generated by BCPWriter and the ones
            /// generated by bcp out, this way it is easier to find bugs.
            /// </summary>
            Debug
        }

        /// <summary>
        /// Mode to be used by BCPWriter.
        /// </summary>
        public BackendMode Mode
        {
            get;
            set;
        }

        /// <summary>
        /// Add a column (SQL type).
        /// </summary>
        /// <param name="column">SQL type of the column</param>
        public void AddColumn(IBCPSerialization column)
        {
            _columns.Add(column);
        }

        /// <summary>
        /// Add a range of columns (SQL types).
        /// </summary>
        /// <param name="columns">list of SQL types</param>
        public void AddColumns(IEnumerable<IBCPSerialization> columns)
        {
            _columns.AddRange(columns);
        }

        /// <summary>
        /// Writes all the values to the .bcp file.
        /// </summary>
        /// <remarks>
        /// You must call AddColumn() or AddColumns() before.<br/>
        /// The rows given to this method should match the SQL types (the columns).<br/>
        /// Don't forget to close your BinaryWriter after using this method.
        /// </remarks>
        /// <param name="writer">BinaryWriter to use to write the .bcp file</param>
        /// <param name="rows">the values to write to the .bcp file</param>
        public void WriteRows(BinaryWriter writer, IEnumerable<object> rows)
        {
            if (_columns.Count() == 0)
            {
                throw new ArgumentException("No columns");
            }

            for (int i = 0; i < rows.Count(); i++)
            {
                IBCPSerialization column = _columns[i % _columns.Count()];
                object row = rows.ElementAt(i);

                column.Write(writer, row);
            }

            if (Mode == BackendMode.Debug)
            {
                new BCPWriterSQLServer(writer, _columns, rows);
            }
        }


        #region Static functions

        /// <summary>
        /// Writes a SQL bigint.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteBigInt(BinaryWriter writer, long? value)
        {
            SQLBigInt sql = new SQLBigInt();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL binary.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public static void WriteBinary(BinaryWriter writer, byte[] value, ushort length)
        {
            SQLBinary sql = new SQLBinary(length);
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL char.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        /// <param name="length"></param>
        public static void WriteChar(BinaryWriter writer, string text, ushort length)
        {
            SQLChar sql = new SQLChar(length);
            sql.Write(writer, text);
        }

        /// <summary>
        /// Writes a SQL date.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteDate(BinaryWriter writer, DateTime? value)
        {
            SQLDate sql = new SQLDate();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL datetime.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteDateTime(BinaryWriter writer, DateTime? value)
        {
            SQLDateTime sql = new SQLDateTime();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL datetime2.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteDateTime2(BinaryWriter writer, DateTime? value)
        {
            SQLDateTime2 sql = new SQLDateTime2();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL float.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteFloat(BinaryWriter writer, float? value)
        {
            SQLFloat sql = new SQLFloat();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL float.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteFloat(BinaryWriter writer, double? value)
        {
            SQLFloat sql = new SQLFloat();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL int.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteInt(BinaryWriter writer, int? value)
        {
            SQLInt sql = new SQLInt();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL nchar.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        /// <param name="length"></param>
        public static void WriteNChar(BinaryWriter writer, string text, ushort length)
        {
            SQLNChar sql = new SQLNChar(length);
            sql.Write(writer, text);
        }

        /// <summary>
        /// Writes a SQL ntext.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        public static void WriteNText(BinaryWriter writer, string text)
        {
            SQLNText sql = new SQLNText();
            sql.Write(writer, text);
        }

        /// <summary>
        /// Writes a SQL nvarchar.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        /// <param name="length"></param>
        public static void WriteNVarChar(BinaryWriter writer, string text, uint length)
        {
            SQLNVarChar sql = new SQLNVarChar(length);
            sql.Write(writer, text);
        }

        /// <summary>
        /// Writes a SQL real.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteReal(BinaryWriter writer, float? value)
        {
            SQLReal sql = new SQLReal();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL text.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        public static void WriteText(BinaryWriter writer, string text)
        {
            SQLText sql = new SQLText();
            sql.Write(writer, text);
        }

        /// <summary>
        /// Writes a SQL time.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteTime(BinaryWriter writer, DateTime? value)
        {
            SQLTime sql = new SQLTime();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL uniqueidentifier.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteUniqueIdentifier(BinaryWriter writer, Guid? value)
        {
            SQLUniqueIdentifier sql = new SQLUniqueIdentifier();
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL varbinary.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public static void WriteVarBinary(BinaryWriter writer, byte[] value, uint length)
        {
            SQLVarBinary sql = new SQLVarBinary(length);
            sql.Write(writer, value);
        }

        /// <summary>
        /// Writes a SQL varchar.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        /// <param name="length"></param>
        public static void WriteVarChar(BinaryWriter writer, string text, uint length)
        {
            SQLVarChar sql = new SQLVarChar(length);
            sql.Write(writer, text);
        }

        /// <summary>
        /// Writes a SQL varchar.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="xml"></param>
        public static void WriteXML(BinaryWriter writer, XmlDocument xml)
        {
            SQLXml sql = new SQLXml();
            sql.Write(writer, xml);
        }

        #endregion
    }
}
