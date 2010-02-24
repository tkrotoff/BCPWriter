﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// SQL binary and varbinary.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187752.aspx">Data Types (Transact-SQL)</a><br/>
    /// <a href="http://databases.about.com/od/sqlserver/a/mssql_datatypes.htm">Microsoft SQL Server Data Types</a><br/>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms188362.aspx">binary and varbinary (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
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
        byte[] _data;
        ushort _length;

        public static readonly int MAX = SQLChar.MAX;
        //public static readonly int MAX_UNICODE = 2 ^ 30 - 1;

        public static readonly int MIN_LENGTH = SQLChar.MIN_LENGTH;
        public static readonly int MAX_LENGTH = SQLChar.MAX_LENGTH;

        /// <summary>
        /// Constructs a SQL binary or varbinary.
        /// </summary>
        /// <param name="data">binary data</param>
        /// <param name="length">
        /// length of n bytes, where n is a value from 1 through 8,000.
        /// The storage size is n bytes.
        /// length can also be MAX.
        /// </param>
        public SQLBinary(byte[] data, ushort length)
        {
            System.Diagnostics.Trace.Assert(data.Length <= length);

            System.Diagnostics.Trace.Assert(length <= MAX);
            if (length < MAX)
            {
                //Can be a value from 1 through 8,000
                System.Diagnostics.Trace.Assert(length >= MIN_LENGTH);
                System.Diagnostics.Trace.Assert(length <= MAX_LENGTH);
            }

            _data = data;
            _length = length;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //ushort is 2 bytes long
            writer.Write(_length);

            string hex = ToHexString(_data);
            byte[] hexBytes = StringToByteArray(hex);

            //Append 0s if needed
            List<byte> bytes = new List<byte>(hexBytes);
            while (bytes.Count < _length)
            {
                bytes.Add(0);
            }

            writer.Write(bytes.ToArray());
        }

        public static string ToHexString(byte[] data)
        {
            StringBuilder hex = new StringBuilder();
            foreach (byte b in data)
            {
                hex.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "{0:x2}", b);
            }
            return hex.ToString();
        }

        /// <summary>
        /// See <a href="http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa-in-c">How do you convert Byte Array to Hexadecimal String, and vice versa, in C#?</a>
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string hex)
        {
            int nbChars = hex.Length;
            byte[] bytes = new byte[nbChars / 2];
            for (int i = 0; i < nbChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}