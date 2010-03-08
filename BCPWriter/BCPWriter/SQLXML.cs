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
        public void Write(BinaryWriter writer, object value)
        {
            Write(writer, (XmlDocument)value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns></returns>
        public void Write(BinaryWriter writer, XmlDocument xml)
        {
            if (xml == null)
            {
                //8 bytes long
                byte[] nullBytes = { 255, 255, 255, 255, 255, 255, 255, 255 };
                writer.Write(nullBytes);
                return;
            }

            string xmlString = xml.DocumentElement.OuterXml;
            //System.Xml.XmlDocument does not give the same string as MS SQL Server :/
            xmlString = xmlString.Replace(" />", "/>");
            int xmlLength = xmlString.Length;

            //4 bytes: position of the next bytes to read
            //00 00 03 FC = 1020
            const int chunkValue = 1020;
            const int chunkSize = chunkValue / 2;

            if (xmlLength <= chunkSize)
            {
                //ulong is 8 bytes long
                //* 2 because we are in UTF-16, thus 1 char is 2 bytes long
                ulong length = (ulong)(xmlLength * 2);
                writer.Write(length);

                writer.Write(Encoding.Unicode.GetBytes(xmlString));
            }
            else
            {
                //Start: 8 bytes: FE FF FF FF FF FF FF FF
                byte[] start = { 254, 255, 255, 255, 255, 255, 255, 255 };
                writer.Write(start);
                ////

                //Cut in chunks and write them
                int nbAnchors = xmlLength / chunkSize;
                int position = 0;
                for (int i = 0; i < nbAnchors; )
                {
                    writer.Write(chunkValue);

                    string chunk = xmlString.Substring(position, chunkSize);
                    writer.Write(Encoding.Unicode.GetBytes(chunk));

                    i++;
                    position = i * chunkSize;
                }
                ////
                
                //Write last chunk
                int lastChunkSize = xmlLength % chunkSize;
                if (lastChunkSize > 0)
                {
                    //* 2 because we are in UTF-16
                    writer.Write(lastChunkSize * 2);

                    string lastChunk = xmlString.Substring(position);
                    writer.Write(Encoding.Unicode.GetBytes(lastChunk));
                    ////
                }

                //End: 4 bytes: 00 00 00 00
                byte[] end = { 0, 0, 0, 0 };
                writer.Write(end);
                ////
            }
        }
    }
}
