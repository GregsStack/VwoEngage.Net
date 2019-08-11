namespace GregsStack.PushCrew.Net.Api.Tests.Converters
{
    using System;

    using Api.Converters;

    using Request;

    using Xunit;

    public class FormUrlEncodedContentConverterTests
    {
        [Fact]
        public void Convert()
        {
            var testSample = new SendMessageRequest { TimeToLive = TimeSpan.FromSeconds(10) };
            var convert = testSample.ToFormUrlEncodedContent();
            Assert.NotNull(convert);
        }
    }
}
