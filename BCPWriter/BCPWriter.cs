namespace BCPWriter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// BCP writer library: writes a SQL table to a .bcp (Bulk-copy Data) file, see BCPWriter for more documentation.
    /// </summary>
    /// <see cref="BCPWriter"/>
    /// 
    /// @mainpage
    public static class NamespaceDoc
    {
        // Special trick to document the namespace
        // See http://stackoverflow.com/questions/793210/c-xml-documentation-for-a-namespace
    }

    /// <summary>
    /// Writes a SQL table (contains columns and rows) to a .bcp (Bulk-copy Data) file.
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
    /// Why bcp? because bcp is *very* fast for inserting data into MS SQL Server.<br/>
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
    /// You can also use the "simple" functions provided by BCPWriterSimple.
    /// By using BCPWriterSimple you will have to be consistent since there is no fishnet:
    /// the rows you insert won't be checked against the columns so you can easily end up
    /// with a "corrupted" .bcp file.<br/>
    /// Also the MS SQL Server backend (see BCPWriterSQLServer) is not available with BCPWriterSimple
    /// so there won't be any easy way to debug BCPWriter if you encounter bugs.<br/>
    /// <br/>
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
    ///
    /// To view hexadecimal representation of a .bcp file, use
    /// <a href="http://frhed.sourceforge.net/">Frhed - Free hex editor</a>, a GNU GPL
    /// (open source) application.<br/>
    /// <br/>
    /// In order to compare .bcp files together, use
    /// <a href="http://www.cjmweb.net/vbindiff/">VBinDiff - Visual Binary Diff</a>,
    /// a GNU GPL application that highlights the differences between
    /// 2 files in hexadecimal.
    /// </remarks>
    public class BCPWriter : IBCPWriter
    {
        /// <summary>
        /// List of SQL types = columns.
        /// </summary>
        private readonly List<IBCPSerialization> _columns = new List<IBCPSerialization>();

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
            private get;
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

            int nbRows = rows.Count();
            int nbColumns = _columns.Count();

            int modulo = nbRows % nbColumns;
            if (modulo != 0)
            {
                throw new ArgumentException(
                    string.Format("The number of rows ({0}) should match the number of columns ({1})",
                    nbRows, nbColumns));
            }

            for (int i = 0; i < nbRows; i++)
            {
                IBCPSerialization column = _columns[i % nbColumns];
                object row = rows.ElementAt(i);

                column.Write(writer, row);
            }

            if (Mode == BackendMode.Debug)
            {
                new BCPWriterSQLServer(writer, _columns, rows);
            }
        }
    }
}
