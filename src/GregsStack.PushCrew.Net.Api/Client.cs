namespace GregsStack.PushCrew.Net.Api
{
    using Converters;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public class Client
    {
        public void Resolve()
        {
            var contractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = contractResolver };
            jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            jsonSerializerSettings.Converters.Add(new BooleanConverter());
        }
    }
}
