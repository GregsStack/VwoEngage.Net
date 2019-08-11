namespace GregsStack.PushCrew.Net.Api.Response
{
    public class NotificationStatusResponse : PushCrewResponse
    {
        public long CountClicked { get; set; }
        public long CountDelivered { get; set; }
        public long SuccessCount { get; set; }
    }
}
