namespace GregsStack.VwoEngage.Net.Api.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    using Extensions;

    public class VwoEngageException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="VwoEngageException" /> class with serialized data.</summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
        /// <exception cref="SerializationException">The class name is null or <see cref="Exception.HResult" /> is zero (0).</exception>
        public VwoEngageException(SerializationInfo info, StreamingContext context)
            : base(info.GetSafeString("message") ?? info.GetSafeString("error"))
        {
        }
    }
}
