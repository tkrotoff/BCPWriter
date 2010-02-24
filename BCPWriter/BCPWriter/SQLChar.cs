using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    public class SQLChar : IBCPSerialization
    {
        public static readonly string SPACE = " ";

        private string _text;
        private ushort _length;

        public SQLChar(string text, ushort length)
        {
            System.Diagnostics.Trace.Assert(text.Length <= length);

            _text = text;
            _length = length;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //ushort is 2 bytes long
            writer.Write(_length);

            //Append spaces if needed
            StringBuilder tmp = new StringBuilder(_text);
            while (tmp.Length < _length)
            {
                tmp.Append(SPACE);
            }
            ////

            //Text should be in ascii
            byte[] asciiText = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(tmp.ToString()));
            writer.Write(asciiText);
        }
    }
}
