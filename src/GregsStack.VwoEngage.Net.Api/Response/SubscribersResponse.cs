namespace GregsStack.VwoEngage.Net.Api.Response
{
    using System;
    using System.Collections.Generic;

    using Models;

    public class SubscribersResponse : StatusResponse
    {
        /// <summary>
        /// Denotes the count of active subscribers in the segment.
        /// Active subscribers are those for whom <see cref="Subscriber.IsInactive"/> is <c>false</c> and <see cref="Subscriber.IsGhost"/> is <c>false</c>.
        /// </summary>
        public long? CountActive { get; set; }

        /// <summary>
        /// Denotes the count of all subscribers in the segment.
        /// Includes inactive and ghost subscribers.
        /// </summary>
        public long? CountTotal { get; set; }

        /// <summary>
        /// Collection of subscribers.
        /// </summary>
        public ICollection<Subscriber> SubscriberList { get; set; } = new List<Subscriber>();

        /// <summary>
        /// Present if subscriber list can't be fetched.
        /// A string denoting why the request failed.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// It is a RESTFUL link to the previous page of results.
        /// Should be present on all pages except the first page.
        /// </summary>
        public Uri NextPage { get; set; }

        /// <summary>
        /// It is a RESTFUL link to the next page of results.
        /// Should be present on all pages except the last page.
        /// </summary>
        public Uri PreviousPage { get; set; }
    }
}
