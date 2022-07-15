using System;
using System.Collections.Concurrent;
using System.Xml.Serialization;

namespace Salesforce.Common;

public static class XmlSerializerCache
{
    private static readonly object _lock = new object();
    private static readonly ConcurrentDictionary<string, XmlSerializer> _serializers = new ConcurrentDictionary<string, XmlSerializer>();

    public static XmlSerializer GetSerializer<T>()
    {
        lock (_lock)
        {
            var key = $"{typeof(T)}";
            if (!_serializers.TryGetValue(key, out XmlSerializer serializer))
            {
                serializer = new XmlSerializer(typeof(T));
                _serializers.GetOrAdd(key, serializer);
            }

            return serializer;
        }
    }

    public static XmlSerializer GetSerializer<T>(string rootName)
    {
        lock (_lock)
        {
            var key = $"{typeof(T)}|{rootName}";
            if (!_serializers.TryGetValue(key, out XmlSerializer serializer))
            {
                if (!string.IsNullOrWhiteSpace(rootName))
                {
                    serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));
                    _serializers.GetOrAdd(key, serializer);
                }
                else
                {
                    serializer = new XmlSerializer(typeof(T));
                    _serializers.GetOrAdd(key, serializer);
                }
            }

            return serializer;
        }
    }

    public static XmlSerializer GetSerializer(Type type)
    {
        lock (_lock)
        {
            var key = $"{type}";
            if (!_serializers.TryGetValue(key, out XmlSerializer serializer))
            {
                serializer = new XmlSerializer(type);
                _serializers.GetOrAdd(key, serializer);
            }

            return serializer;
        }
    }
}