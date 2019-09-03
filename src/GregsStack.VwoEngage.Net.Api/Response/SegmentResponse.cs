namespace GregsStack.VwoEngage.Net.Api.Response
{
    public class SegmentResponse : StatusResponse
    {
        /// <summary>
        /// To identify the segment newly created.
        /// The same ID is used to add/remove subscribers to/from the segment through the API.
        /// </summary>
        public long SegmentId { get; set; }

        /// <summary>
        /// Present in case of failure. Used to denote reason of failure.
        /// </summary>
        public string Message { get; set; }
    }
}
