namespace GregsStack.VwoEngage.Net.Api.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Extensions;

    using Response.Models;

    public class BadRequestException : VwoEngageException
    {
        /// <inheritdoc cref="VwoEngageException" />
        public BadRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            var statusString = info.GetSafeString("status");
            if (Enum.TryParse(statusString, true, out Status status))
            {
                this.Status = status;
            }

            this.InvalidList = info.GetSafeValue("invalid_list", new List<string>());
        }

        /// <summary>
        /// To denote whether push request succeeded or not. Values can be <see cref="Response.Models.Status.Success"/> or <see cref="Response.Models.Status.Failure"/>.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Invalid subscriber IDs.
        /// </summary>
        public ICollection<string> InvalidList { get; set; }
    }
}
