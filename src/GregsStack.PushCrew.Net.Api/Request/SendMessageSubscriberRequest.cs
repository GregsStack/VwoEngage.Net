namespace GregsStack.PushCrew.Net.Api.Request
{
    public class SendMessageSubscriberRequest : SendMessageRequest
    {
        /// <summary>
        /// Subscriber ID of the subscriber.
        /// </summary>
        public string SubscriberId { get; set; }
    }
}
