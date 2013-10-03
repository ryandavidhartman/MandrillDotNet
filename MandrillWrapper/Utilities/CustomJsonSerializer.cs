using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using RestSharp.Serializers;

namespace MandrillWrapper.Utilities
{
    class CustomJsonSerializer : ISerializer
    {
        private readonly DataContractJsonSerializer _serializer;

        public string Serialize(object obj)
        {
            var memoryStream = new MemoryStream();
            _serializer.WriteObject(memoryStream, obj);
            string json = Encoding.UTF8.GetString(memoryStream.ToArray());
            memoryStream.Close();
            return json;
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }

        public CustomJsonSerializer(Type t)
        {
            _serializer = new DataContractJsonSerializer(t);
            ContentType = "application/json";
        }
    }
}
