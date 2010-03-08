using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL date.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/bb630352.aspx">date (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLDate : IBCPSerialization
    {
        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (DateTime?)value);
        }

        public void Write(BinaryWriter writer, DateTime? date)
        {
            if (!date.HasValue)
            {
                //1 byte long
                byte[] nullBytes = { 255 };
                writer.Write(nullBytes);
                return;
            }

            //byte is 1 byte long :)
            byte size = 3;
            writer.Write(size);

            //Format is the number of days since 0001-01-01
            //0001-01-01 gives 03 00 00 00
            //0001-01-02 gives 03 01 00 00 -> 00 00 01 = 1 day
            //0001-02-01 gives 03 1F 00 00 -> 00 00 1F = 31 days
            //0001-02-02 gives 03 20 00 00 -> 00 00 20 = 32 days
            //0001-09-30 gives 03 10 01 00 -> 00 01 10 = 272 days
            //1000-01-01 gives 03 4D 91 05 -> 05 91 4D = 364 877 days

            DateTime initDate = DateTime.Parse("0001-01-01", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan span = date.Value - initDate;

            //3 bytes long
            byte[] valueBytes = BitConverter.GetBytes((int)span.TotalDays);
            List<byte> bytes = new List<byte>(valueBytes);
            bytes.RemoveAt(3);
            writer.Write(bytes.ToArray());
        }
    }
}
