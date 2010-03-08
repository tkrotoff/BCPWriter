using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL nchar.
    /// </summary>
    /// 
    /// <remarks>
    /// <see>SQLChar</see>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms186939.aspx">nchar and nvarchar (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// Character data types that are either fixed-length, nchar, or variable-length, nvarchar,<br/>
    /// Unicode data and use the UNICODE UCS-2 character set.<br/>
    /// <br/>
    /// When n is not specified in a data definition or variable declaration statement, the default length is 1.<br/>
    /// When n is not specified with the CAST function, the default length is 30.<br/>
    /// Use nchar when the sizes of the column data entries are probably going to be similar.<br/>
    /// Use nvarchar when the sizes of the column data entries are probably going to vary considerably.<br/>
    /// </remarks>
    public class SQLNChar : IBCPSerialization
    {
        private ushort _length;

        public const ushort MIN_LENGTH = 1;
        public const ushort MAX_LENGTH = 4000;

        /// <summary>
        /// Constructs a SQL nchar.
        /// </summary>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 4,000.
        /// The storage size is two times n bytes.
        /// </param>
        public SQLNChar(ushort length)
        {
            //Can be a value from 1 through 4,000
            if (length < MIN_LENGTH || length > MAX_LENGTH)
            {
                throw new ArgumentException("length should be between 1 and 4,000");
            }

            _length = length;
        }

        public ushort Length
        {
            get { return _length; }
        }

        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (string)value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">text</param>
        /// <returns></returns>
        public void Write(BinaryWriter writer, string text)
        {
            if (text == null)
            {
                //8 bytes long
                byte[] nullBytes = { 255, 255 };
                writer.Write(nullBytes);
                return;
            }

            if (text.Length > _length)
            {
                throw new ArgumentException("text is longer than the length declared inside the constructor");
            }

            //ushort is 2 bytes long
            //* 2 because we are in UTF-16, thus 1 char is 2 bytes long
            short length = (short) (_length * 2);
            writer.Write(length);

            //Append spaces if needed
            StringBuilder tmp = new StringBuilder(text);
            while (tmp.Length < _length)
            {
                tmp.Append(SQLChar.SPACE);
            }
            ////

            //Text should be in unicode UTF-16
            writer.Write(Encoding.Unicode.GetBytes(tmp.ToString()));
        }
    }
}
