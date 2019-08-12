namespace GregsStack.PushCrew.Net.Api.Response
{
    using System.Collections.Generic;

    public class SegmentsResponse : StatusResponse
    {
        /// <summary>
        /// Denotes the number of segments the subscriber is a part of.
        /// </summary>
        public long? Count { get; set; }

        /// <summary>
        /// Present if segment list can't be fetched.
        /// A string denoting why the request failed.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Collection of segments with their name and IDs.
        /// </summary>
        public ICollection<Segment> SegmentList { get; set; } = new List<Segment>();
    }
}
