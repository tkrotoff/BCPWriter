namespace BCPWriter
{
    internal interface IBCPWriter
    {
        void AddColumn(IBCPSerialization column);
        void AddColumns(System.Collections.Generic.IEnumerable<IBCPSerialization> columns);
        void WriteRows(System.IO.BinaryWriter writer, System.Collections.Generic.IEnumerable<object> rows);
    }
}
