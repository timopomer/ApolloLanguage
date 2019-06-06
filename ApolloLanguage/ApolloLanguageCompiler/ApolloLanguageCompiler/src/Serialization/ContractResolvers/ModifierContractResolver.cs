using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApolloLanguageCompiler.Serialization.ContractResolvers
{
    public class ModifierContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var Properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                 .Select(p => base.CreateProperty(p, memberSerialization));
            var Fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                             .Select(f => base.CreateProperty(f, memberSerialization));
            var Union = Properties.Union(Fields).ToList();
            Union.ForEach(p => { p.Writable = true; p.Readable = true; });
            return Union;
        }

    }
}
