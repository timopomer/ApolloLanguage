using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class AccessElement : BinaryOperatorElement
    {
        public override object Clone() => new AccessElement(this);

        private AccessElement(ExpressionElement left, ExpressionElement right) : base(left, right)
        {
        }
        private AccessElement(AccessElement element) : base(element)
        {
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(GenericElement.TryParse, out expression, LocalWalker))
                return false;

            while (LocalWalker.TryGetNext(SyntaxKeyword.Colon))
            {
                if (TryParse(GenericElement.TryParse, out ExpressionElement right, LocalWalker))
                {
                    expression = new AccessElement(expression, right);
                    expression.SetContext(Context, LocalWalker);
                }
                else
                    return false;
            }
            return true;
        }       
	}
}
