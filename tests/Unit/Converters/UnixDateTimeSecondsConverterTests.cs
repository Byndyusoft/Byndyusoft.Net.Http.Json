using System.Net.Http.Json.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace System.Net.Http.Json.Unit.Converters
{
    public class UnixDateTimeSecondsConverterTests
    {
        [Fact]
        public void Deserialize_Test()
        {
            // arrange
            var json = @"{""Time"":1680894441}";

            // act
            var model = JsonSerializer.Deserialize<Model>(json);

            // assert
            var expected = DateTimeOffset.FromUnixTimeSeconds(1680894441).UtcDateTime;
            Assert.Equal(expected, model!.Time);
        }

        [Fact]
        public void Serialize_Test()
        {
            // arrange
            var model = new Model
            {
                Time = DateTimeOffset.FromUnixTimeSeconds(1680894441).UtcDateTime
            };

            // act
            var json = JsonSerializer.Serialize(model);

            // assert
            var expected = @"{""Time"":1680894441}";
            Assert.Equal(expected, json);
        }

        public class Model
        {
            [JsonConverter(typeof(UnixDateTimeSecondsConverter))]
            public DateTime Time { get; set; }
        }
    }
}