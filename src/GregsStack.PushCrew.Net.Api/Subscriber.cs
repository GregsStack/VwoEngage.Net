namespace GregsStack.PushCrew.Net.Api
{
    using System;
    using System.Net;

    public class Subscriber
    {
        public string SubscriberId { get; set; }

        public IPAddress IpAddress { get; set; }

        public string UserAgent { get; set; }

        public string Browser { get; set; }

        public string BrowserVersion { get; set; }

        public string Platform { get; set; }

        public string DevicePlatform { get; set; }

        public string OperatingSystem { get; set; }

        public string Device { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public bool IsInactive { get; set; }

        public bool IsGhost { get; set; }

        public DateTime SubscriberAddedTimestamp { get; set; }

        public DateTime AddedToSegmentTimestamp { get; set; }
    }
}
