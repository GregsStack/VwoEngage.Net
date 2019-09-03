namespace GregsStack.VwoEngage.Net.Api
{
    using System;

    public static class VwoEngageConfiguration
    {
        /// <summary>
        /// Default client name to resolve named <see cref="System.Net.Http.HttpClient"/> instance.
        /// </summary>
        public static string ClientName { get; } = nameof(VwoEngageClient);

        /// <summary>
        /// Base VWO Engage API URL.
        /// </summary>
        public static Uri BaseUri { get; } = new Uri("https://pushcrew.com/api/v1/");
    }
}
