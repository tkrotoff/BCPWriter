using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    public class BCPWriter
    {
        private List<IBCPSerialization> _columns = new List<IBCPSerialization>();

        private BinaryWriter _writer = null;

        public BCPWriter(BinaryWriter writer)
        {
            _writer = writer;
        }

        public void AddColumn(IBCPSerialization column)
        {
            _columns.Add(column);
        }

        public void AddColumns(IEnumerable<IBCPSerialization> columns)
        {
            _columns.AddRange(columns);
        }

        public void WriteRows(IEnumerable<object> rows)
        {
            if (_columns.Count() == 0)
            {
                throw new ArgumentException("No columns");
            }

            for (int i = 0; i < rows.Count(); i++)
            {
                IBCPSerialization column = _columns[i % _columns.Count()];
                object row = rows.ElementAt(i);

                if (column is SQLBinary)
                {
                    _writer.Write(((SQLBinary)column).ToBCP((byte[])row));
                }
                else if (column is SQLChar)
                {
                    _writer.Write(((SQLChar)column).ToBCP((string)row));
                }
                else if (column is SQLInt)
                {
                    _writer.Write(((SQLInt)column).ToBCP((int)row));
                }
                else if (column is SQLNChar)
                {
                    _writer.Write(((SQLNChar)column).ToBCP((string)row));
                }
                else if (column is SQLNVarChar)
                {
                    _writer.Write(((SQLNVarChar)column).ToBCP((string)row));
                }
                else if (column is SQLVarBinary)
                {
                    _writer.Write(((SQLVarBinary)column).ToBCP((byte[])row));
                }
                else if (column is SQLVarChar)
                {
                    _writer.Write(((SQLVarChar)column).ToBCP((string)row));
                }
                else if (column is SQLXML)
                {
                    _writer.Write(((SQLXML)column).ToBCP((string)row));
                }
                else
                {
                    System.Diagnostics.Trace.Assert(false);
                }
            }
            _writer.Close();
        }
    }
}
