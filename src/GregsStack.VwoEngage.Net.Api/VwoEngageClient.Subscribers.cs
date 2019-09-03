namespace GregsStack.VwoEngage.Net.Api
{
    using System.Threading.Tasks;

    using Response;

    public partial class VwoEngageClient
    {
        public async Task<SubscribersResponse> ListSegmentsOfSubscriberAsync(string subscriberId)
        {
            var relativeUri = $"subscribers/{subscriberId}/segments";
            return await this.GetAsync<SubscribersResponse>(relativeUri);
        }
    }
}
