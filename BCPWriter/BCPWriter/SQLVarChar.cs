using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL varchar.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLChar</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms176089.aspx">char and varchar (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLVarChar : IBCPSerialization
    {
        private uint _length;

        public const uint MAX = (uint)SQLInt.MAX_VALUE;

        /// <summary>
        /// Constructs a SQL varchar.
        /// </summary>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// </param>
        public SQLVarChar(uint length)
        {
            if (length != MAX)
            {
                if (length < SQLBinary.MIN_LENGTH || length > SQLBinary.MAX_LENGTH)
                {
                    throw new ArgumentException("length should be between 1 and 8,000");
                }
            }

            _length = length;
        }

        public byte[] ToBCP(string text)
        {
            if (text.Length > _length)
            {
                throw new ArgumentException("text is longer than the length declared inside the constructor");
            }

            byte[] sizeBytes = null;
            if (_length == MAX)
            {
                //ulong is 8 bytes long
                sizeBytes = BitConverter.GetBytes((ulong)text.Length);
            }
            else
            {
                //ushort is 2 bytes long
                sizeBytes = BitConverter.GetBytes((ushort)text.Length);
            }

            return SQLInt.ConcatByteArrays(sizeBytes, SQLChar.EncodeToOEMCodePage(text));
        }
    }
}
