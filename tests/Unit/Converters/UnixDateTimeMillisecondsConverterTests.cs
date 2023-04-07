using System.Net.Http.Json.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace System.Net.Http.Json.Unit.Converters
{
    public class UnixDateTimeMillisecondsConverterTests
    {
        [Fact]
        public void Deserialize_Test()
        {
            // arrange
            var json = @"{""Time"":16808940090000}";

            // act
            var model = JsonSerializer.Deserialize<Model>(json);

            // assert
            var expected = DateTimeOffset.FromUnixTimeMilliseconds(16808940090000).UtcDateTime;
            Assert.Equal(expected, model!.Time);
        }

        [Fact]
        public void Serialize_Test()
        {
            // arrange
            var model = new Model
            {
                Time = DateTimeOffset.FromUnixTimeMilliseconds(16808940090000).UtcDateTime
            };
            
            // act
            var json = JsonSerializer.Serialize(model);

            // assert
            var expected = @"{""Time"":16808940090000}";
            Assert.Equal(expected, json);
        }

        public class Model
        {
            [JsonConverter(typeof(UnixDateTimeMillisecondsConverter))]
            public DateTime Time { get; set; }
        }
    }
}
