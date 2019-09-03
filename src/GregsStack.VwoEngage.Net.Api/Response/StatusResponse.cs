namespace GregsStack.VwoEngage.Net.Api.Response
{
    using Models;

    public class StatusResponse
    {
        /// <summary>
        /// To denote whether push request succeeded or not. Values can be <see cref="Models.Status.Success"/> or <see cref="Models.Status.Failure"/>.
        /// </summary>
        public Status Status { get; set; }
    }
}
