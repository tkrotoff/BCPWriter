using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL datetime2, to be used instead of SQL datetime.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLDateTime</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/bb677335.aspx">datetime2 (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLDateTime2 : IBCPSerialization
    {
        public byte[] ToBCP(DateTime dateTime)
        {
            //byte is 1 byte long :)
            byte size = 8;
            byte[] sizeBytes = { size };

            //long is 8 bytes long
            byte[] valueBytes = BitConverter.GetBytes(dateTime.ToBinary());

            return Util.ConcatByteArrays(sizeBytes, valueBytes);
        }
    }
}
