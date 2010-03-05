using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL char.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms176089.aspx">char and varchar (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// When n is not specified in a data definition or variable declaration statement,
    /// the default length is 1.<br/>
    /// When n is not specified with the CAST function, the default length is 30.<br/>
    /// <br/>
    /// Use char when the sizes of the column data entries are consistent.<br/>
    /// Use varchar when the sizes of the column data entries vary considerably.<br/>
    /// Use varchar(max) when the sizes of the column data entries vary considerably, and the size might exceed 8,000 bytes.<br/>
    /// </remarks>
    public class SQLChar : IBCPSerialization
    {
        private ushort _length;

        public const char SPACE = ' ';

        public const ushort MIN_LENGTH = 1;
        public const ushort MAX_LENGTH = 8000;

        /// <summary>
        /// Constructs a SQL char.
        /// </summary>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// </param>
        public SQLChar(ushort length)
        {
            //Can be a value from 1 through 8,000
            if (length < MIN_LENGTH || length > MAX_LENGTH)
            {
                throw new ArgumentException("length should be between 1 and 8,000");
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

        public void Write(BinaryWriter writer, string text)
        {
            if (text == null)
            {
                //2 bytes long
                byte[] nullBytes = { 255, 255 };
                writer.Write(nullBytes);
                return;
            }

            if (text.Length > _length)
            {
                throw new ArgumentException("text is longer than the length declared inside the constructor");
            }

            //ushort is 2 bytes long
            writer.Write(_length);

            //Append spaces if needed
            StringBuilder tmp = new StringBuilder(text);
            while (tmp.Length < _length)
            {
                tmp.Append(SPACE);
            }
            ////

            writer.Write(Util.EncodeToOEMCodePage(tmp.ToString()));
        }
    }
}
