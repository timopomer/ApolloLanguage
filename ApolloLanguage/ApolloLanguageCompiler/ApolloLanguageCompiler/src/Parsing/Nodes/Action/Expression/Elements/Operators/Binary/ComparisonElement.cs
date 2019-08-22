using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class ComparisonElement : BinaryOperatorElement
    {
        public override object Clone() => new ComparisonElement(this);

        public ComparisonOperator Operator { get; private set; }

        public enum ComparisonOperator
        {
            Greater,
            GreaterEqual,
            Lesser,
            LesserEqual
        }
        private static readonly Dictionary<SyntaxKeyword, ComparisonOperator> TypeConverter = new Dictionary<SyntaxKeyword, ComparisonOperator>
        {
            {SyntaxKeyword.Greater,     ComparisonOperator.Greater      },
            {SyntaxKeyword.GreaterEqual,ComparisonOperator.GreaterEqual },
            {SyntaxKeyword.Lesser,      ComparisonOperator.Lesser       },
            {SyntaxKeyword.LesserEqual, ComparisonOperator.LesserEqual  }
        };

        private ComparisonElement(ExpressionElement left, ComparisonOperator @operator, ExpressionElement right) : base(left, right)
        {
            this.Operator = @operator;
        }
        private ComparisonElement(ComparisonElement element) : base(element)
        {
            this.Operator = element.Operator;
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(AdditionElement.TryParse, out expression, LocalWalker))
                return false;

            while (LocalWalker.TryGetNext(out Token ComparisonToken, SyntaxKeyword.Greater, SyntaxKeyword.GreaterEqual, SyntaxKeyword.Lesser, SyntaxKeyword.LesserEqual))
            {
                if (!TryParse(AdditionElement.TryParse, out ExpressionElement right, LocalWalker))
                {
                    expression = new ComparisonElement(expression, TypeConverter[ComparisonToken.Kind], right);
                    expression.SetContext(Context, LocalWalker);
                }
                else
                    return false;
            }
            return true;
        }       
	}
}
