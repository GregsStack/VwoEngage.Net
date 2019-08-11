namespace GregsStack.PushCrew.Net.Api.Response
{
    using System.Collections.Generic;

    public class SegmentResponse : PushCrewResponse
    {
        public long Count { get; set; }

        public ICollection<Segment> SegmentList { get; set; } = new List<Segment>();
    }
}
