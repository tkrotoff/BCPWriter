using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL varbinary(max).
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLBinary</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms188362.aspx">binary and varbinary (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLVarBinaryMax : IBCPSerialization
    {
        byte[] _data;

        public static readonly int MAX = SQLInt.MAX_VALUE;
        //public static readonly int MAX_UNICODE = 2 ^ 30 - 1;

        /// <summary>
        /// Constructs a SQL varbinary(max).
        /// </summary>
        /// <param name="data">binary data</param>
        public SQLVarBinaryMax(byte[] data)
        {
            System.Diagnostics.Trace.Assert(data.Length <= MAX);

            _data = data;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //ulong is 8 bytes long
            ulong length = (ulong)_data.Length;
            writer.Write(length);

            string hex = SQLBinary.ToHexString(_data);
            byte[] hexBytes = SQLBinary.StringToByteArray(hex);

            writer.Write(hexBytes.ToArray());
        }
    }
}
