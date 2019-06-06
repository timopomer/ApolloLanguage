using System;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Parsing.Nodes;
using Newtonsoft.Json;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Types
{
    public abstract class TypeExpression : LiteralExpression
    {
        public static TypeExpression Illegal => null;

        [JsonIgnore]
        public override TypeExpression Type => this;

        protected TypeExpression(IContainsContext context, IR ir) : base(context, ir)
        {
        }

        public static TypeExpression Transform(PrimaryElement.TypeElement element, IR parent)
        {
            switch (element.Type)
            {
                case PrimaryElement.TypeElement.Types.Number: return new NumberType(element, parent);
                case PrimaryElement.TypeElement.Types.Str: return new StringType(element, parent);
            }
            throw new UnexpectedNodeException(element.ToString());
        }
    }
}
