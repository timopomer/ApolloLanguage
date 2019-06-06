using System;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Literals.Types
{
    public abstract class PrimitiveType : TypeExpression
    {
        protected PrimitiveType(IContainsContext context, IR ir) : base(context, ir)
        {
        }

        public abstract Type ILType { get; }
    }
}
