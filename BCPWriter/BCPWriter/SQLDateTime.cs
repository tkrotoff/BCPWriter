using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// Obsolete! SQL datetime.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187819.aspx">datetime (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// Use the time, date, datetime2 and datetimeoffset data types for new work.
    /// These types align with the SQL Standard.
    /// They are more portable.
    /// time, datetime2 and datetimeoffset provide more seconds precision.
    /// datetimeoffset provides time zone support for globally deployed applications.
    /// </remarks>
    [Obsolete]
    public class SQLDateTime : IBCPSerialization
    {
        public byte[] ToBCP(DateTime dateTime)
        {
            //byte is 1 byte long :)
            byte size = 8;
            byte[] sizeBytes = { size };

            //long is 8 bytes long
            byte[] valueBytes = BitConverter.GetBytes(dateTime.ToUniversalTime().ToBinary());

            return Util.ConcatByteArrays(sizeBytes, valueBytes);
        }
    }
}
