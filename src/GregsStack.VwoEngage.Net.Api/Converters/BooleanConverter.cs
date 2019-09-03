namespace GregsStack.VwoEngage.Net.Api.Converters
{
    using System;

    using Newtonsoft.Json;

    public class BooleanConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((bool)value ? 1 : 0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader?.Value?.ToString() == "1";
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(objectType) : objectType) == typeof(bool);
        }
    }
}
