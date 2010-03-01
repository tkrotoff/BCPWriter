using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL nvarchar.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLNChar</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms176089.aspx">char and varchar (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLNVarChar : IBCPSerialization
    {
        private uint _length;

        public const uint MAX = (uint)SQLInt.MAX_VALUE;

        /// <summary>
        /// Constructs a SQL nvarchar.
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 4,000.
        /// The storage size is two times n bytes.
        /// length can also be MAX.
        /// </param>
        public SQLNVarChar(uint length)
        {
            if (length != MAX)
            {
                //Can be a value from 1 through 4,000
                System.Diagnostics.Trace.Assert(length >= SQLNChar.MIN_LENGTH);
                System.Diagnostics.Trace.Assert(length <= SQLNChar.MAX_LENGTH);
            }

            _length = length;
        }

        public byte[] ToBCP(string text)
        {
            System.Diagnostics.Trace.Assert(text.Length <= _length);

            byte[] sizeBytes = null;
            if (_length == MAX)
            {
                //ulong is 8 bytes long
                //* 2 because we are in unicode, thus 1 char is 2 bytes long
                sizeBytes = BitConverter.GetBytes((ulong)(text.Length * 2));
            }
            else
            {
                //ushort is 2 bytes long
                //* 2 because we are in unicode, thus 1 char is 2 bytes long
                sizeBytes = BitConverter.GetBytes((ushort)(text.Length * 2));
            }

            //Text should be in unicode
            return SQLInt.ConcatByteArrays(sizeBytes, Encoding.Unicode.GetBytes(text));
        }
    }
}
