namespace GregsStack.PushCrew.Net.Api.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using Attributes;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public static class FormUrlEncodedContentConverter
    {
        private static readonly JsonSerializerSettings JsonSettings;

        static FormUrlEncodedContentConverter()
        {
            var contractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };

            JsonSettings = new JsonSerializerSettings
            { ContractResolver = contractResolver, NullValueHandling = NullValueHandling.Ignore };

            JsonSettings.Converters.Add(new BooleanConverter());
            JsonSettings.Converters.Add(new StringEnumConverter());
            JsonSettings.Converters.Add(new TimeSpanConverter());
        }

        public static FormUrlEncodedContent ToFormUrlEncodedContent<T>([ValidatedNotNull] this T value)
            where T : class
        {
            var validValue = value ?? throw new ArgumentNullException(nameof(value));

            var jsonString = JsonConvert.SerializeObject(validValue, JsonSettings);
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

            return new FormUrlEncodedContent(values);
        }
    }
}
