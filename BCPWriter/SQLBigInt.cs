﻿namespace BCPWriter
{
    using System.IO;

    /// <summary>
    /// SQL bigint.
    /// </summary>
    /// 
    /// <remarks>
    /// <see cref="SQLInt"/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187745.aspx">int, bigint, smallint, and tinyint (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLBigInt : IBCPSerialization
    {
        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (long?)value);
        }

        public static void Write(BinaryWriter writer, long? value)
        {
            if (!value.HasValue)
            {
                // 1 byte long
                byte[] nullBytes = { 255 };
                writer.Write(nullBytes);
                return;
            }

            // byte is 1 byte long :)
            const byte size = 8;
            writer.Write(size);

            // long is 8 bytes long
            writer.Write(value.Value);
        }
    }
}
