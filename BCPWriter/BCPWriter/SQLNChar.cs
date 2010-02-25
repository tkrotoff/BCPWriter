using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL nchar and nvarchar.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLChar</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms186939.aspx">nchar and nvarchar (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// Character data types that are either fixed-length, nchar, or variable-length, nvarchar,<br/>
    /// Unicode data and use the UNICODE UCS-2 character set.<br/>
    /// <br/>
    /// When n is not specified in a data definition or variable declaration statement, the default length is 1.<br/>
    /// When n is not specified with the CAST function, the default length is 30.<br/>
    /// Use nchar when the sizes of the column data entries are probably going to be similar.<br/>
    /// Use nvarchar when the sizes of the column data entries are probably going to vary considerably.<br/>
    /// </remarks>
    public class SQLNChar : IBCPSerialization
    {
        private string _text;
        private ushort _length;

        public SQLNChar(string text, ushort length)
        {
            _text = text;
            _length = length;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //ushort is 2 bytes long
            //* 2 because we are in unicode, thus 1 char is 2 bytes long
            short length = (short) (_length * 2);
            writer.Write(length);

            //Append spaces if needed
            StringBuilder tmp = new StringBuilder(_text);
            while (tmp.Length < _length)
            {
                tmp.Append(SQLChar.SPACE);
            }
            ////

            //Text should be in unicode
            writer.Write(Encoding.Unicode.GetBytes(tmp.ToString()));
        }
    }
}
