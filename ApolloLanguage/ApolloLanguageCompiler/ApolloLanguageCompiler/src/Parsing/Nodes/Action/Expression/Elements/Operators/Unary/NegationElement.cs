
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class NegationElement : UnaryOperatorElement
    {
        public override object Clone() => new NegationElement(this);

        public UnaryOperator Operator { get; private set; }

        public enum UnaryOperator
        {
            Minus,
            Negate
        }
        private static readonly Dictionary<SyntaxKeyword, UnaryOperator> TypeConverter = new Dictionary<SyntaxKeyword, UnaryOperator>
        {
            {SyntaxKeyword.Minus,    UnaryOperator.Minus    },
            {SyntaxKeyword.Negate,   UnaryOperator.Negate   }
        };

        private NegationElement(UnaryOperator @operator, ExpressionElement right) : base(right)
        {
            this.Operator = @operator;
        }
        private NegationElement(NegationElement element) : base(element)
        {
            this.Operator = element.Operator;
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (LocalWalker.TryGetNext(out Token UnaryToken, SyntaxKeyword.Minus, SyntaxKeyword.Negate))
            {
                if (TryParse(NegationElement.TryParse, out ExpressionElement right, LocalWalker))
                {
                    expression = new NegationElement(TypeConverter[UnaryToken.Kind], right);
                    expression.SetContext(LocalWalker);
                    return true;
                }
                else
                    throw new UnexpectedFailureException("Unary expression shouldve parsed according to previously found tokens");
            }
            return TryParse(CallFunctionElement.TryParse, out expression, LocalWalker);
        }       
	}
}
