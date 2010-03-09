using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private uint _length;

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
            if (length != MAX)
            {
                if (length < SQLBinary.MIN_LENGTH || length > SQLBinary.MAX_LENGTH)
                {
                    throw new ArgumentException("length should be between 1 and 8,000");
                }
            }

            _length = length;
        }

        /// <summary>
        /// SQL varchar length.
        /// </summary>
        public uint Length
        {
            get { return _length; }
        }

        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (string)value);
        }

        public void Write(BinaryWriter writer, string text)
        {
            if (text == null)
            {
                //8 bytes long
                byte[] nullBytes = { 255, 255, 255, 255, 255, 255, 255, 255 };
                writer.Write(nullBytes);
                return;
            }

            if (text.Length > _length)
            {
                throw new ArgumentException("text is longer than the length declared inside the constructor");
            }

            if (_length == MAX)
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
