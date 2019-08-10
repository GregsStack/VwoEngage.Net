namespace GregsStack.PushCrew.Net.Api.Response
{
    using System;
    using System.Collections.Generic;

    public class SubscriberPushCrewResponse : PushCrewResponse
    {
        public long CountActive { get; set; }

        public long CountTotal { get; set; }

        public ICollection<Subscriber> SubscriberList { get; set; } = new List<Subscriber>();

        public Uri NextPage { get; set; }

        public Uri PreviousPage { get; set; }
    }
}
