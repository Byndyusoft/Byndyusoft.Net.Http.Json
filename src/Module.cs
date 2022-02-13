#if NET5_0_OR_GREATER
using System.Net.Http.Formatting;
using System.Net.Http.Json.Formatting;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Json
{
    internal static class Module
    {
        [ModuleInitializer]
        internal static void Init()
        {
            MediaTypeFormatterCollection.Default.Add(new JsonMediaTypeFormatter());
        }
    }
}

#endif