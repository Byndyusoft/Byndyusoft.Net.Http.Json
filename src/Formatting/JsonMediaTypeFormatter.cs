using System.IO;
using System.Net.Http.Formatting;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Json.Formatting
{
    /// <summary>
    ///     <see cref="MediaTypeFormatter" /> class to handle Json.
    /// </summary>
    public class JsonMediaTypeFormatter : MediaTypeFormatter
    {
        /// <summary>
        ///     Returns UTF8 Encoding without BOM and throws on invalid bytes.
        /// </summary>
        private static readonly Encoding Utf8EncodingWithoutBom = new UTF8Encoding(false, true);

        /// <summary>
        ///     Returns UTF16 Encoding which uses littleEndian byte order with BOM and throws on invalid bytes.
        /// </summary>
        private static readonly Encoding Utf16EncodingLittleEndian = new UnicodeEncoding(false, true, true);

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonMediaTypeFormatter" /> class.
        /// </summary>
        public JsonMediaTypeFormatter() : this(JsonDefaults.SerializerOptions)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonMediaTypeFormatter" /> class.
        /// </summary>
        /// <param name="serializerOptions">Options for running serialization.</param>
        public JsonMediaTypeFormatter(JsonSerializerOptions serializerOptions)
        {
            SerializerOptions = Guard.NotNull(serializerOptions, nameof(serializerOptions));

            SupportedEncodings.Add(Utf8EncodingWithoutBom);
            SupportedEncodings.Add(Utf16EncodingLittleEndian);

            SupportedMediaTypes.Add(JsonDefaults.MediaTypeHeaders.ApplicationJson);
            SupportedMediaTypes.Add(JsonDefaults.MediaTypeHeaders.ApplicationAnyJson);
            SupportedMediaTypes.Add(JsonDefaults.MediaTypeHeaders.TextJson);
        }

        public JsonSerializerOptions SerializerOptions { get; }

        /// <inheritdoc />
        public override async Task<object?> ReadFromStreamAsync(Type type, Stream readStream, HttpContent? content,
            IFormatterLogger? formatterLogger, CancellationToken cancellationToken = default)
        {
            Guard.NotNull(type, nameof(type));
            Guard.NotNull(readStream, nameof(readStream));

            if (content is ObjectContent objectContent)
                return objectContent.Value;

            var length = content?.Headers.ContentLength ?? -1;
            if (length == 0)
                return null;

            return await JsonSerializer.DeserializeAsync(readStream, type, SerializerOptions, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override Task WriteToStreamAsync(Type type, object? value, Stream writeStream, HttpContent? content,
            TransportContext? transportContext, CancellationToken cancellationToken = default)
        {
            Guard.NotNull(type, nameof(type));
            Guard.NotNull(writeStream, nameof(writeStream));

            return JsonSerializer.SerializeAsync(writeStream, value, type, SerializerOptions, cancellationToken);
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }
    }
}