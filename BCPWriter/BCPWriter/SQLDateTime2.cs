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
        public static DateTime MIN_DATETIME2 = new DateTime(0001, 01, 01, 00, 00, 00);

        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (DateTime?)value);
        }

        public void Write(BinaryWriter writer, DateTime? dateTime)
        {
            if (!dateTime.HasValue)
            {
                //1 byte long
                byte[] nullBytes = { 255 };
                writer.Write(nullBytes);
                return;
            }

            if (dateTime < MIN_DATETIME2)
            {
                throw new ArgumentException("dateTime cannot be inferior than 0001-01-01T00:00:00");
            }

            //byte is 1 byte long :)
            byte size = 8;
            writer.Write(size);

            //Format:
            //0001-01-01T00:00:00 gives 08 00 00 00 00 00 00 00 00
            //0001-01-02T00:00:00 gives 08 00 00 00 00 00 01 00 00 -> 00 00 01 = 1 day
            //0001-02-01T00:00:00 gives 08 00 00 00 00 00 1F 00 00 -> 00 00 1F = 31 days
            //0001-02-02T00:00:00 gives 08 00 00 00 00 00 20 00 00 -> 00 00 20 = 32 days
            //0001-09-30T00:00:00 gives 08 00 00 00 00 00 10 01 00 -> 00 01 10 = 272 days
            //2000-01-01T00:00:00 gives 08 00 00 00 00 00 07 24 0B -> 0B 24 07 = 730119 days

            //0001-01-01T00:00:00 gives 08 00 00 00 00 00 00 00 00
            //0001-01-01T00:00:01 gives 08 80 96 98 00 00 00 00 00 -> 00 00 98 96 80 =    10000000
            //0001-01-01T00:01:00 gives 08 00 46 C3 23 00 00 00 00 -> 00 23 C3 46 00 =   600000000
            //0001-01-01T01:00:00 gives 08 00 68 C4 61 08 00 00 00 -> 08 61 C4 68 00 = 36000000000
            //0001-01-01T01:01:01 gives 08 80 44 20 86 08 00 00 00 -> 08 86 20 44 80 = 36610000000

            //0001-01-01T00:00:00.001 gives 08 10 27 00 00 00 00 00 00 -> 00 00 00 27 10 = 10000
            //0001-01-01T00:00:00.002 gives 08 20 4E 00 00 00 00 00 00 -> 00 00 00 4E 20 = 20000
            //0001-01-01T00:00:00.003 gives 08 30 75 00 00 00 00 00 00 -> 00 00 00 75 30 = 30000
            //0001-01-01T00:00:00.999 gives 08 70 6F 98 00 00 00 00 00 -> 00 00 98 6F 70 = 9990000

            //2004-05-23T14:25:10 gives 08 00 67 C5 DC 78 4B 2A 0B  -> 0B 2A 4B = 731723 days
            //                                                      -> 78 DC C5 67 00 = 519100000000

            //Time, 5 bytes long
            DateTime time = DateTime.Parse(dateTime.Value.ToString("HH:mm:ss.fffffff"));
            DateTime initTime = DateTime.Parse("00:00:00", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan span = time - initTime;
            long bcpTimeFormat = (long)(span.TotalMilliseconds * 10000);
            List<byte> timeBytes = new List<byte>(BitConverter.GetBytes(bcpTimeFormat));
            timeBytes.RemoveAt(7);
            timeBytes.RemoveAt(6);
            timeBytes.RemoveAt(5);
            writer.Write(timeBytes.ToArray());

            //Date, 3 bytes long
            DateTime date = dateTime.Value.Date;
            DateTime initDate = DateTime.Parse("0001-01-01", System.Globalization.CultureInfo.InvariantCulture);
            span = date - initDate;
            List<byte> dateBytes = new List<byte>(BitConverter.GetBytes((int)span.TotalDays));
            dateBytes.RemoveAt(3);
            writer.Write(dateBytes.ToArray());
        }
    }
}
