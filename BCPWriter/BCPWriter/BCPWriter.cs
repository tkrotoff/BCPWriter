using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

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

            StringBuilder createTableString = new StringBuilder();
            createTableString.AppendLine("CREATE TABLE BCPTest (");

            StringBuilder insertIntoString = new StringBuilder();
            insertIntoString.AppendLine("INSERT INTO BCPTest VALUES (");

            for (int i = 0; i < rows.Count(); i++)
            {
                IBCPSerialization column = _columns[i % _columns.Count()];
                object row = rows.ElementAt(i);

                //column.Write(writer, row);

                createTableString.AppendFormat("col{0} ", i);

                //FIXME Is there a better way than casting every type?
                //Don't forget to add new SQL types here
                //and to modify the unit tests accordingly
                if (column is SQLBinary)
                {
                    SQLBinary sql = (SQLBinary)column;
                    byte[] value = (byte[])row;

                    createTableString.AppendFormat("binary({0})", sql.Length);
                    insertIntoString.AppendFormat(
                        "CAST('{0}' AS binary({1}))",
                        Encoding.Default.GetString(value), sql.Length);
                    
                    sql.Write(writer, value);
                }
                else if (column is SQLChar)
                {
                    SQLChar sql = (SQLChar)column;
                    string value = (string)row;

                    createTableString.AppendFormat("char({0})", sql.Length);
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLInt)
                {
                    SQLInt sql = (SQLInt)column;
                    int value = (int)row;

                    createTableString.Append("int");
                    insertIntoString.AppendFormat("{0}", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLNChar)
                {
                    SQLNChar sql = (SQLNChar)column;
                    string value = (string)row;

                    createTableString.AppendFormat("nchar({0})", sql.Length);
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLNVarChar)
                {
                    SQLNVarChar sql = (SQLNVarChar)column;
                    string value = (string)row;

                    if (sql.Length == SQLNVarChar.MAX)
                    {
                        createTableString.Append("nvarchar(max)");
                    }
                    else
                    {
                        createTableString.AppendFormat("nvarchar({0})", sql.Length);
                    }
                    insertIntoString.AppendFormat("'{0}'", value);
                    
                    sql.Write(writer, value);
                }
                else if (column is SQLVarBinary)
                {
                    SQLVarBinary sql = (SQLVarBinary)column;
                    byte[] value = (byte[])row;

                    if (sql.Length == SQLVarBinary.MAX)
                    {
                        createTableString.Append("varbinary(max)");
                        insertIntoString.AppendFormat(
                            "CAST('{0}' AS varbinary(max))",
                            Encoding.Default.GetString(value));
                    }
                    else
                    {
                        createTableString.AppendFormat("varbinary({0})", sql.Length);
                        insertIntoString.AppendFormat(
                            "CAST('{0}' AS varbinary({1}))",
                            Encoding.Default.GetString(value), sql.Length);

                    }

                    sql.Write(writer, value);
                }
                else if (column is SQLVarChar)
                {
                    SQLVarChar sql = (SQLVarChar)column;
                    string value = (string)row;

                    if (sql.Length == SQLVarChar.MAX)
                    {
                        createTableString.Append("varchar(max)");
                    }
                    else
                    {
                        createTableString.AppendFormat("varchar({0})", sql.Length);
                    }
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLNText)
                {
                    SQLNText sql = (SQLNText)column;
                    string value = (string)row;

                    createTableString.Append("ntext");
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLText)
                {
                    SQLText sql = (SQLText)column;
                    string value = (string)row;

                    createTableString.Append("text");
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLXML)
                {
                    SQLXML sql = (SQLXML)column;
                    XmlDocument value = (XmlDocument)row;

                    createTableString.Append("xml");
                    insertIntoString.AppendFormat("'{0}'", value.DocumentElement.OuterXml);

                    sql.Write(writer, value);
                }
                else if (column is SQLFloat)
                {
                    SQLFloat sql = (SQLFloat)column;

                    createTableString.Append("float");

                    if (row is float)
                    {
                        float value = (float)row;

                        insertIntoString.AppendFormat("{0}", value);

                        sql.Write(writer, value);
                    }
                    else
                    {
                        //If we don't know, let's cast it to double
                        double value = (double)row;

                        insertIntoString.AppendFormat("{0}", value);

                        sql.Write(writer, value);
                    }
                }
                else if (column is SQLReal)
                {
                    SQLReal sql = (SQLReal)column;
                    float value = (float)row;

                    createTableString.Append("real");
                    insertIntoString.AppendFormat("{0}", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLUniqueIdentifier)
                {
                    SQLUniqueIdentifier sql = (SQLUniqueIdentifier)column;
                    Guid value = (Guid)row;

                    createTableString.Append("uniqueidentifier");
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLBigInt)
                {
                    SQLBigInt sql = (SQLBigInt)column;
                    long value = (long)row;

                    createTableString.Append("bigint");
                    insertIntoString.AppendFormat("{0}", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLDateTime)
                {
                    SQLDateTime sql = (SQLDateTime)column;
                    DateTime value = (DateTime)row;

                    createTableString.Append("datetime");
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLDateTime2)
                {
                    SQLDateTime2 sql = (SQLDateTime2)column;
                    DateTime value = (DateTime)row;

                    createTableString.Append("datetime2");
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLDate)
                {
                    SQLDate sql = (SQLDate)column;
                    DateTime value = (DateTime)row;

                    createTableString.Append("date");
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else if (column is SQLTime)
                {
                    SQLTime sql = (SQLTime)column;
                    DateTime value = (DateTime)row;

                    createTableString.Append("time");
                    insertIntoString.AppendFormat("'{0}'", value);

                    sql.Write(writer, value);
                }
                else
                {
                    System.Diagnostics.Trace.Assert(false);
                }

                if (i < rows.Count() - 1)
                {
                    createTableString.AppendLine(",");
                    insertIntoString.AppendLine(",");
                }
            }



            createTableString.Append(")");
            insertIntoString.Append(")");

            string server = "localhost";
            string username = "sa";
            string password = "Password01";
            string database = "BCPTest";

            string connectionString = string.Format(
                "Data Source={0};User ID={1};Password={2};Initial Catalog={3}",
                server, username, password, database
            );

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES";
                ArrayList result = new ArrayList();
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader.GetString(0));
                }
                reader.Close();
                reader.Dispose();

                command.CommandText = "IF OBJECT_ID('BCPTest','U') IS NOT NULL DROP TABLE BCPTest";
                command.ExecuteNonQuery();

                command.CommandText = createTableString.ToString();
                command.ExecuteNonQuery();

                command.CommandText = insertIntoString.ToString();
                command.ExecuteNonQuery();

                connection.Close();
            }




            //Don't do that, user must do it
            //writer.Close();
        }
    }
}
