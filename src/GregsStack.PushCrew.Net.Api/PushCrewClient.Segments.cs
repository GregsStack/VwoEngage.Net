namespace GregsStack.PushCrew.Net.Api
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Request;

    using Response;

    public partial class PushCrewClient
    {
        private const string SegmentsRelativeUri = "segments";

        public async Task<SegmentResponse> AddSegmentAsync(string name)
        {
            var dict = new Dictionary<string, string> { { "name", name } };
            return await this.PostAsync<Dictionary<string, string>, SegmentResponse>(dict, SegmentsRelativeUri);
        }

        public async Task<SegmentsResponse> ListSegmentsAsync()
        {
            return await this.GetAsync<SegmentsResponse>(SegmentsRelativeUri);
        }

        public async Task<SegmentResponse> AddSubscribersToSegmentAsync(long segmentId, ICollection<string> subscriberList)
        {
            var relativeUri = $"{SegmentsRelativeUri}/{segmentId}/subscribers";

            var validSubscriberList = subscriberList ?? throw new ArgumentNullException(nameof(subscriberList));
            var subscribers = new Dictionary<string, ICollection<string>> { { "subscriber_list", validSubscriberList } };
            var subscriberRequest = new SendMessageSubscribersRequest
            {
                SubscriberList = JsonConvert.SerializeObject(subscribers)
            };

            return await this.PostAsync<SendMessageSubscribersRequest, SegmentResponse>(subscriberRequest, relativeUri);
        }

        public async Task<SubscribersResponse> ListSubscribersInSegmentAsync(long segmentId, int page = 1, int perPage = 1024)
        {
            var relativeUri = $"{SegmentsRelativeUri}/{segmentId}/subscribers?page={page}&per_page={perPage}";
            return await this.GetAsync<SubscribersResponse>(relativeUri);
        }

        public async Task<StatusResponse> RemoveSubscribersAsync(long segmentId, RemoveSubscriberRequest removeSubscriberRequest)
        {
            var relativeUri = $"{SegmentsRelativeUri}/{segmentId}/subscribers";
            return await this.PutAsync<RemoveSubscriberRequest, StatusResponse>(removeSubscriberRequest, relativeUri);
        }

        public async Task<StatusResponse> DeleteSegmentAsync(long segmentId)
        {
            var relativeUri = $"{SegmentsRelativeUri}/{segmentId}";
            return await this.DeleteAsync<StatusResponse>(relativeUri);
        }
    }
}
