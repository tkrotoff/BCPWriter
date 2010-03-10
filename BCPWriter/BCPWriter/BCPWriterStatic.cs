using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace BCPWriter
{
    public class BCPWriterStatic
    {
        private BinaryWriter _writer = null;

        public BCPWriterStatic(BinaryWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// Writes a SQL bigint.
        /// </summary>
        /// <param name="value"></param>
        public void WriteBigInt(long? value)
        {
            SQLBigInt.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL binary.
        /// </summary>
        /// <param name="_writer"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public void WriteBinary(byte[] value, ushort length)
        {
            SQLBinary.Write(_writer, value, length);
        }

        /// <summary>
        /// Writes a SQL char.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        public void WriteChar(string text, ushort length)
        {
            SQLChar.Write(_writer, text, length);
        }

        /// <summary>
        /// Writes a SQL date.
        /// </summary>
        /// <param name="value"></param>
        public void WriteDate(DateTime? value)
        {
            SQLDate.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL datetime.
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime(DateTime? value)
        {
            SQLDateTime.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL datetime2.
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime2(DateTime? value)
        {
            SQLDateTime2.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL float.
        /// </summary>
        /// <param name="value"></param>
        public void WriteFloat(float? value, ushort nbBits)
        {
            SQLFloat.Write(_writer, value, nbBits);
        }

        /// <summary>
        /// Writes a SQL float.
        /// </summary>
        /// <param name="value"></param>
        public void WriteFloat(double? value, ushort nbBits)
        {
            SQLFloat.Write(_writer, value, nbBits);
        }

        /// <summary>
        /// Writes a SQL float.
        /// </summary>
        /// <param name="value"></param>
        public void WriteFloat(double? value)
        {
            SQLFloat.Write(_writer, value, SQLFloat.MAX_NBBITS);
        }

        /// <summary>
        /// Writes a SQL int.
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt(int? value)
        {
            SQLInt.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL nchar.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        public void WriteNChar(string text, ushort length)
        {
            SQLNChar.Write(_writer, text, length);
        }

        /// <summary>
        /// Writes a SQL ntext.
        /// </summary>
        /// <param name="text"></param>
        public void WriteNText(string text)
        {
            SQLNText.Write(_writer, text);
        }

        /// <summary>
        /// Writes a SQL nvarchar.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        public void WriteNVarChar(string text, uint length)
        {
            SQLNVarChar.Write(_writer, text, length);
        }

        /// <summary>
        /// Writes a SQL real.
        /// </summary>
        /// <param name="value"></param>
        public void WriteReal(float? value)
        {
            SQLReal.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL text.
        /// </summary>
        /// <param name="text"></param>
        public void WriteText(string text)
        {
            SQLText.Write(_writer, text);
        }

        /// <summary>
        /// Writes a SQL time.
        /// </summary>
        /// <param name="value"></param>
        public void WriteTime(DateTime? value)
        {
            SQLTime.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL uniqueidentifier.
        /// </summary>
        /// <param name="value"></param>
        public void WriteUniqueIdentifier(Guid? value)
        {
            SQLUniqueIdentifier.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL varbinary.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public void WriteVarBinary(byte[] value, uint length)
        {
            SQLVarBinary.Write(_writer, value, length);
        }

        /// <summary>
        /// Writes a SQL varchar.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        public void WriteVarChar(string text, uint length)
        {
            SQLVarChar.Write(_writer, text, length);
        }

        /// <summary>
        /// Writes a SQL varchar.
        /// </summary>
        /// <param name="xml"></param>
        public void WriteXML(XmlDocument xml)
        {
            SQLXml.Write(_writer, xml);
        }
    }
}
