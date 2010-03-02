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
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// length can also be MAX.
        /// </param>
        public SQLVarBinary(uint length)
        {
            if (length != MAX)
            {
                if (length < SQLChar.MIN_LENGTH || length > SQLChar.MAX_LENGTH)
                {
                    throw new ArgumentException("length should be between 1 and 8,000");
                }
            }

            _length = length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns></returns>
        public byte[] ToBCP(byte[] data)
        {
            if (data.Length > _length)
            {
                throw new ArgumentException("data is longer than the length declared inside the constructor");
            }

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

            string hex = Util.ToHexString(data);
            byte[] hexBytes = Util.HexToByteArray(hex);

            return Util.ConcatByteArrays(sizeBytes, hexBytes);
        }
    }
}
