namespace GregsStack.PushCrew.Net.Api.Request
{
    using System;

    public class SendMessageRequest
    {
        /// <summary>
        /// Title of the Push Notification. Maximum of 35 chars.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Message to be displayed in the Push Notification. Maximum of 80 chars.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// URL to open upon clicking of the push notification.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// URL of the icon to be shown in the notification.
        /// URL needs to be on HTTPS and needs to point to a 192 x 192 PNG.
        /// If this is not provided, the default company logo will be shown in the notification.
        /// </summary>
        public Uri ImageUrl { get; set; }

        /// <summary>
        /// This feature can only be used by business and enterprise accounts.
        /// URL of the big image to be shown in the notification.
        /// URL needs to be on HTTPS and needs to point to an image file.
        /// Please note that this only works for Chrome subscribers. Firefox does not support this as of now.
        /// </summary>
        public Uri HeroImageUrl { get; set; }

        /// <summary>
        /// This feature can only be used by business and enterprise accounts.
        /// This is the label of the first call-to-action button which will be shown in the notification.
        /// Maximum length of this parameter is 12 characters.
        /// Please note that this only works for Chrome subscribers. Firefox does not support this as of now.
        /// </summary>
        public string ButtonOneLabel { get; set; }

        /// <summary>
        /// This feature can only be used by business and enterprise accounts.
        /// This is the URL which will open upon clicking the first call-to-action button shown in the notification.
        /// Please note that this only works for Chrome subscribers. Firefox does not support this as of now.
        /// </summary>
        public Uri ButtonOneUrl { get; set; }

        /// <summary>
        /// This feature can only be used by business and enterprise accounts.
        /// This is the label of the second call-to-action button which will be shown in the notification.
        /// Maximum length of this parameter is 12 characters.
        /// Please note that this only works for Chrome subscribers. Firefox does not support this as of now.
        /// </summary>
        public string ButtonTwoLabel { get; set; }

        /// <summary>
        /// This feature can only be used by business and enterprise accounts.
        /// This is the URL which will open upon clicking the second call-to-action button shown in the notification.
        /// Please note that this only works for Chrome subscribers. Firefox does not support this as of now.
        /// </summary>
        public Uri ButtonTwoUrl { get; set; }

        /// <summary>
        /// This parameter is used to control the time up till which the notification should be attempted if the subscriber is offline.
        /// Pass the number of seconds elapsed after which the notification should not be sent.
        /// Default value(2419200) stands for 4 weeks.
        /// </summary>
        public TimeSpan? TimeToLive { get; set; }

        /// <summary>
        /// <c>True</c> denotes that the notification will remain on screen until it's clicked or closed.
        /// <c>False</c> denotes that the notification will auto-hide(if not clicked upon or closed) after 20 seconds.
        /// </summary>
        public bool? AutohideNotification { get; set; }
    }
}
