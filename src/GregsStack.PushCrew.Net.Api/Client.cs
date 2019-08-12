namespace GregsStack.PushCrew.Net.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Converters;

    using Exceptions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    using Request;

    using Response;

    public class Client : IDisposable
    {
        private readonly HttpClient client;
        private readonly JsonMediaTypeFormatter jsonFormatter;
        private readonly Uri baseUri;

        public Client(string token)
            : this(token, new Uri("https://pushcrew.com/api/v1/"))
        {
        }

        public Client(string token, Uri baseUri)
        {
            var apiToken = token ?? throw new ArgumentNullException(nameof(token));
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", apiToken);

            this.baseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));

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

        public async Task<SendMessageResponse> SendAllSubscribers(SendMessageRequest request)
        {
            var uri = new Uri(this.baseUri, "send/all");
            return await this.PostAsync<SendMessageRequest, SendMessageResponse>(request, uri);
        }

        public async Task<SendMessageResponse> SendSubscribersInSegment(SendMessageRequest request, long segmentId)
        {
            var uri = new Uri(this.baseUri, $"send/segment/{segmentId}");
            return await this.PostAsync<SendMessageRequest, SendMessageResponse>(request, uri);
        }

        public async Task<SendMessageResponse> SendSubscribers(SendMessageRequest request, ICollection<string> subscriberList)
        {
            var uri = new Uri(this.baseUri, "send/list");

            var validRequest = request ?? throw new ArgumentNullException(nameof(request));
            var validSubscriberList = subscriberList ?? throw new ArgumentNullException(nameof(subscriberList));
            var subscribers = new Dictionary<string, ICollection<string>> { { "subscriber_list", validSubscriberList } };
            var subscriberRequest = (SendMessageSubscribersRequest)validRequest;
            subscriberRequest.SubscriberList = JsonConvert.SerializeObject(subscribers);

            return await this.PostAsync<SendMessageSubscribersRequest, SendMessageResponse>(subscriberRequest, uri);
        }

        public async Task<SendMessageResponse> SendSubscriber(SendMessageRequest request, string subscriberId)
        {
            var uri = new Uri(this.baseUri, "send/individual");

            var validRequest = request ?? throw new ArgumentNullException(nameof(request));
            var validSubscriberId = subscriberId ?? throw new ArgumentNullException(nameof(subscriberId));
            var subscriberRequest = (SendMessageSubscriberRequest)validRequest;
            subscriberRequest.SubscriberId = validSubscriberId;

            return await this.PostAsync<SendMessageSubscriberRequest, SendMessageResponse>(subscriberRequest, uri);
        }

        public async Task<NotificationStatusResponse> CheckNotificationRequestStatus(string id)
        {
            var uri = new Uri(this.baseUri, $"checkstatus/{id}");
            return await this.GetAsync<NotificationStatusResponse>(uri);
        }

        public async Task<ScheduleMessageResponse> ScheduleAllSubscribers(ScheduleMessageRequest request)
        {
            var uri = new Uri(this.baseUri, "send/all");
            return await this.PostAsync<ScheduleMessageRequest, ScheduleMessageResponse>(request, uri);
        }

        public async Task<ScheduleMessageResponse> ScheduleSegment(ScheduleMessageRequest request, string segmentId)
        {
            var uri = new Uri(this.baseUri, $"send/segment/{segmentId}");
            return await this.PostAsync<ScheduleMessageRequest, ScheduleMessageResponse>(request, uri);
        }

        public async Task<SegmentResponse> AddSegment(string name)
        {
            var uri = new Uri(this.baseUri, "segments");
            var dict = new Dictionary<string, string> { { "name", name } };
            return await this.PostAsync<Dictionary<string, string>, SegmentResponse>(dict, uri);
        }

        public async Task<SegmentsResponse> ListSegments()
        {
            var uri = new Uri(this.baseUri, "segments");
            return await this.GetAsync<SegmentsResponse>(uri);
        }

        public async Task<SegmentResponse> AddSubscribersToSegment(string segmentId, ICollection<string> subscriberList)
        {
            var uri = new Uri(this.baseUri, $"segments/{segmentId}/subscribers");

            var validSubscriberList = subscriberList ?? throw new ArgumentNullException(nameof(subscriberList));
            var subscribers = new Dictionary<string, ICollection<string>> { { "subscriber_list", validSubscriberList } };
            var subscriberRequest = new SendMessageSubscribersRequest
            {
                SubscriberList = JsonConvert.SerializeObject(subscribers)
            };

            return await this.PostAsync<SendMessageSubscribersRequest, SegmentResponse>(subscriberRequest, uri);
        }

        public async Task<SubscribersResponse> ListSubscribersInSegment(string segmentId, int page = 1, int perPage = 1024)
        {
            var uri = new Uri(this.baseUri, $"segments/{segmentId}/subscribers?page={page}&per_page={perPage}");
            return await this.GetAsync<SubscribersResponse>(uri);
        }

        public async Task<SubscribersResponse> ListSegmentsOfSubscriber(string subscriberId)
        {
            var uri = new Uri(this.baseUri, $"subscribers/{subscriberId}/segments");
            return await this.GetAsync<SubscribersResponse>(uri);
        }

        public async Task<StatusResponse> RemoveSubscribers(long segmentId, RemoveSubscriberRequest removeSubscriberRequest)
        {
            var uri = new Uri(this.baseUri, $"segments/{segmentId}/subscribers");
            var response = await this.client.PutAsync(uri, removeSubscriberRequest, this.jsonFormatter);
            await this.VerifyResponse(response);
            return await this.ReadAsAsync<StatusResponse>(response);
        }

        public async Task<StatusResponse> DeleteSegment(long segmentId)
        {
            var uri = new Uri(this.baseUri, $"segments/{segmentId}");
            var response = await this.client.DeleteAsync(uri);
            await this.VerifyResponse(response);
            return await this.ReadAsAsync<StatusResponse>(response);
        }

        public void Dispose()
        {
            this.client?.Dispose();
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request, Uri uri)
            where TRequest : class
            where TResponse : class
        {
            var response = await this.client.PostAsync(uri, request.ToFormUrlEncodedContent());
            await this.VerifyResponse(response);
            return await this.ReadAsAsync<TResponse>(response);
        }

        private async Task<TResponse> GetAsync<TResponse>(Uri uri)
            where TResponse : class
        {
            var response = await this.client.GetAsync(uri);
            await this.VerifyResponse(response);
            return await this.ReadAsAsync<TResponse>(response);
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
        }

        private async Task<T> ReadAsAsync<T>(HttpResponseMessage response)
            where T : class
        {
            return await response.Content.ReadAsAsync<T>(new[] { this.jsonFormatter });
        }
    }
}
