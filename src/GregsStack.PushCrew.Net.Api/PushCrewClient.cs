namespace GregsStack.PushCrew.Net.Api
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading.Tasks;

    using Converters;

    using Exceptions;

    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public partial class PushCrewClient : IPushCrewClient
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
