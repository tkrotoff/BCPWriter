using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL varchar(max).
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLChar</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms176089.aspx">char and varchar (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLVarCharMax : IBCPSerialization
    {
        private string _text;

        public static readonly int MAX = SQLInt.MAX_VALUE;

        /// <summary>
        /// Constructs a SQL varchar(max).
        /// </summary>
        /// <param name="text">text</param>
        public SQLVarCharMax(string text)
        {
            System.Diagnostics.Trace.Assert(text.Length <= MAX);

            _text = text;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //ulong is 8 bytes long
            ulong length = (ulong) _text.Length;
            writer.Write(length);

            //Text should be in ascii
            byte[] asciiText = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(_text.ToString()));
            writer.Write(asciiText);
        }
    }
}
