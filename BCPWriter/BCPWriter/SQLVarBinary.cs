using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL varbinary.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLBinary</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms188362.aspx">binary and varbinary (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLVarBinary : IBCPSerialization
    {
        private uint _length;

        public const uint MAX = (uint)SQLInt.MAX_VALUE;
        //public const int MAX_UNICODE = 2 ^ 30 - 1;

        /// <summary>
        /// Constructs a SQL varbinary.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// length can also be MAX.
        /// </param>
        public SQLVarBinary(uint length)
        {
            if (length != MAX)
            {
                //Can be a value from 1 through 8,000
                System.Diagnostics.Trace.Assert(length >= SQLChar.MIN_LENGTH);
                System.Diagnostics.Trace.Assert(length <= SQLChar.MAX_LENGTH);
            }

            _length = length;
        }

        public byte[] ToBCP(byte[] data)
        {
            System.Diagnostics.Trace.Assert(data.Length <= _length);

            byte[] sizeBytes = null;
            if (_length == MAX)
            {
                //ulong is 8 bytes long
                sizeBytes = BitConverter.GetBytes((ulong)data.Length);
            }
            else
            {
                //ushort is 2 bytes long
                sizeBytes = BitConverter.GetBytes((ushort)data.Length);
            }

            string hex = SQLBinary.ToHexString(data);
            byte[] hexBytes = SQLBinary.HexToByteArray(hex);

            return SQLInt.ConcatByteArrays(sizeBytes, hexBytes);
        }
    }
}
