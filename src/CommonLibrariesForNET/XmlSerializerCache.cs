using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Salesforce.Common
{
    public class XmlSerializerCache
    {
        public static Lazy<XmlSerializerCache> Instance = new Lazy<XmlSerializerCache>(() => new XmlSerializerCache(), true);

        private XmlSerializerCache()
        {
            XmlSerializerDictionary = new ConcurrentDictionary<string, XmlSerializer>();
        }

        public ConcurrentDictionary<string, XmlSerializer> XmlSerializerDictionary { get; }
    }
}