using System;
using System.Collections.Generic;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL time.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/bb677243.aspx">time (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLTime : IBCPSerialization
    {
        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (DateTime?)value);
        }

        public static void Write(BinaryWriter writer, DateTime? time)
        {
            if (!time.HasValue)
            {
                //1 byte long
                byte[] nullBytes = { 255 };
                writer.Write(nullBytes);
                return;
            }

            //byte is 1 byte long :)
            const byte size = 5;
            writer.Write(size);

            //Format:
            //00:00:00 gives 05 00 00 00 00 00
            //00:00:01 gives 05 80 96 98 00 00 -> 00 00 98 96 80 =    1.0000000
            //00:01:00 gives 05 00 46 C3 23 00 -> 00 23 C3 46 00 =   60.0000000
            //01:00:00 gives 05 00 68 C4 61 08 -> 08 61 C4 68 00 = 3600.0000000

            DateTime initTime = DateTime.Parse("00:00:00", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan span = time.Value - initTime;

            byte[] valueBytes = BitConverter.GetBytes((long)(span.TotalSeconds * 10000000));
            List<byte> bytes = new List<byte>(valueBytes);
            bytes.RemoveAt(7);
            bytes.RemoveAt(6);
            bytes.RemoveAt(5);
            writer.Write(bytes.ToArray());
        }
    }
}
