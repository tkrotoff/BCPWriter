using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL varbinary.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLBinary</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms188362.aspx">binary and varbinary (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLVarBinary : IBCPSerialization
    {
        private uint _length;

        /// <summary>
        /// Maximum length allowed for SQL varbinary
        /// </summary>
        /// <remarks>
        /// Use this in order to get a SQL varbinary(max)
        /// </remarks>
        public const uint MAX = (uint)SQLInt.MAX_VALUE;

        /// <summary>
        /// Constructs a SQL varbinary.
        /// </summary>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// length can also be MAX.
        /// </param>
        public SQLVarBinary(uint length)
        {
            if (length != MAX)
            {
                if (length < SQLChar.MIN_LENGTH || length > SQLChar.MAX_LENGTH)
                {
                    throw new ArgumentException("length should be between 1 and 8,000");
                }
            }

            _length = length;
        }

        public uint Length
        {
            get { return _length; }
        }

        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (byte[])value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">binary data</param>
        /// <returns></returns>
        public void Write(BinaryWriter writer, byte[] data)
        {
            if (data == null)
            {
                if (_length == MAX)
                {
                    //8 bytes long
                    byte[] nullBytes = { 255, 255, 255, 255, 255, 255, 255, 255 };
                    writer.Write(nullBytes);
                }
                else
                {
                    //2 bytes long
                    byte[] nullBytes = { 255, 255 };
                    writer.Write(nullBytes);
                }
                return;
            }

            if (data.Length > _length)
            {
                throw new ArgumentException("data is longer than the length declared inside the constructor");
            }

            if (_length == MAX)
            {
                //ulong is 8 bytes long
                writer.Write((ulong)data.Length);
            }
            else
            {
                //ushort is 2 bytes long
                writer.Write((ushort)data.Length);
            }

            string hex = Util.ToHexString(data);
            byte[] hexBytes = Util.HexToByteArray(hex);

            writer.Write(hexBytes);
        }
    }
}
