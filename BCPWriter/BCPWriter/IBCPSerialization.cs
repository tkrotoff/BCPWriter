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
        /// <summary>
        /// Writes a SQL value to a binary stream.
        /// </summary>
        /// <remarks>
        /// All classes inheriting this interface should also inherit the C# comments in a future version
        /// of Visual Studio cf:
        /// <a href="http://connect.microsoft.com/VisualStudio/feedback/details/367384/inherit-c-xml-comment-from-the-base-class-or-an-interface">Inherit C# xml comment from the base class or an interface</a>
        /// Maybe, one day...
        /// </remarks>
        /// <param name="writer">BinaryWriter</param>
        /// <param name="value">SQL value</param>
        void Write(BinaryWriter writer, object value);
    }
}
