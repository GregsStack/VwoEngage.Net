namespace GregsStack.PushCrew.Net.Api
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Converters;

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

        public async Task<PushCrewResponse> CheckNotificationRequestStatus(string id)
        {
            var uri = new Uri(this.baseUri, $"checkstatus/{id}");
            var response = await this.client.GetAsync(uri);
            return await response.Content.ReadAsAsync<NotificationRequestStatusResponse>();
        }

        public async Task<PushCrewResponse> RemoveSubscribers(long segmentId, RemoveSubscriberRequest removeSubscriberRequest)
        {
            var uri = new Uri(this.baseUri, $"segments/{segmentId}/subscribers");
            var response = await this.client.PutAsync(uri, removeSubscriberRequest, this.jsonFormatter);
            return await response.Content.ReadAsAsync<PushCrewResponse>();
        }

        public async Task<PushCrewResponse> DeleteSegment(long segmentId)
        {
            var uri = new Uri(this.baseUri, $"segments/{segmentId}");
            var response = await this.client.DeleteAsync(uri);
            return await response.Content.ReadAsAsync<PushCrewResponse>();
        }

        public void Dispose()
        {
            this.client?.Dispose();
        }
    }
}
