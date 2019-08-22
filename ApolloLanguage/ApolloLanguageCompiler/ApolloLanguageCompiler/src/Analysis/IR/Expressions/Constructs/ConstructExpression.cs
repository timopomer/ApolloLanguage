using System;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Constructs;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.Parsing;


namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Operators
{
    public abstract class ConstructExpression : ExpressionIR
    {
        protected ConstructExpression(IContainsContext context, IR parent) : base(context, parent)
        {
        }

        public static ConstructExpression Transform(ConstructElement element, IR parent)
        {
            switch (element)
            {
                case ParameterElement parameterElement: return new ParameterExpression(parameterElement, parent);
            }
            throw new UnexpectedNodeException(element.ToString());
        }
    }
}
