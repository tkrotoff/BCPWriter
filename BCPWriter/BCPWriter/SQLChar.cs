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
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
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

        public byte[] ToBCP(string text)
        {
            if (text.Length > _length)
            {
                throw new ArgumentException("text is longer than the length declared inside the constructor");
            }

            //ushort is 2 bytes long
            byte[] sizeBytes = BitConverter.GetBytes(_length);

            //Append spaces if needed
            StringBuilder tmp = new StringBuilder(text);
            while (tmp.Length < _length)
            {
                tmp.Append(SPACE);
            }
            ////

            return SQLInt.ConcatByteArrays(sizeBytes, EncodeToOEMCodePage(tmp.ToString()));
        }

        /// <summary>
        /// Encode text using OEM code page, see http://en.wikipedia.org/wiki/Windows_code_page
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] EncodeToOEMCodePage(string text)
        {
            Encoding enc = Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            return enc.GetBytes(text);
        }
    }
}
