using System.IO;
using System.Text.Json.Serialization;
using Xunit;

namespace System.Net.Http.Json.Models
{
    public class SimpleType
    {
        [JsonInclude] public string Field = default!;

        public int Property { get; set; }

        public SeekOrigin Enum { get; set; }

        public int? Nullable { get; set; }

        public int[] Array { get; set; } = default!;

        public static SimpleType Create()
        {
            return new SimpleType
            {
                Property = 10,
                Enum = SeekOrigin.Current,
                Field = "string",
                Array = new[] { 1, 2 },
                Nullable = 100
            };
        }

        public void Verify()
        {
            var input = Create();

            Assert.Equal(input.Property, Property);
            Assert.Equal(input.Field, Field);
            Assert.Equal(input.Enum, Enum);
            Assert.Equal(input.Array, Array);
            Assert.Equal(input.Nullable, Nullable);
        }
    }
}