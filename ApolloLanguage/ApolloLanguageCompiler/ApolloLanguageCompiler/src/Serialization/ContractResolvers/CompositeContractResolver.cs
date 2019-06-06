using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace ApolloLanguageCompiler.Serialization.ContractResolvers
{
    public class CompositeContractResolver : IContractResolver, IEnumerable<IContractResolver>
    {
        private readonly IList<IContractResolver> _contractResolvers = new List<IContractResolver>();

        public JsonContract ResolveContract(Type type) => this._contractResolvers.Select(x => x.ResolveContract(type)).FirstOrDefault(n => n != null);

        public void Add(IContractResolver contractResolver) => this._contractResolvers.Add(contractResolver);

        public IEnumerator<IContractResolver> GetEnumerator() => this._contractResolvers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
