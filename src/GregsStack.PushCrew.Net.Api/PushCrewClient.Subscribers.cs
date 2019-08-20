namespace GregsStack.PushCrew.Net.Api
{
    using System.Threading.Tasks;

    using Response;

    public partial class PushCrewClient
    {
        public async Task<SubscribersResponse> ListSegmentsOfSubscriberAsync(string subscriberId)
        {
            var relativeUri = $"subscribers/{subscriberId}/segments";
            return await this.GetAsync<SubscribersResponse>(relativeUri);
        }
    }
}
