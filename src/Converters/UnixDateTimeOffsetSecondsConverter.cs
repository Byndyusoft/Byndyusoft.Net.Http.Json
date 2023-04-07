using System.Text.Json;
using System.Text.Json.Serialization;

namespace System.Net.Http.Json.Converters
{
    public class UnixDateTimeOffsetSecondsConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var unixTimestamp = reader.GetInt64();
            return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTimeOffset dateTimeValue,
            JsonSerializerOptions options)
        {
            var unixTimestamp = dateTimeValue.ToUnixTimeSeconds();
            writer.WriteNumberValue(unixTimestamp);
        }
    }
}