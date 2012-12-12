namespace BCPWriter
{
    using System;

    interface IBCPWriterSimple
    {
        void Close();
        void WriteBigInt(long? value);
        void WriteBinary(byte[] value, ushort length);
        void WriteChar(string text, ushort length);
        void WriteDate(DateTime? value);
        void WriteDateTime(DateTime? value);
        void WriteDateTime2(DateTime? value);
        void WriteFloat(double? value);
        void WriteFloat(double? value, ushort nbBits);
        void WriteFloat(float? value, ushort nbBits);
        void WriteInt(int? value);
        void WriteNChar(string text, ushort length);
        void WriteNText(string text);
        void WriteNVarChar(string text, uint length);
        void WriteReal(float? value);
        void WriteText(string text);
        void WriteTime(DateTime? value);
        void WriteUniqueIdentifier(Guid? value);
        void WriteVarBinary(byte[] value, uint length);
        void WriteVarChar(string text, uint length);
        void WriteXML(System.Xml.XmlDocument xml);
    }
}
