using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL bigint.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLInt</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187745.aspx">int, bigint, smallint, and tinyint (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLBigInt : IBCPSerialization
    {
        public byte[] ToBCP(long? value)
        {
            if (!value.HasValue)
            {
                //9 bytes long
                byte[] nullBytes = { 255, 255, 255, 255, 255, 255, 255, 255, 255 };
                return nullBytes;
            }

            //byte is 1 byte long :)
            byte size = 8;
            byte[] sizeBytes = { size };

            //long is 8 bytes long
            byte[] valueBytes = BitConverter.GetBytes(value.Value);

            return Util.ConcatByteArrays(sizeBytes, valueBytes);
        }
    }
}
