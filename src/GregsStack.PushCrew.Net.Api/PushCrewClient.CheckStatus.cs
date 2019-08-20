﻿namespace GregsStack.PushCrew.Net.Api
{
    using System.Threading.Tasks;

    using Response;

    public partial class PushCrewClient
    {
        public async Task<NotificationStatusResponse> CheckNotificationRequestStatusAsync(string id)
        {
            var relativeUri = $"checkstatus/{id}";
            return await this.GetAsync<NotificationStatusResponse>(relativeUri);
        }
    }
}
