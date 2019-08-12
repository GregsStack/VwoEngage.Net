namespace GregsStack.PushCrew.Net.Api.Response
{
    public class ScheduleMessageResponse : StatusResponse
    {
        /// <summary>
        /// An integer sent to identify the scheduling request sent by you to our API.
        /// </summary>
        public int ScheduledNotificationRequestId { get; set; }

        /// <summary>
        /// Used to denote success or reason of failure.
        /// </summary>
        public string Message { get; set; }
    }
}
