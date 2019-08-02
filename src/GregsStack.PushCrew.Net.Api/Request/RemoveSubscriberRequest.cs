namespace GregsStack.PushCrew.Net.Api.Request
{
    using System.Collections.Generic;

    public class RemoveSubscriberRequest
    {
        public ICollection<string> DeleteList { get; set; } = new List<string>();
    }
}
