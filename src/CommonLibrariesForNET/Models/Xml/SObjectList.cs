using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Salesforce.Common.Models.Xml
{
    [XmlRoot(Namespace = "http://www.force.com/2009/06/asyncapi/dataload", ElementName = "sObjects")]
    public sealed class SObjectList<T> : List<T>, ISObjectList<T>
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var entry in this)
            {
                var value = entry as IXmlSerializable;
                if (value != null)
                {
                    value.WriteXml(writer);
                }
                else
                {
                    // JF:  Check the XmlSerializer Cache before creating a new one
                    // See: https://github.com/wadewegner/Force.com-Toolkit-for-NET/pull/255
                    // See: https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?redirectedfrom=MSDN&view=netcore-3.1
                    XmlSerializer xmlSerializer = XmlSerializerCache.Instance.Value.GetSerializer<T>("sObject");

                    var ns = new XmlSerializerNamespaces();
                    ns.Add(string.Empty, string.Empty);
                    var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
                    var stringBuilder = new StringBuilder();
                    using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                    {
                        xmlSerializer.Serialize(xmlWriter, entry, ns);
                    }
                    writer.WriteRaw(stringBuilder.ToString());
                }
            }
        }
    }
}
