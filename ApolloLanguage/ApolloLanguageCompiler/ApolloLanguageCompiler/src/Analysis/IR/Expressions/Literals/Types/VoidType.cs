using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals.Types;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Parsing.Nodes;
using GrEmit;
using Newtonsoft.Json;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Types
{
    public class VoidType : PrimitiveType
    {
        [JsonIgnore]
        public override TypeExpression Type => this;

        public override Type ILType => typeof(void);

        public VoidType(IContainsContext context, IR ir) : base(context, ir)
        {
        }
        public override bool Equals(object obj) => obj is VoidType;
    }
}
