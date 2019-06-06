using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing.Nodes
{
    public class NamespaceElement : BinaryOperatorElement
    {
        public override object Clone() => new NamespaceElement(this);

        private NamespaceElement(ExpressionElement left, ExpressionElement right) : base(left, right)
        {
        }

        private NamespaceElement(NamespaceElement element) : base(element)
        {
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(PrimaryElement.TryParse, out expression, LocalWalker))
                return false;

            while (LocalWalker.TryGetNext(SyntaxKeyword.Colon))
            {
                if (TryParse(PrimaryElement.TryParse, out ExpressionElement right, LocalWalker))
                {
                    expression = new NamespaceElement(expression, right);
                    expression.SetContext(Context, LocalWalker);
                }
                else
                    return false;
            }
            return true;
        }
    }
}