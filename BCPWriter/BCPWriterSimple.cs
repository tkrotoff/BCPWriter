﻿namespace BCPWriter
{
    using System;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Simple API for BCPWriter.
    /// </summary>
    /// 
    /// <remarks>
    /// This API is not recommended for C# application (use BCPWriter instead).
    /// This API was designed for easy integration with scripts (PowerShell).
    /// See BCPWriterSnapIn for the BCPWriterSimple PowerShell snap-in.
    ///
    /// <example>
    /// PowerShell script using BCPWriterSimple API:
    /// <code>
    /// [Reflection.Assembly]::LoadFrom("BCPWriter\bin\Release\BCPWriter.dll");
    ///
    /// $bcpFileName = "data.bcp";
    /// $bcp = new-object BCPWriter.BCPWriterSimple($bcpFileName);
    ///
    /// $bcp.WriteNVarChar("Frédéric François", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteNVarChar("Chopin", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteInt(1810);
    /// $bcp.WriteInt(1849);
    ///
    /// $bcp.WriteNVarChar("Franz", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteNVarChar("Liszt", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteInt(1811);
    /// $bcp.WriteInt(1886);
    ///
    /// $bcp.WriteNVarChar("George", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteNVarChar("Sand", [BCPWriter.SQLNVarChar]::MAX);
    /// $bcp.WriteInt(1804);
    /// $bcp.WriteInt(1876);
    ///
    /// $bcp.Close();
    ///
    /// $table = "[BCPTest].[dbo].[BCPTest]";
    /// $servername = "localhost";
    /// $username = "sa";
    /// $password = "Password01";
    ///
    /// # Calls bcp and inserts rows inside the table
    /// bcp $table in $bcpFileName -n -S $servername -U $username -P $password
    /// </code>
    /// </example>
    /// </remarks>
    /// <see cref="BCPWriter"/>
    public class BCPWriterSimple : IBCPWriterSimple
    {
        private readonly BinaryWriter _writer = null;

        /// <summary>
        /// Constructs a BCPWriterStatic given a bcp file name.
        /// </summary>
        /// <param name="bcpFileName">The new bcp file to create.</param>
        public BCPWriterSimple(string bcpFileName)
            : this(new BinaryWriter(new FileStream(bcpFileName, FileMode.Create)))
        {
        }

        /// <summary>
        /// Constructs a BCPWriterStatic given a BinaryWriter.
        /// </summary>
        public BCPWriterSimple(BinaryWriter writer)
        {
            _writer = writer;
        }

        /// <summary>
        /// Closes the BinaryWriter.
        /// </summary>
        public void Close()
        {
            _writer.Close();
        }

        /// <summary>
        /// Writes a SQL bigint.
        /// </summary>
        public void WriteBigInt(long? value)
        {
            SQLBigInt.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL binary.
        /// </summary>
        public void WriteBinary(byte[] value, ushort length)
        {
            SQLBinary.Write(_writer, value, length);
        }

        /// <summary>
        /// Writes a SQL char.
        /// </summary>
        public void WriteChar(string text, ushort length)
        {
            SQLChar.Write(_writer, text, length);
        }

        /// <summary>
        /// Writes a SQL date.
        /// </summary>
        public void WriteDate(DateTime? value)
        {
            SQLDate.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL datetime.
        /// </summary>
        public void WriteDateTime(DateTime? value)
        {
            SQLDateTime.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL datetime2.
        /// </summary>
        public void WriteDateTime2(DateTime? value)
        {
            SQLDateTime2.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL float.
        /// </summary>
        public void WriteFloat(float? value, ushort nbBits)
        {
            SQLFloat.Write(_writer, value, nbBits);
        }

        /// <summary>
        /// Writes a SQL float.
        /// </summary>
        public void WriteFloat(double? value, ushort nbBits)
        {
            SQLFloat.Write(_writer, value, nbBits);
        }

        /// <summary>
        /// Writes a SQL float.
        /// </summary>
        public void WriteFloat(double? value)
        {
            SQLFloat.Write(_writer, value, SQLFloat.MAX_NBBITS);
        }

        /// <summary>
        /// Writes a SQL int.
        /// </summary>
        public void WriteInt(int? value)
        {
            SQLInt.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL nchar.
        /// </summary>
        public void WriteNChar(string text, ushort length)
        {
            SQLNChar.Write(_writer, text, length);
        }

        /// <summary>
        /// Writes a SQL ntext.
        /// </summary>
        public void WriteNText(string text)
        {
            SQLNText.Write(_writer, text);
        }

        /// <summary>
        /// Writes a SQL nvarchar.
        /// </summary>
        public void WriteNVarChar(string text, uint length)
        {
            SQLNVarChar.Write(_writer, text, length);
        }

        /// <summary>
        /// Writes a SQL real.
        /// </summary>
        public void WriteReal(float? value)
        {
            SQLReal.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL text.
        /// </summary>
        public void WriteText(string text)
        {
            SQLText.Write(_writer, text);
        }

        /// <summary>
        /// Writes a SQL time.
        /// </summary>
        public void WriteTime(DateTime? value)
        {
            SQLTime.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL uniqueidentifier.
        /// </summary>
        public void WriteUniqueIdentifier(Guid? value)
        {
            SQLUniqueIdentifier.Write(_writer, value);
        }

        /// <summary>
        /// Writes a SQL varbinary.
        /// </summary>
        public void WriteVarBinary(byte[] value, uint length)
        {
            SQLVarBinary.Write(_writer, value, length);
        }

        /// <summary>
        /// Writes a SQL varchar.
        /// </summary>
        public void WriteVarChar(string text, uint length)
        {
            SQLVarChar.Write(_writer, text, length);
        }

        /// <summary>
        /// Writes a SQL varchar.
        /// </summary>
        public void WriteXML(XmlDocument xml)
        {
            SQLXml.Write(_writer, xml);
        }
    }
}
