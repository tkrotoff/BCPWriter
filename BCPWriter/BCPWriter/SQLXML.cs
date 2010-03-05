using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace BCPWriter
{
    /// <summary>
    /// SQL xml.
    /// </summary>
    /// 
    /// <remarks>
    /// <a href="http://msdn.microsoft.com/en-us/library/ms187339.aspx">xml (Transact-SQL)</a><br/>
    /// <br/>
    /// From SQL Server 2008 Books Online:<br/>
    /// <br/>
    /// CONTENT<br/>
    /// Restricts the xml instance to be a well-formed XML fragment.<br/>
    /// The XML data can contain multiple zero or more elements at the top level.<br/>
    /// Text nodes are also allowed at the top level.<br/>
    /// This is the default behavior.<br/>
    /// <br/>
    /// DOCUMENT<br/>
    /// Restricts the xml instance to be a well-formed XML document.<br/>
    /// The XML data must have one and only one root element.<br/>
    /// Text nodes are not allowed at the top level.<br/>
    /// </remarks>
    public class SQLXML : IBCPSerialization
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns></returns>
        public byte[] ToBCP(XmlDocument xml)
        {
            if (xml == null)
            {
                //8 bytes long
                byte[] nullBytes = { 255, 255, 255, 255, 255, 255, 255, 255 };
                return nullBytes;
            }

            string tmp = xml.DocumentElement.OuterXml;
            //System.Xml.XmlDocument does not give the same string as MS SQL Server :/
            tmp = tmp.Replace(" />", "/>");
            List<byte> xmlBytes = new List<byte>(Encoding.Unicode.GetBytes(tmp));

            //4 bytes: position of the next bytes to read
            //00 00 03 FC = 1020
            const int nextPosition = 1020;
            byte[] nextPositionBytes = BitConverter.GetBytes(nextPosition);

            if (xmlBytes.Count <= nextPosition)
            {
                //ulong is 8 bytes long
                ulong xmlLength = (ulong)xmlBytes.Count;
                byte[] sizeBytes = BitConverter.GetBytes(xmlLength);

                return Util.ConcatByteArrays(sizeBytes, xmlBytes.ToArray());
            }
            else
            {
                int nbAnchors = xmlBytes.Count / nextPosition;
                int modulo = xmlBytes.Count % nextPosition;

                int position = 0;
                for (int i = 0; i < nbAnchors; )
                {
                    xmlBytes.InsertRange(position, nextPositionBytes);
                    i++;
                    position = i * nextPosition;
                    position += i * nextPositionBytes.Count();
                }
                xmlBytes.InsertRange(position, BitConverter.GetBytes(modulo));

                //Start: 8 bytes: FE FF FF FF FF FF FF FF
                byte[] start = { 254, 255, 255, 255, 255, 255, 255, 255 };
                xmlBytes.InsertRange(0, start);

                //End: 4 bytes: 00 00 00 00
                byte[] end = { 0, 0, 0, 0 };
                xmlBytes.AddRange(end);

                return xmlBytes.ToArray();
            }
        }
    }
}
