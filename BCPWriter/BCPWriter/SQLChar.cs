using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL char and varchar.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms176089.aspx">char and varchar (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// When n is not specified in a data definition or variable declaration statement,
    /// the default length is 1.<br/>
    /// When n is not specified with the CAST function, the default length is 30.<br/>
    /// <br/>
    /// Use char when the sizes of the column data entries are consistent.<br/>
    /// Use varchar when the sizes of the column data entries vary considerably.<br/>
    /// Use varchar(max) when the sizes of the column data entries vary considerably, and the size might exceed 8,000 bytes.<br/>
    /// </remarks>
    public class SQLChar : IBCPSerialization
    {
        public static readonly string SPACE = " ";

        private string _text;
        private ushort _length;

        public static readonly int MAX = (int) Math.Pow(2, 31) - 1;

        public static readonly int MIN_LENGTH = 1;
        public static readonly int MAX_LENGTH = 8000;

        /// <summary>
        /// Constructs a SQL char and varchar.
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// length can also be MAX.
        /// </param>
        public SQLChar(string text, ushort length)
        {
            System.Diagnostics.Trace.Assert(text.Length <= length);

            System.Diagnostics.Trace.Assert(length <= MAX);
            if (length < MAX)
            {
                //Can be a value from 1 through 8,000
                System.Diagnostics.Trace.Assert(length >= MIN_LENGTH);
                System.Diagnostics.Trace.Assert(length <= MAX_LENGTH);
            }

            _text = text;
            _length = length;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //ushort is 2 bytes long
            writer.Write(_length);

            //Append spaces if needed
            StringBuilder tmp = new StringBuilder(_text);
            while (tmp.Length < _length)
            {
                tmp.Append(SPACE);
            }
            ////

            //Text should be in ascii
            byte[] asciiText = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(tmp.ToString()));
            writer.Write(asciiText);
        }
    }
}
