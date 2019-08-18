namespace GregsStack.PushCrew.Net.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Request;

    using Response;

    public interface IPushCrewClient
    {
        Task<SendMessageResponse> SendAllSubscribersAsync(SendMessageRequest request);
        Task<SendMessageResponse> SendSubscribersInSegmentAsync(SendMessageRequest request, long segmentId);
        Task<SendMessageResponse> SendSubscribersAsync(SendMessageRequest request, ICollection<string> subscriberList);
        Task<SendMessageResponse> SendSubscriberAsync(SendMessageRequest request, string subscriberId);
        Task<NotificationStatusResponse> CheckNotificationRequestStatusAsync(string id);
        Task<ScheduleMessageResponse> ScheduleAllSubscribersAsync(ScheduleMessageRequest request);
        Task<ScheduleMessageResponse> ScheduleSegmentAsync(ScheduleMessageRequest request, string segmentId);
        Task<SegmentResponse> AddSegmentAsync(string name);
        Task<SegmentsResponse> ListSegmentsAsync();
        Task<SegmentResponse> AddSubscribersToSegmentAsync(string segmentId, ICollection<string> subscriberList);
        Task<SubscribersResponse> ListSubscribersInSegmentAsync(string segmentId, int page = 1, int perPage = 1024);
        Task<SubscribersResponse> ListSegmentsOfSubscriberAsync(string subscriberId);
        Task<StatusResponse> RemoveSubscribersAsync(long segmentId, RemoveSubscriberRequest removeSubscriberRequest);
        Task<StatusResponse> DeleteSegmentAsync(long segmentId);
    }
}
