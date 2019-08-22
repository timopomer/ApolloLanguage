using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class EqualityElement : BinaryOperatorElement
    {
        public override object Clone() => new EqualityElement(this);

        public EqualityOperator Operator { get; private set; }

        public enum EqualityOperator
        {
            Equal,
            InEqual
        }
        private static readonly Dictionary<SyntaxKeyword, EqualityOperator> TypeConverter = new Dictionary<SyntaxKeyword, EqualityOperator>
        {
            {SyntaxKeyword.Equal,   EqualityOperator.Equal      },
            {SyntaxKeyword.InEqual, EqualityOperator.InEqual    }
        };

        private EqualityElement(ExpressionElement left, EqualityOperator @operator, ExpressionElement right) : base(left, right)
        {
            this.Operator = @operator;
        }
        private EqualityElement(EqualityElement element) : base(element)
        {
            this.Operator = element.Operator;
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(ComparisonElement.TryParse, out expression, LocalWalker))
                return false;

            while (LocalWalker.TryGetNext(out Token EqualityToken, SyntaxKeyword.Equal, SyntaxKeyword.InEqual))
            {
                if (TryParse(ComparisonElement.TryParse, out ExpressionElement right, LocalWalker))
                {
                    expression = new EqualityElement(expression, TypeConverter[EqualityToken.Kind], right);
                    expression.SetContext(Context, LocalWalker);
                }
                else
                    return false;
            }
            return true;
        }       
	}
}
