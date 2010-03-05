using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BCPWriter
{
    /// <summary>
    /// Interface to serialize SQL data types to the bcp format.
    /// </summary>
    public interface IBCPSerialization
    {
        void Write(BinaryWriter writer, object value);
    }
}
