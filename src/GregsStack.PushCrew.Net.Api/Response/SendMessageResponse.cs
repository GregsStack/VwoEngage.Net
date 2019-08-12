namespace GregsStack.PushCrew.Net.Api.Response
{
    public class SendMessageResponse : StatusResponse
    {
        /// <summary>
        /// To identify the push notification request number sent by you to our API.
        /// It is used in the Check Delivery Status API call to check detailed delivery status of push notifications for that particular request.
        /// </summary>
        public int RequestId { get; set; }
    }
}
