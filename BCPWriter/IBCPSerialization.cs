using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    interface IBCPSerialization
    {
        void ToBCPFormat(BinaryWriter writer);
    }
}
