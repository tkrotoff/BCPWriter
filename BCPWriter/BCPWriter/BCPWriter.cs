using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// Writes a table (contains columns and rows) to a .bcp file.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms162802.aspx">bcp Utility</a><br/>
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
    /// </remarks>
    /// 
    /// <example>
    /// BCPWrite example of use:
    /// <code>
    /// BCPWriter writer = new BCPWriter("data.bcp");
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
    /// writer.WriteRows(rows);
    /// </code>
    /// </example>
    public class BCPWriter
    {
        /// <summary>
        /// List of SQL types = columns.
        /// </summary>
        private List<IBCPSerialization> _columns = new List<IBCPSerialization>();

        /// <summary>
        /// .bcp file name.
        /// </summary>
        private string _bcpFileName = string.Empty;

        /// <summary>
        /// Creates a bcp file format writer given a .bcp file name.
        /// </summary>
        /// <param name="bcpFileName">.bcp file name where columns and rows will be written</param>
        public BCPWriter(string bcpFileName)
        {
            if (string.IsNullOrEmpty(bcpFileName))
            {
                throw new ArgumentNullException("Empty bcp file name");
            }

            _bcpFileName = bcpFileName;
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
        /// This method directly write to the .bcp file and then close it.
        /// </remarks>
        /// <param name="rows">the rows to write to the .bcp file</param>
        public void WriteRows(IEnumerable<object> rows)
        {
            if (_columns.Count() == 0)
            {
                throw new ArgumentException("No columns");
            }

            FileStream stream = new FileStream(_bcpFileName, FileMode.Create);

            BinaryWriter writer = new BinaryWriter(stream);

            for (int i = 0; i < rows.Count(); i++)
            {
                IBCPSerialization column = _columns[i % _columns.Count()];
                object row = rows.ElementAt(i);

                //FIXME Is there a better way than casting every type?
                //Don't forget to add new SQL types here
                if (column is SQLBinary)
                {
                    writer.Write(((SQLBinary)column).ToBCP((byte[])row));
                }
                else if (column is SQLChar)
                {
                    writer.Write(((SQLChar)column).ToBCP((string)row));
                }
                else if (column is SQLInt)
                {
                    writer.Write(((SQLInt)column).ToBCP((int)row));
                }
                else if (column is SQLNChar)
                {
                    writer.Write(((SQLNChar)column).ToBCP((string)row));
                }
                else if (column is SQLNVarChar)
                {
                    writer.Write(((SQLNVarChar)column).ToBCP((string)row));
                }
                else if (column is SQLVarBinary)
                {
                    writer.Write(((SQLVarBinary)column).ToBCP((byte[])row));
                }
                else if (column is SQLVarChar)
                {
                    writer.Write(((SQLVarChar)column).ToBCP((string)row));
                }
                else if (column is SQLXML)
                {
                    writer.Write(((SQLXML)column).ToBCP((string)row));
                }
                else if (column is SQLFloat)
                {
                    //FIXME cast to float or double?
                    writer.Write(((SQLFloat)column).ToBCP((double)row));
                }
                else if (column is SQLReal)
                {
                    writer.Write(((SQLReal)column).ToBCP((float)row));
                }
                else
                {
                    System.Diagnostics.Trace.Assert(false);
                }
            }

            writer.Close();
        }
    }
}
