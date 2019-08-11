namespace GregsStack.PushCrew.Net.Api.Request
{
    public class SendMessageSubscribersRequest : SendMessageRequest
    {
        /// <summary>
        /// JSON Object containing array of Subscriber ID's.
        /// </summary>
        public string SubscriberList { get; set; }
    }
}
