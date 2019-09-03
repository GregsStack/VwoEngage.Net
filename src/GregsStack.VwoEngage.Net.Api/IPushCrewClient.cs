namespace GregsStack.VwoEngage.Net.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Request;

    using Response;

    public interface IPushCrewClient
    {
        Task<SendMessageResponse> SendAllSubscribersAsync(SendMessageRequest request);
        Task<SendMessageResponse> SendSubscribersInSegmentAsync(long segmentId, SendMessageRequest request);
        Task<SendMessageResponse> SendSubscribersAsync(ICollection<string> subscriberList, SendMessageRequest request);
        Task<SendMessageResponse> SendSubscriberAsync(string subscriberId, SendMessageRequest request);
        Task<NotificationStatusResponse> CheckNotificationRequestStatusAsync(string id);
        Task<ScheduleMessageResponse> ScheduleAllSubscribersAsync(ScheduleMessageRequest request);
        Task<ScheduleMessageResponse> ScheduleSegmentAsync(long segmentId, ScheduleMessageRequest request);
        Task<SegmentResponse> AddSegmentAsync(string name);
        Task<SegmentsResponse> ListSegmentsAsync();
        Task<SegmentResponse> AddSubscribersToSegmentAsync(long segmentId, ICollection<string> subscriberList);
        Task<SubscribersResponse> ListSubscribersInSegmentAsync(long segmentId, int page = 1, int perPage = 1024);
        Task<SubscribersResponse> ListSegmentsOfSubscriberAsync(string subscriberId);
        Task<StatusResponse> RemoveSubscribersAsync(long segmentId, RemoveSubscriberRequest request);
        Task<StatusResponse> DeleteSegmentAsync(long segmentId);
    }
}
