namespace GregsStack.VwoEngage.Net.Api.Response.Models
{
    using System;
    using System.Net;

    public class Subscriber
    {
        /// <summary>
        /// Unique SubscriberID for the subscriber.
        /// </summary>
        public string SubscriberId { get; set; }

        /// <summary>
        /// The IP Address of the subscriber. Can be in IPV4/IPV6.
        /// VWO Engage masks certain octets (depending upon the format) by default in the IP Address to make sure that the IP address cannot be used to personally identify a subscriber.
        /// </summary>
        public IPAddress IpAddress { get; set; }

        /// <summary>
        /// The user agent string can be used for identifying the browser, browser version and operating system for the push subscription.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// The browser through which the subscriber subscribed. Should be present if platform is web-push. For example: Chrome, Firefox, Opera etc.
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// The version number of the browser through which subscriber subscribed. For example: 58.0.3029.110.
        /// </summary>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// The platform through which the subscriber subscribed. For example: "web-push".
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Value can be <see cref="Models.DevicePlatform.Desktop"/> / <see cref="Models.DevicePlatform.Mobile"/>.
        /// Depending upon the device from which the subscriber subscribed.
        /// </summary>
        public DevicePlatform DevicePlatform { get; set; }

        /// <summary>
        /// The operating system of the device through which subscriber subscribed. For example: MacOS, Windows.
        /// </summary>
        public string OperatingSystem { get; set; }

        /// <summary>
        /// The device through which the subscriber subscribed. It's value will be 'android' for subscriptions on Chrome on Android or '' (empty string) for subscriptions on browsers on desktops/laptops.
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// The country from where the subscriber subscribed. For example: India, Germany.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The region is the sub division or state from where the subscriber subscribed. For example: New Delhi, Texas.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The city from where the subscriber subscribed. For example: Houston, Milwaukee.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Field which denotes if subscriber has unsubscribed.
        /// <c>True</c> stands for unsubscribed and <c>false</c> stands for subscribed.
        /// </summary>
        public bool IsInactive { get; set; }

        /// <summary>
        /// Field which denotes if subscriber is a ghost.
        /// Ghosts is a terminology for subscribers whom we're not able to reach.
        /// <c>True</c> stands for unsubscribed and <c>false</c> stands for subscribed.
        /// </summary>
        public bool IsGhost { get; set; }

        /// <summary>
        /// The GMT timestamp of the time when the subscriber got added into our system.
        /// </summary>
        public DateTime SubscriberAddedTimestamp { get; set; }

        /// <summary>
        /// The GMT timestamp of the time when the subscriber got added into the segment.
        /// </summary>
        public DateTime AddedToSegmentTimestamp { get; set; }
    }
}
