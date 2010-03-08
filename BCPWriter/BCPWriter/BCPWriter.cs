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
    /// </remarks>
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
        }

        /// <summary>
        /// Add a column.
        /// </summary>
        /// <param name="column">SQL type of the column</param>
        public void AddColumn(IBCPSerialization column)
        {
            _columns.Add(column);
        }

        /// <summary>
        /// Add a range of columns.
        /// </summary>
        /// <param name="columns">list of SQL types</param>
        public void AddColumns(IEnumerable<IBCPSerialization> columns)
        {
            _columns.AddRange(columns);
        }

        /// <summary>
        /// Writes all the rows to the .bcp file.
        /// </summary>
        /// <remarks>
        /// You must call AddColumn() or AddColumns() before.<br/>
        /// The rows given to this method should match the SQL types (the columns).</br>
        /// Don't forget to close your BinaryWriter after using this method.
        /// </remarks>
        /// <param name="writer">BinaryWriter to use to write the .bcp file</param>
        /// <param name="rows">the rows to write to the .bcp file</param>
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

            new BCPWriterSQLServer(writer, _columns, rows);
        }

        public void Write(BinaryWriter writer)
        {
        }
    }
}
