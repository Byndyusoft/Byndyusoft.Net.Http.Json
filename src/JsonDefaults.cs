using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace System.Net.Http.Json
{
    public static class JsonDefaults
    {
        public static readonly string MediaTypeFormat = "json";
        public static readonly string MediaType = MediaTypes.ApplicationJson;

        public static readonly MediaTypeWithQualityHeaderValue MediaTypeHeader = new(MediaType) {CharSet = "utf-8"};

        public static JsonSerializerOptions SerializerOptions => new(JsonSerializerDefaults.Web)
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };

        public static class MediaTypeHeaders
        {
            public static readonly MediaTypeWithQualityHeaderValue ApplicationJson = new(MediaTypes.ApplicationJson);

            public static readonly MediaTypeWithQualityHeaderValue ApplicationAnyJson =
                new(MediaTypes.ApplicationAnyJson);

            public static readonly MediaTypeWithQualityHeaderValue TextJson = new(MediaTypes.TextJson);
        }

        public static class MediaTypes
        {
            public const string ApplicationJson = "application/json";
            public const string ApplicationAnyJson = "application/*+json";
            public const string TextJson = "text/json";
        }
    }
}