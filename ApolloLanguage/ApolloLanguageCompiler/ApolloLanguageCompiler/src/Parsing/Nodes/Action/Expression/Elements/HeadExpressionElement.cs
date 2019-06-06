using ApolloLanguageCompiler.Tokenization;
using System;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing.Nodes
{
    public class HeadExpressionElement : ExpressionElement
    {
        public override object Clone() => new HeadExpressionElement();

        private HeadExpressionElement()
        {
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker) => AssignmentElement.TryParse(out expression, out walk, walker);
    }
}
