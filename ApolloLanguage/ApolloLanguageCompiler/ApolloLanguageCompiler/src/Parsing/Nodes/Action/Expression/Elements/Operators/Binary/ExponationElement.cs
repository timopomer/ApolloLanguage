using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExponationElement : BinaryOperatorElement
    {
        public override object Clone() => new ExponationElement(this);

        public ExponationOperator Operator { get; private set; }

        public enum ExponationOperator
        {
            Power,
            Root
        }
        private static readonly Dictionary<SyntaxKeyword, ExponationOperator> TypeConverter = new Dictionary<SyntaxKeyword, ExponationOperator>
        {
            {SyntaxKeyword.Power,   ExponationOperator.Power    },
            {SyntaxKeyword.Root,    ExponationOperator.Root     }
        };

        private ExponationElement(ExpressionElement left, ExponationOperator @operator, ExpressionElement right) : base(left, right)
        {
            this.Operator = @operator;
        }
        private ExponationElement(ExponationElement element) : base(element)
        {
            this.Operator = element.Operator;
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(NegationElement.TryParse, out expression, LocalWalker))
                return false;

            while (LocalWalker.TryGetNext(out Token ExponationToken, SyntaxKeyword.Power, SyntaxKeyword.Root))
            {
                if (TryParse(NegationElement.TryParse, out ExpressionElement right, LocalWalker))
                {
                    expression = new ExponationElement(expression, TypeConverter[ExponationToken.Kind], right);
                    expression.SetContext(Context, LocalWalker);
                }
                else
                    return false;
            }
            return true;
        }
    }
}
