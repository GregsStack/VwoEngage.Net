namespace GregsStack.PushCrew.Net.Api.Response
{
    public class StatusResponse
    {
        /// <summary>
        /// To denote whether push request succeeded or not. Values can be <see cref="Api.Status.Success"/> or <see cref="Api.Status.Failure"/>.
        /// </summary>
        public Status Status { get; set; }
    }
}
