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

        private string _bcpFileName = string.Empty;

        public BCPWriter(string bcpFileName)
        {
            if (string.IsNullOrEmpty(bcpFileName))
            {
                throw new ArgumentException("Empty bcp file name");
            }

            _bcpFileName = bcpFileName;
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

            FileStream stream = new FileStream(_bcpFileName, FileMode.Create);

            BinaryWriter writer = new BinaryWriter(stream);

            for (int i = 0; i < rows.Count(); i++)
            {
                IBCPSerialization column = _columns[i % _columns.Count()];
                object row = rows.ElementAt(i);

                if (column is SQLBinary)
                {
                    writer.Write(((SQLBinary)column).ToBCP((byte[])row));
                }
                else if (column is SQLChar)
                {
                    writer.Write(((SQLChar)column).ToBCP((string)row));
                }
                else if (column is SQLInt)
                {
                    writer.Write(((SQLInt)column).ToBCP((int)row));
                }
                else if (column is SQLNChar)
                {
                    writer.Write(((SQLNChar)column).ToBCP((string)row));
                }
                else if (column is SQLNVarChar)
                {
                    writer.Write(((SQLNVarChar)column).ToBCP((string)row));
                }
                else if (column is SQLVarBinary)
                {
                    writer.Write(((SQLVarBinary)column).ToBCP((byte[])row));
                }
                else if (column is SQLVarChar)
                {
                    writer.Write(((SQLVarChar)column).ToBCP((string)row));
                }
                else if (column is SQLXML)
                {
                    writer.Write(((SQLXML)column).ToBCP((string)row));
                }
                else
                {
                    System.Diagnostics.Trace.Assert(false);
                }
            }

            writer.Close();
        }
    }
}
