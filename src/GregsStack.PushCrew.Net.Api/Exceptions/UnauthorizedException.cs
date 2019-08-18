namespace GregsStack.PushCrew.Net.Api.Exceptions
{
    using System.Runtime.Serialization;

    public class UnauthorizedException : PushCrewException
    {
        /// <inheritdoc cref="PushCrewException" />
        public UnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
