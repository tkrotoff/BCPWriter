using System;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL varchar.
    /// </summary>
    /// 
    /// <remarks>
    /// <see cref="SQLChar"/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms176089.aspx">char and varchar (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLVarChar : IBCPSerialization
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
        /// Maximum length allowed for SQL varchar
        /// </summary>
        /// <remarks>
        /// Use this in order to get a SQL varchar(max)
        /// </remarks>
        public const uint MAX = (uint)SQLInt.MAX_VALUE;

        /// <summary>
        /// Constructs a SQL varchar.
        /// </summary>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// </param>
        public SQLVarChar(uint length)
        {
            Length = length;
        }

        /// <summary>
        /// SQL varchar length.
        /// </summary>
        public uint Length
        {
            get;
            private set;
        }

        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (string)value, Length);
        }

        public static void Write(BinaryWriter writer, string text, uint length)
        {
            if (length != MAX)
            {
                if (length < MIN_LENGTH || length > MAX_LENGTH)
                {
                    throw new ArgumentException("length should be between 1 and 8,000");
                }
            }

            if (text == null)
            {
                //8 bytes long
                byte[] nullBytes = { 255, 255, 255, 255, 255, 255, 255, 255 };
                writer.Write(nullBytes);
                return;
            }

            if (text.Length > length)
            {
                throw new ArgumentException("text is longer than the length declared inside the constructor");
            }

            if (length == MAX)
            {
                //ulong is 8 bytes long
                writer.Write((ulong)text.Length);
            }
            else
            {
                //ushort is 2 bytes long
                writer.Write((ushort)text.Length);
            }

            writer.Write(Util.EncodeToOEMCodePage(text));
        }
    }
}
