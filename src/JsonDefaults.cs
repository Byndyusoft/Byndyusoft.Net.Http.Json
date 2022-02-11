﻿using System.Net.Http.Headers;
using System.Text.Json;

namespace System.Net.Http.Json
{
    public static class JsonDefaults
    {
        public static readonly string MediaTypeFormat = "json";
        public static readonly string MediaType = MediaTypes.ApplicationJson;

        public static readonly MediaTypeWithQualityHeaderValue MediaTypeHeader =
            new MediaTypeWithQualityHeaderValue(MediaType) { CharSet = "utf-8" };

        public static JsonSerializerOptions SerializerOptions => new JsonSerializerOptions(JsonSerializerDefaults.Web);

        public static class MediaTypeHeaders
        {
            public static readonly MediaTypeWithQualityHeaderValue ApplicationJson =
                new MediaTypeWithQualityHeaderValue(MediaTypes.ApplicationJson);

            public static readonly MediaTypeWithQualityHeaderValue ApplicationAnyJson =
                new MediaTypeWithQualityHeaderValue(MediaTypes.ApplicationAnyJson);

            public static readonly MediaTypeWithQualityHeaderValue TextJson =
                new MediaTypeWithQualityHeaderValue(MediaTypes.TextJson);
        }

        public static class MediaTypes
        {
            public const string ApplicationJson = "application/json";
            public const string ApplicationAnyJson = "application/*+json";
            public const string TextJson = "text/json";
        }
    }
}