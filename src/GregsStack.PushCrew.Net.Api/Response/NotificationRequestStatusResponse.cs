namespace GregsStack.PushCrew.Net.Api.Response
{
    using System.Net.Http.Formatting;

    public class NotificationRequestStatusResponse : PushCrewResponse
    {
        public long CountClicked { get; set; }
        public long CountDelivered { get; set; }
        public long SuccessCount { get; set; }
    }
}
