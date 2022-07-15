using System;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace Salesforce.Common;

public class XmlSerializerCache
{
    public static readonly Lazy<XmlSerializerCache> Instance = new Lazy<XmlSerializerCache>(() => new XmlSerializerCache(), true);
    
    private readonly ConcurrentDictionary<string, XmlSerializer> _serializers = new ConcurrentDictionary<string, XmlSerializer>();

    public XmlSerializer GetSerializer<T>()
    {
        var key = $"{typeof(T).FullName}";
        return _serializers.GetOrAdd(key, k => new XmlSerializer(typeof(T)));
    }

    public XmlSerializer GetSerializer<T>(string rootName)
    {
        var key = $"{typeof(T).FullName}|{rootName}";
        return _serializers.GetOrAdd(key, k => new XmlSerializer(typeof(T), new XmlRootAttribute(rootName)));
    }

    public XmlSerializer GetSerializer(Type type)
    {
        var key = $"{type.FullName}";
        return _serializers.GetOrAdd(key, k => new XmlSerializer(type));
    }
}
