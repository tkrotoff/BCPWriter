using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL nvarchar.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLNChar</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms176089.aspx">char and varchar (Transact-SQL)</a><br/>
    /// </remarks>
    public class SQLNVarChar : IBCPSerialization
    {
        private uint _length;

        public const uint MAX = (uint)SQLInt.MAX_VALUE;

        /// <summary>
        /// Constructs a SQL nvarchar.
        /// </summary>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 4,000.
        /// The storage size is two times n bytes.
        /// length can also be MAX.
        /// </param>
        public SQLNVarChar(uint length)
        {
            if (length != MAX)
            {
                if (length < SQLNChar.MIN_LENGTH || length > SQLNChar.MAX_LENGTH)
                {
                    throw new ArgumentException("length should be between 1 and 4,000");
                }
            }

            _length = length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">text</param>
        /// <returns></returns>
        public byte[] ToBCP(string text)
        {
            if (text == null)
            {
                if (_length == MAX)
                {
                    //8 bytes long
                    byte[] nullBytes = { 255, 255, 255, 255, 255, 255, 255, 255 };
                    return nullBytes;
                }
                else
                {
                    //2 bytes long
                    byte[] nullBytes = { 255, 255 };
                    return nullBytes;
                }
            }

            if (text.Length > _length)
            {
                throw new ArgumentException("text is longer than the length declared inside the constructor");
            }

            List<byte> textBytes = new List<byte>(Encoding.Unicode.GetBytes(text));

            if (_length == MAX)
            {
                //4 bytes: position of the next bytes to read
                //00 00 03 FC = 1020
                const int nextPosition = 1020;
                byte[] nextPositionBytes = BitConverter.GetBytes(nextPosition);

                if (textBytes.Count <= nextPosition)
                {
                    //ulong is 8 bytes long
                    ulong textLength = (ulong)textBytes.Count;
                    byte[] sizeBytes = BitConverter.GetBytes(textLength);

                    return Util.ConcatByteArrays(sizeBytes, textBytes.ToArray());
                }
                else
                {
                    int nbAnchors = textBytes.Count / nextPosition;
                    int modulo = textBytes.Count % nextPosition;

                    int position = 0;
                    for (int i = 0; i < nbAnchors; )
                    {
                        textBytes.InsertRange(position, nextPositionBytes);
                        i++;
                        position = i * nextPosition;
                        position += i * nextPositionBytes.Count();
                    }
                    textBytes.InsertRange(position, BitConverter.GetBytes(modulo));

                    //Start: 8 bytes: FE FF FF FF FF FF FF FF
                    byte[] start = { 254, 255, 255, 255, 255, 255, 255, 255 };
                    textBytes.InsertRange(0, start);

                    //End: 4 bytes: 00 00 00 00
                    byte[] end = { 0, 0, 0, 0 };
                    textBytes.AddRange(end);

                    return textBytes.ToArray();
                }
            }
            else
            {
                //ushort is 2 bytes long
                ushort textLength = (ushort)textBytes.Count;
                byte[] sizeBytes = BitConverter.GetBytes(textLength);

                return Util.ConcatByteArrays(sizeBytes, Encoding.Unicode.GetBytes(text));
            }
        }
    }
}
