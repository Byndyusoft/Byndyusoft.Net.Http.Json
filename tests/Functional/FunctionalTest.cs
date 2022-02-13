using System.Net.Http.Json.Formatting;
using System.Net.Http.Json.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace System.Net.Http.Json.Functional
{
    public class FunctionalTest : MvcTestFixture
    {
        private readonly JsonMediaTypeFormatter _formatter;

        public FunctionalTest()
        {
            _formatter = new JsonMediaTypeFormatter();
        }

        protected override void ConfigureHttpClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(JsonDefaults.MediaTypeHeader);
        }

        protected override void ConfigureMvc(IMvcCoreBuilder builder)
        {
            builder.AddJsonOptions(
                options => { options.JsonSerializerOptions.CopyFrom(JsonDefaults.SerializerOptions); });
        }

        [Fact]
        public async Task PostAsync_NullObject()
        {
            // Act
            var response = await Client.PostAsync<SimpleType>("/json-formatter", null!, _formatter);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<SimpleType>(new[] {_formatter});

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostAsync_SimpleType()
        {
            // Act
            var response = await Client.PostAsync("/json-formatter", SimpleType.Create(), _formatter);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<SimpleType>(new[] {_formatter});

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);
            model.Verify();
        }

        [Fact]
        public async Task PutAsync_NullObject()
        {
            // Act
            var response = await Client.PutAsync<SimpleType>("/json-formatter", null!, _formatter);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<SimpleType>(new[] {_formatter});

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PutAsync_SimpleType()
        {
            // Act
            var response = await Client.PutAsync("/json-formatter", SimpleType.Create(), _formatter);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<SimpleType>(new[] {_formatter});

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);
            model.Verify();
        }
    }
}