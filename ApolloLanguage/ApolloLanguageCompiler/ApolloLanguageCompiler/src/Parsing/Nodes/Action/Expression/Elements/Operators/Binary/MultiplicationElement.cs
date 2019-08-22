using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class MultiplicationElement : BinaryOperatorElement
    {
        public override object Clone() => new MultiplicationElement(this);

        public MultiplicationOperator Operator { get; private set; }

        public enum MultiplicationOperator
        {
            Multiply,
            Divide,
            Mod
        }
        private static readonly Dictionary<SyntaxKeyword, MultiplicationOperator> TypeConverter = new Dictionary<SyntaxKeyword, MultiplicationOperator>
        {
            {SyntaxKeyword.Multiply,    MultiplicationOperator.Multiply },
            {SyntaxKeyword.Divide,      MultiplicationOperator.Divide   },
            {SyntaxKeyword.Mod,         MultiplicationOperator.Mod      }
        };

        private MultiplicationElement(ExpressionElement left, MultiplicationOperator @operator, ExpressionElement right) : base(left, right)
        {
            this.Operator = @operator;
        }
        private MultiplicationElement(MultiplicationElement element) : base(element)
        {
            this.Operator = element.Operator;
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(ExponationElement.TryParse, out expression, LocalWalker))
                return false;

            while (LocalWalker.TryGetNext(out Token MultiplicationToken, SyntaxKeyword.Multiply, SyntaxKeyword.Divide, SyntaxKeyword.Mod))
            {
                if (TryParse(ExponationElement.TryParse, out ExpressionElement right, LocalWalker))
                {
                    expression = new MultiplicationElement(expression, TypeConverter[MultiplicationToken.Kind], right);
                    expression.SetContext(Context, LocalWalker);
                }
                else
                    return false;
            }
            return true;
        }       
	}
}
