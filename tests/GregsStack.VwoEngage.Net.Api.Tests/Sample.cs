namespace GregsStack.VwoEngage.Net.Api.Tests
{
    using System.Threading.Tasks;

    using Exceptions;

    using Extensions;

    using Microsoft.Extensions.DependencyInjection;

    using Xunit;

    public class Sample
    {
        [Fact]
        public async Task AnyMethod_BadToken_ThrowsUnauthorizedException()
        {
            var di = new ServiceCollection();

            di.AddPushCrewHttpClient("BadToken");
            di.AddTransient<IVwoEngageClient, VwoEngageClient>();

            var serviceProvider = di.BuildServiceProvider();

            var client = serviceProvider.GetRequiredService<IVwoEngageClient>();
            await Assert.ThrowsAsync<UnauthorizedException>(() => client.DeleteSegmentAsync(123));
        }
    }
}
