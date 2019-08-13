namespace GregsStack.PushCrew.Net.Api.Response
{
    public class NotificationStatusResponse : StatusResponse
    {
        /// <summary>
        /// Number of subscribers who clicked on the notification.
        /// </summary>
        public long CountClicked { get; set; }

        /// <summary>
        /// Number of subscribers to whom notification was delivered successfully.
        /// </summary>
        public long CountDelivered { get; set; }

        /// <summary>
        /// Number of Subscribers to whom notification was sent successfully.
        /// </summary>
        public long SuccessCount { get; set; }
    }
}
