using System;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.Parsing.Nodes;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Operators
{
    public abstract class OperatorExpression : ExpressionIR
    {
        protected OperatorExpression(IContainsContext context, IR parent) : base(context, parent)
        {
        }

        public static OperatorExpression Transform(OperatorElement element, IR parent)
        {
            switch (element)
            {
                case BinaryOperatorElement binaryOperator: return BinaryExpression.Transform(binaryOperator, parent);
            }
            throw new UnexpectedNodeException(element.ToString());
        }
    }
}
