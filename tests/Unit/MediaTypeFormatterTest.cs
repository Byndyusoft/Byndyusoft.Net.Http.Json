using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Json.Formatting;
using System.Net.Http.Json.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace System.Net.Http.Json.Unit
{
    public class MediaTypeFormatterTest
    {
        private readonly SystemTextJsonHttpContent _content;
        private readonly TransportContext? _context = null;
        private readonly JsonMediaTypeFormatter _formatter;
        private readonly IFormatterLogger? _logger = null;

        public MediaTypeFormatterTest()
        {
            _formatter = new JsonMediaTypeFormatter();
            _content = new SystemTextJsonHttpContent(_formatter.SerializerOptions);
        }

        [Fact]
        public void DefaultConstructor()
        {
            // Act
            var formatter = new JsonMediaTypeFormatter();

            // Assert
            Assert.NotNull(formatter.SerializerOptions);
        }

        [Fact]
        public void ConstructorWithSerializerOptions()
        {
            // Arrange
            var serializerOptions = new JsonSerializerOptions();

            // Act
            var formatter = new JsonMediaTypeFormatter(serializerOptions);

            // Assert
            Assert.Same(serializerOptions, formatter.SerializerOptions);
        }

        [Theory]
        [InlineData(typeof(IInterface), true)]
        [InlineData(typeof(AbstractClass), true)]
        [InlineData(typeof(NonPublicClass), true)]
        [InlineData(typeof(Dictionary<string, object>), true)]
        [InlineData(typeof(string), true)]
        [InlineData(typeof(SimpleType), true)]
        [InlineData(typeof(ComplexType), true)]
        public void CanReadType_ReturnsTrue(Type modelType, bool expectedCanRead)
        {
            // Act
            var result = _formatter.CanReadType(modelType);

            // Assert
            Assert.Equal(expectedCanRead, result);
        }

        [Theory]
        [InlineData(typeof(IInterface), true)]
        [InlineData(typeof(AbstractClass), true)]
        [InlineData(typeof(NonPublicClass), true)]
        [InlineData(typeof(Dictionary<string, object>), true)]
        [InlineData(typeof(string), true)]
        [InlineData(typeof(SimpleType), true)]
        [InlineData(typeof(ComplexType), true)]
        public void CanWriteType_CanReadType_ReturnsTrue(Type modelType, bool expectedCanRead)
        {
            // Act
            var result = _formatter.CanWriteType(modelType);

            // Assert
            Assert.Equal(expectedCanRead, result);
        }

        [Theory]
        [InlineData("application/json")]
        [InlineData("text/json")]
        [InlineData("application/*+json")]
        public void HasProperSupportedMediaTypes(string mediaType)
        {
            // Assert
            Assert.Contains(mediaType, _formatter.SupportedMediaTypes.Select(content => content.ToString()));
        }

        [Fact]
        public async Task ReadFromStreamAsync_WhenTypeIsNull_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => _formatter.ReadFromStreamAsync(null!, _content.Stream, _content, _logger));

            // Assert
            Assert.Equal("type", exception.ParamName);
        }

        [Fact]
        public async Task ReadFromStreamAsync_WhenStreamIsNull_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => _formatter.ReadFromStreamAsync(typeof(object), null!, _content, _logger));

            // Assert
            Assert.Equal("readStream", exception.ParamName);
        }

        [Fact]
        public async Task ReadFromStreamAsync_ReadsNullObject()
        {
            // Assert
            await _content.WriteObjectAsync<object>(null!);

            // Act
            var result = await _formatter.ReadFromStreamAsync(typeof(object), _content.Stream, _content, _logger);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ReadFromStreamAsync_ReadsBasicType()
        {
            // Arrange
            var expectedInt = 10;
            await _content.WriteObjectAsync(expectedInt);

            // Act
            var result = await _formatter.ReadFromStreamAsync(typeof(int), _content.Stream, _content, _logger);

            // Assert
            Assert.Equal(expectedInt, result);
        }

        [Fact]
        public async Task ReadFromStreamAsync_ReadsSimpleTypes()
        {
            // Arrange
            var input = SimpleType.Create();
            await _content.WriteObjectAsync(input);

            // Act
            var result = await _formatter.ReadFromStreamAsync(typeof(SimpleType), _content.Stream, _content, _logger);

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<SimpleType>(result);
            model.Verify();
        }

        [Fact]
        public async Task ReadFromStreamAsync_ReadsComplexTypes()
        {
            // Arrange
            var input = ComplexType.Create();
            await _content.WriteObjectAsync(input);

            // Act
            var result = await _formatter.ReadFromStreamAsync(typeof(ComplexType), _content.Stream, _content, _logger);

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<ComplexType>(result);
            model.Verify();
        }


        [Fact]
        public async Task WriteToStreamAsync_WhenTypeIsNull_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => _formatter.WriteToStreamAsync(null!, new object(), _content.Stream, _content, _context));

            // Assert
            Assert.Equal("type", exception.ParamName);
        }

        [Fact]
        public async Task WriteToStreamAsync_WhenStreamIsNull_ThrowsException()
        {
            // Act
            var exception =
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => _formatter.WriteToStreamAsync(typeof(object), new object(), null!, _content, _context));

            // Assert
            Assert.Equal("writeStream", exception.ParamName);
        }

        [Fact]
        public async Task WriteToStreamAsync_WritesNullObject()
        {
            // Act
            await _formatter.WriteToStreamAsync(typeof(object), null, _content.Stream, _content, _context);

            // Assert
            var result = await _content.ReadObjectAsync<object>();
            Assert.Null(result);
        }

        [Fact]
        public async Task WriteToStreamAsync_WritesBasicType()
        {
            // Arrange
            var expectedInt = 10;

            // Act
            await _formatter.WriteToStreamAsync(typeof(int), expectedInt, _content.Stream, _content, _context);

            // Assert
            var result = await _content.ReadObjectAsync<int>();
            Assert.Equal(expectedInt, result);
            Assert.NotEqual(0, _content.Headers.ContentLength);
        }

        [Fact]
        public async Task WriteToStreamAsync_WritesSimpleType()
        {
            // Arrange
            var input = SimpleType.Create();

            // Act
            await _formatter.WriteToStreamAsync(typeof(SimpleType), input, _content.Stream, _content, _context);

            // Assert
            var result = await _content.ReadObjectAsync<SimpleType>();
            Assert.NotEqual(0, _content.Headers.ContentLength);
            result!.Verify();
        }

        [Fact]
        public async Task WriteToStreamAsync_WritesComplexType()
        {
            // Arrange
            var input = ComplexType.Create();

            // Act
            await _formatter.WriteToStreamAsync(typeof(ComplexType), input, _content.Stream, _content, _context);

            // Assert
            var result = await _content.ReadObjectAsync<ComplexType>();
            Assert.NotEqual(0, _content.Headers.ContentLength);
            result!.Verify();
        }

        private class SystemTextJsonHttpContent : StreamContent
        {
            public SystemTextJsonHttpContent(JsonSerializerOptions serializerOptions) : this(new MemoryStream())
            {
                SerializerOptions = serializerOptions;
            }

            private SystemTextJsonHttpContent(MemoryStream stream)
                : base(stream)
            {
                Stream = stream;
            }

            public MemoryStream Stream { get; }
            private JsonSerializerOptions SerializerOptions { get; } = default!;

            public async Task WriteObjectAsync<T>(T value)
            {
                await JsonSerializer.SerializeAsync(Stream, value, typeof(T), SerializerOptions);
                Stream.Position = 0;
            }

            public async Task<T?> ReadObjectAsync<T>()
            {
                Stream.Position = 0;
                return await JsonSerializer.DeserializeAsync<T>(Stream, SerializerOptions);
            }
        }

        private interface IInterface
        {
        }

        private abstract class AbstractClass
        {
        }

        private class NonPublicClass
        {
        }
    }
}