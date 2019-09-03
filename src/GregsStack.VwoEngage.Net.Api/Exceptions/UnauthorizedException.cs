namespace GregsStack.VwoEngage.Net.Api.Exceptions
{
    using System.Runtime.Serialization;

    public class UnauthorizedException : VwoEngageException
    {
        /// <inheritdoc cref="VwoEngageException" />
        public UnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
