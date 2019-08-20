namespace GregsStack.PushCrew.Net.Api
{
    using System;

    public static class PushCrewConfiguration
    {
        public static string ClientName { get; } = nameof(PushCrewClient);

        public static Uri BaseUri { get; } = new Uri("https://pushcrew.com/api/v1/");
    }
}
