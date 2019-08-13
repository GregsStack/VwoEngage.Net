namespace GregsStack.PushCrew.Net.Api.Request
{
    using System.Collections.Generic;

    public class RemoveSubscriberRequest
    {
        /// <summary>
        /// List of subscribers for deletion.
        /// </summary>
        public ICollection<string> DeleteList { get; set; } = new List<string>();
    }
}
