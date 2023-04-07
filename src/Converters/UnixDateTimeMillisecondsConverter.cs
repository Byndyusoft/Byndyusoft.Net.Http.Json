using System.Text.Json;
using System.Text.Json.Serialization;

namespace System.Net.Http.Json.Converters
{
    public class UnixDateTimeMillisecondsConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var unixTimestamp = reader.GetInt64();
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options)
        {
            var unixTimestamp = ((DateTimeOffset) dateTimeValue).ToUnixTimeMilliseconds();
            writer.WriteNumberValue(unixTimestamp);
        }
    }
}