using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    class SQLInt : IBCPSerialization
    {
        int _value;

        public SQLInt(int value)
        {
            _value = value;
        }

        public void ToBCP(BinaryWriter writer)
        {
            //Byte is 1 byte long :)
            byte size = 4;
            writer.Write(size);

            //Int is 4 bytes long
            writer.Write(_value);
        }
    }
}
