namespace GregsStack.VwoEngage.Net.Api.Extensions
{
    using System;
    using System.Runtime.Serialization;

    using Attributes;

    public static class SerializationInfoExtension
    {
        public static T GetSafeValue<T>([ValidatedNotNull] this SerializationInfo info, [ValidatedNotNull] string name, T defaultValue = default)
        {
            var serializationInfo = info ?? throw new ArgumentNullException(nameof(info));
            var elementName = name ?? throw new ArgumentNullException(nameof(name));

            try
            {
                return (T)serializationInfo.GetValue(elementName, typeof(T));
            }
            catch (SerializationException)
            {
                return defaultValue;
            }
        }

        public static string GetSafeString([ValidatedNotNull] this SerializationInfo info, [ValidatedNotNull] string name, string defaultValue = default) => info.GetSafeValue(name, defaultValue);
    }
}
