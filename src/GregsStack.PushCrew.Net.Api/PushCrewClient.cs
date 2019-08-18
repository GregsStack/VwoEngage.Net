namespace GregsStack.PushCrew.Net.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading.Tasks;

    using Converters;

    using Exceptions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    using Request;

    using Response;

    public class PushCrewClient : IPushCrewClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly JsonMediaTypeFormatter jsonFormatter;

        public PushCrewClient(IHttpClientFactory httpClientFactory)
        {
            this.clientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

            var contractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
            this.jsonFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings =
                {
                    ContractResolver = contractResolver
                }
            };

            this.jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            this.jsonFormatter.SerializerSettings.Converters.Add(new BooleanConverter());
        }

        public async Task<SendMessageResponse> SendAllSubscribersAsync(SendMessageRequest request)
        {
            const string relativeUri = "send/all";
            return await this.PostAsync<SendMessageRequest, SendMessageResponse>(request, relativeUri);
        }

        public async Task<SendMessageResponse> SendSubscribersInSegmentAsync(SendMessageRequest request, long segmentId)
        {
            var relativeUri = $"send/segment/{segmentId}";
            return await this.PostAsync<SendMessageRequest, SendMessageResponse>(request, relativeUri);
        }

        public async Task<SendMessageResponse> SendSubscribersAsync(SendMessageRequest request, ICollection<string> subscriberList)
        {
            const string relativeUri = "send/list";

            var validRequest = request ?? throw new ArgumentNullException(nameof(request));
            var validSubscriberList = subscriberList ?? throw new ArgumentNullException(nameof(subscriberList));
            var subscribers = new Dictionary<string, ICollection<string>> { { "subscriber_list", validSubscriberList } };
            var subscriberRequest = (SendMessageSubscribersRequest)validRequest;
            subscriberRequest.SubscriberList = JsonConvert.SerializeObject(subscribers);

            return await this.PostAsync<SendMessageSubscribersRequest, SendMessageResponse>(subscriberRequest, relativeUri);
        }

        public async Task<SendMessageResponse> SendSubscriberAsync(SendMessageRequest request, string subscriberId)
        {
            const string relativeUri = "send/individual";

            var validRequest = request ?? throw new ArgumentNullException(nameof(request));
            var validSubscriberId = subscriberId ?? throw new ArgumentNullException(nameof(subscriberId));
            var subscriberRequest = (SendMessageSubscriberRequest)validRequest;
            subscriberRequest.SubscriberId = validSubscriberId;

            return await this.PostAsync<SendMessageSubscriberRequest, SendMessageResponse>(subscriberRequest, relativeUri);
        }

        public async Task<NotificationStatusResponse> CheckNotificationRequestStatusAsync(string id)
        {
            var relativeUri = $"checkstatus/{id}";
            return await this.GetAsync<NotificationStatusResponse>(relativeUri);
        }

        public async Task<ScheduleMessageResponse> ScheduleAllSubscribersAsync(ScheduleMessageRequest request)
        {
            const string relativeUri = "send/all";
            return await this.PostAsync<ScheduleMessageRequest, ScheduleMessageResponse>(request, relativeUri);
        }

        public async Task<ScheduleMessageResponse> ScheduleSegmentAsync(ScheduleMessageRequest request, long segmentId)
        {
            var relativeUri = $"send/segment/{segmentId}";
            return await this.PostAsync<ScheduleMessageRequest, ScheduleMessageResponse>(request, relativeUri);
        }

        public async Task<SegmentResponse> AddSegmentAsync(string name)
        {
            const string relativeUri = "segments";
            var dict = new Dictionary<string, string> { { "name", name } };
            return await this.PostAsync<Dictionary<string, string>, SegmentResponse>(dict, relativeUri);
        }

        public async Task<SegmentsResponse> ListSegmentsAsync()
        {
            const string relativeUri = "segments";
            return await this.GetAsync<SegmentsResponse>(relativeUri);
        }

        public async Task<SegmentResponse> AddSubscribersToSegmentAsync(long segmentId, ICollection<string> subscriberList)
        {
            var relativeUri = $"segments/{segmentId}/subscribers";

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
            var relativeUri = $"segments/{segmentId}/subscribers?page={page}&per_page={perPage}";
            return await this.GetAsync<SubscribersResponse>(relativeUri);
        }

        public async Task<SubscribersResponse> ListSegmentsOfSubscriberAsync(string subscriberId)
        {
            var relativeUri = $"subscribers/{subscriberId}/segments";
            return await this.GetAsync<SubscribersResponse>(relativeUri);
        }

        public async Task<StatusResponse> RemoveSubscribersAsync(long segmentId, RemoveSubscriberRequest removeSubscriberRequest)
        {
            var relativeUri = $"segments/{segmentId}/subscribers";
            return await this.PutAsync<RemoveSubscriberRequest, StatusResponse>(removeSubscriberRequest, relativeUri);
        }

        public async Task<StatusResponse> DeleteSegmentAsync(long segmentId)
        {
            var relativeUri = $"segments/{segmentId}";
            return await this.DeleteAsync<StatusResponse>(relativeUri);
        }

        private HttpClient CreateClient()
        {
            return this.clientFactory.CreateClient(PushCrewConfiguration.ClientName);
        }

        private async Task<TResponse> DeleteAsync<TResponse>(string requestUri)
            where TResponse : class
        {
            var client = this.CreateClient();
            using (var response = await client.DeleteAsync(requestUri))
            {
                await this.VerifyResponse(response);
                return await this.ReadAsAsync<TResponse>(response);
            }
        }

        private async Task<TResponse> GetAsync<TResponse>(string requestUri)
            where TResponse : class
        {
            var client = this.CreateClient();
            using (var response = await client.GetAsync(requestUri))
            {
                await this.VerifyResponse(response);
                return await this.ReadAsAsync<TResponse>(response);
            }
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, string requestUri)
            where TRequest : class
            where TResponse : class
        {
            var client = this.CreateClient();
            using (var response = await client.PostAsync(requestUri, request.ToFormUrlEncodedContent()))
            {
                await this.VerifyResponse(response);
                return await this.ReadAsAsync<TResponse>(response);
            }
        }

        private async Task<TResponse> PutAsync<TRequest, TResponse>(TRequest request, string requestUri)
            where TRequest : class
            where TResponse : class
        {
            var client = this.CreateClient();
            using (var response = await client.PutAsync(requestUri, request, this.jsonFormatter))
            {
                await this.VerifyResponse(response);
                return await this.ReadAsAsync<TResponse>(response);
            }
        }

        private async Task VerifyResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    {
                        var exception = await this.ReadAsAsync<UnauthorizedException>(response);
                        throw exception;
                    }

                case HttpStatusCode.BadRequest:
                    {
                        var exception = await this.ReadAsAsync<BadRequestException>(response);
                        throw exception;
                    }

                case HttpStatusCode.InternalServerError:
                    {
                        var exception = await this.ReadAsAsync<InternalServerErrorException>(response);
                        throw exception;
                    }
            }

            response.EnsureSuccessStatusCode();
        }

        private async Task<TResponse> ReadAsAsync<TResponse>(HttpResponseMessage response)
            where TResponse : class
        {
            return await response.Content.ReadAsAsync<TResponse>(new[] { this.jsonFormatter });
        }
    }
}
