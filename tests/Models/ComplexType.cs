using Xunit;

namespace System.Net.Http.Json.Models
{
    public class ComplexType
    {
        public SimpleType Inner { get; set; } = default!;

        public static ComplexType Create()
        {
            return new ComplexType
            {
                Inner = SimpleType.Create()
            };
        }

        public void Verify()
        {
            Assert.NotNull(Inner);
            Inner.Verify();
        }
    }
}