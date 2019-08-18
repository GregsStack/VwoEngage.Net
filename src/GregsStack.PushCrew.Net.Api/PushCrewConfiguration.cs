namespace GregsStack.PushCrew.Net.Api
{
    using System;

    public static class PushCrewConfiguration
    {
        public static readonly string ClientName = nameof(PushCrewClient);

        public static readonly Uri BaseUri = new Uri("https://pushcrew.com/api/v1/");
    }
}
