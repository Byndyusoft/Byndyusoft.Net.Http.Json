﻿using System.Reflection;
using System.Text.Json;

namespace System.Net.Http.Json
{
    public static class JsonSerializerOptionsExtensions
    {
        public static void CopyFrom(this JsonSerializerOptions serializerOptions, JsonSerializerOptions source)
        {
            if (serializerOptions == null) throw new ArgumentNullException(nameof(serializerOptions));
            if (source == null) throw new ArgumentNullException(nameof(source));

            var properties = typeof(JsonSerializerOptions).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (property.SetMethod == null)
                    continue;

                property.SetValue(serializerOptions, property.GetValue(source));
            }
        }
    }
}