﻿namespace GregsStack.VwoEngage.Net.Api
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Request;

    using Response;

    public partial class VwoEngageClient
    {
        private const string SendRelativeUri = "send";

        public async Task<SendMessageResponse> SendAllSubscribersAsync(SendMessageRequest request)
        {
            var relativeUri = $"{SendRelativeUri}/all";
            return await this.PostAsync<SendMessageRequest, SendMessageResponse>(request, relativeUri);
        }

        public async Task<SendMessageResponse> SendSubscribersInSegmentAsync(long segmentId, SendMessageRequest request)
        {
            var relativeUri = $"{SendRelativeUri}/segment/{segmentId}";
            return await this.PostAsync<SendMessageRequest, SendMessageResponse>(request, relativeUri);
        }

        public async Task<SendMessageResponse> SendSubscribersAsync(ICollection<string> subscriberList, SendMessageRequest request)
        {
            var relativeUri = $"{SendRelativeUri}/list";

            var validRequest = request ?? throw new ArgumentNullException(nameof(request));
            var validSubscriberList = subscriberList ?? throw new ArgumentNullException(nameof(subscriberList));
            var subscribers = new Dictionary<string, ICollection<string>> { { "subscriber_list", validSubscriberList } };
            var subscriberRequest = (SendMessageSubscribersRequest)validRequest;
            subscriberRequest.SubscriberList = JsonConvert.SerializeObject(subscribers);

            return await this.PostAsync<SendMessageSubscribersRequest, SendMessageResponse>(subscriberRequest, relativeUri);
        }

        public async Task<SendMessageResponse> SendSubscriberAsync(string subscriberId, SendMessageRequest request)
        {
            var relativeUri = $"{SendRelativeUri}/individual";

            var validRequest = request ?? throw new ArgumentNullException(nameof(request));
            var validSubscriberId = subscriberId ?? throw new ArgumentNullException(nameof(subscriberId));
            var subscriberRequest = (SendMessageSubscriberRequest)validRequest;
            subscriberRequest.SubscriberId = validSubscriberId;

            return await this.PostAsync<SendMessageSubscriberRequest, SendMessageResponse>(subscriberRequest, relativeUri);
        }

        public async Task<ScheduleMessageResponse> ScheduleAllSubscribersAsync(ScheduleMessageRequest request)
        {
            var relativeUri = $"{SendRelativeUri}/all";
            return await this.PostAsync<ScheduleMessageRequest, ScheduleMessageResponse>(request, relativeUri);
        }

        public async Task<ScheduleMessageResponse> ScheduleSegmentAsync(long segmentId, ScheduleMessageRequest request)
        {
            var relativeUri = $"{SendRelativeUri}/segment/{segmentId}";
            return await this.PostAsync<ScheduleMessageRequest, ScheduleMessageResponse>(request, relativeUri);
        }
    }
}
