namespace GregsStack.VwoEngage.Net.Api
{
    using System;

    public static class VwoEngageConfiguration
    {
        public static string ClientName { get; } = nameof(VwoEngageClient);

        public static Uri BaseUri { get; } = new Uri("https://pushcrew.com/api/v1/");
    }
}
