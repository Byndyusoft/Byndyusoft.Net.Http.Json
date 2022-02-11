#if NET5_0_OR_GREATER

using System.Net.Http.Formatting;
using System.Net.Http.Json.Formatting;
using Xunit;

namespace System.Net.Http.Json
{
    public class ModuleTests
    {
        [Fact]
        public void Init_Test()
        {
            // assert
            var writer = MediaTypeFormatterCollection.Default.FindWriter(typeof(string), JsonDefaults.MediaTypeHeader);
            Assert.NotNull(writer);
            Assert.IsType<JsonMediaTypeFormatter>(writer);
            
            var reader = MediaTypeFormatterCollection.Default.FindReader(typeof(string), JsonDefaults.MediaTypeHeader);
            Assert.NotNull(reader);
            Assert.IsType<JsonMediaTypeFormatter>(reader);
        }
    }
}

#endif