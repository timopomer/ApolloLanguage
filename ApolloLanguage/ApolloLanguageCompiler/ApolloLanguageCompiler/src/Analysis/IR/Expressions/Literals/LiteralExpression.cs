using System;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.Parsing;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Literals
{
    public abstract class LiteralExpression : ExpressionIR
    {
        protected LiteralExpression(IContainsContext context, IR ir) : base(context, ir)
        {
        }

        public static LiteralExpression Transform(PrimaryElement element, IR parent)
        {
            switch (element)
            {
                case PrimaryElement.LiteralElement literalElement: 
                    switch(literalElement.Type)
                    {
                        case PrimaryElement.LiteralElement.Literal.Number: return new PrimitiveNumber(literalElement, parent);
                        case PrimaryElement.LiteralElement.Literal.Str: return new PrimitiveString(literalElement, parent);
                    }
                    break;
                case PrimaryElement.TypeElement typeElement: return TypeExpression.Transform(typeElement, parent);                    
                case PrimaryElement.IdentifierElement identifierElement: return new Identifier(identifierElement, parent);                    
            }
            throw new UnexpectedNodeException(element.ToString());
        }
    }
}
