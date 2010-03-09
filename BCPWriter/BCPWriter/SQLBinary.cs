using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL binary.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms188362.aspx">binary and varbinary (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// Fixed-length binary data with a length of n bytes, where n is a value from 1 through 8,000.<br/>
    /// The storage size is n bytes.<br/>
    /// <br/>
    /// When n is not specified in a data definition or variable declaration statement,
    /// the default length is 1.<br/>
    /// When n is not specified with the CAST function, the default length is 30.<br/>
    /// <br/>
    /// Use binary when the sizes of the column data entries are consistent.<br/>
    /// Use varbinary when the sizes of the column data entries vary considerably.<br/>
    /// Use varbinary(max) when the column data entries exceed 8,000 bytes.<br/>
    /// </remarks>
    public class SQLBinary : IBCPSerialization
    {
        /// <summary>
        /// Minimum length allowed for SQL char.
        /// </summary>
        public const ushort MIN_LENGTH = SQLChar.MIN_LENGTH;

        /// <summary>
        /// Maximum length allowed for SQL char.
        /// </summary>
        public const ushort MAX_LENGTH = SQLChar.MAX_LENGTH;

        /// <summary>
        /// Constructs a SQL binary or varbinary.
        /// </summary>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// </param>
        public SQLBinary(ushort length)
        {
            //Can be a value from 1 through 8,000
            if (length < MIN_LENGTH || length > MAX_LENGTH)
            {
                throw new ArgumentException("length should be between 1 and 8,000");
            }

            Length = length;
        }

        /// <summary>
        /// SQL binary length.
        /// </summary>
        public ushort Length
        {
            get;
            private set;
        }

        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (byte[])value);
        }

        public void Write(BinaryWriter writer, byte[] data)
        {
            if (data == null)
            {
                //2 bytes long
                byte[] nullBytes = { 255, 255 };
                writer.Write(nullBytes);
                return;
            }

            if (data.Length > Length)
            {
                throw new ArgumentException("data is longer than the length declared inside the constructor");
            }

            //ushort is 2 bytes long
            writer.Write(Length);

            string hex = Util.ToHexString(data);
            byte[] hexBytes = Util.HexToByteArray(hex);

            //Append 0s if needed
            List<byte> bytes = new List<byte>(hexBytes);
            while (bytes.Count < Length)
            {
                bytes.Add(0);
            }

            writer.Write(bytes.ToArray());
        }
    }
}
