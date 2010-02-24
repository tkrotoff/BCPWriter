using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    class SQLNChar : IBCPSerialization
    {
        private string _text;
        private short _length;

        public SQLNChar(string text, short length)
        {
            _text = text;
            _length = length;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //Short is 2 bytes long
            //* 2 because we are in unicode, thus 1 char is 2 bytes long
            short length = (short) (_length * 2);
            writer.Write(length);

            //Append spaces if needed
            StringBuilder tmp = new StringBuilder(_text);
            while (tmp.Length < _length)
            {
                tmp.Append(SQLChar.SPACE);
            }
            ////

            //Text should be in unicode
            writer.Write(Encoding.Unicode.GetBytes(tmp.ToString()));
        }
    }
}
