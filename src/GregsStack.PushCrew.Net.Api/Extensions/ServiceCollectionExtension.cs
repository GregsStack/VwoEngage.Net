namespace GregsStack.PushCrew.Net.Api.Extensions
{
    using System;
    using System.Net.Http.Headers;

    using Attributes;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtension
    {
        public static IHttpClientBuilder AddPushCrewHttpClient([ValidatedNotNull] this IServiceCollection serviceCollection, [ValidatedNotNull] string apiToken)
        {
            var validatedServiceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
            var validatedApiToken = apiToken ?? throw new ArgumentNullException(nameof(apiToken));
            return validatedServiceCollection.AddHttpClient(PushCrewConfiguration.ClientName, client =>
            {
                client.BaseAddress = PushCrewConfiguration.BaseUri;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", validatedApiToken);
            });
        }
    }
}
