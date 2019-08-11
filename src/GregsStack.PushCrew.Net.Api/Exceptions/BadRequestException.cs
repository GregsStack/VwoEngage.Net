namespace GregsStack.PushCrew.Net.Api.Exceptions
{
    using System.Collections.Generic;

    public class BadRequestException : PushCrewException
    {
        /// <summary>
        /// To denote whether push request succeeded or not. Values can be <see cref="Api.Status.Success"/> or <see cref="Api.Status.Failure"/>.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Invalid subscriber IDs.
        /// </summary>
        public ICollection<string> InvalidList { get; set; }
    }
}
