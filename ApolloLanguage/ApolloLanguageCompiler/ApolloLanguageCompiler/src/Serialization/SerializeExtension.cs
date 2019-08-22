using System;
using ApolloLanguageCompiler.Serialization.ContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApolloLanguageCompiler.Serialization
{
    public static class SerializeExtension
    {
        public static string Serialize(this object obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects,
                Converters = new[] { new StringEnumConverter() },
                ContractResolver = new CompositeContractResolver
                {
                    new DontSerializeContractResolver<SourceContext>(),
                    new ModifierContractResolver()
                },

            };
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
