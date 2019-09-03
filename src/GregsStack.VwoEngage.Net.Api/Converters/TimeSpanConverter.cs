namespace GregsStack.VwoEngage.Net.Api.Converters
{
    using System;

    using Newtonsoft.Json;

    public class TimeSpanConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var totalSeconds = Convert.ToInt32(((TimeSpan)value).TotalSeconds);
            writer.WriteValue(totalSeconds);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (double.TryParse(reader?.Value?.ToString(), out var value))
            {
                return TimeSpan.FromSeconds(value);
            }

            return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? (TimeSpan?)null
                : new TimeSpan();
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(objectType) : objectType) == typeof(TimeSpan);
        }
    }
}
