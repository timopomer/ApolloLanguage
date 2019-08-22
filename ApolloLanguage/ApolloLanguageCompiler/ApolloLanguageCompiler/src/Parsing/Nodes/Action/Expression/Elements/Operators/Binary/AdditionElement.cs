using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class AdditionElement : BinaryOperatorElement
    {
        public override object Clone() => new AdditionElement(this);

        public AdditionOperator Operator { get; private set; }

        public enum AdditionOperator
        {
            Plus,
            Minus
        }
        private static readonly Dictionary<SyntaxKeyword, AdditionOperator> TypeConverter = new Dictionary<SyntaxKeyword, AdditionOperator>
        {
            {SyntaxKeyword.Plus,    AdditionOperator.Plus   },
            {SyntaxKeyword.Minus,   AdditionOperator.Minus  }
        };

        private AdditionElement(ExpressionElement left, AdditionOperator @operator, ExpressionElement right) : base(left, right)
        {
            this.Operator = @operator;
        }
        private AdditionElement(AdditionElement element) : base(element)
        {
            this.Operator = element.Operator;
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(MultiplicationElement.TryParse, out expression, LocalWalker))
                return false;

            while (LocalWalker.TryGetNext(out Token AdditionToken, SyntaxKeyword.Plus, SyntaxKeyword.Minus))
            {
                if (TryParse(MultiplicationElement.TryParse, out ExpressionElement right, LocalWalker))
                {
                    expression = new AdditionElement(expression, TypeConverter[AdditionToken.Kind], right);
                    expression.SetContext(Context, LocalWalker);
                }
                else
                    return false;
            }
            return true;
        }       
	}
}
