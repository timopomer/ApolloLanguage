using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class ExpressionElement : ASTNode
    {
        public static bool TryParse(IContains<ExpressionElement> container, TokenWalker localWalker)
        {
            if (HeadExpressionElement.TryParse(out ExpressionElement Expression, out StateWalker EqualityWalk, localWalker))
                localWalker.Walk(EqualityWalk);
            else
                return false;

            container.AddNode(Expression);
            return true;
        }

        public delegate bool ExpressionParser(out ExpressionElement expression, out StateWalker walk, TokenWalker walker);
        public static bool TryParse(IContains<ExpressionElement> container, TokenWalker walker, ExpressionParser parser)
        {
            if (parser(out ExpressionElement Expression, out StateWalker walk, walker))
                walk(walker);
            else
                return false;

            container.AddNode(Expression);
            return true;
        }
        public static bool TryParse(ExpressionParser parser, out ExpressionElement expression, TokenWalker walker)
        {
            if (parser(out expression, out StateWalker walk, walker))
            {
                walk(walker);
                return true;
            }
            else
                return false;
        }
        public static bool TryParseParameters(out List<ExpressionElement> parameters, TokenWalker walker, out StateWalker walk, ExpressionParser parameterParser, SyntaxKeyword openingKeyword, SyntaxKeyword seperatorKeyword, SyntaxKeyword closingKeyword, bool allowNoParameters)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            parameters = new List<ExpressionElement>();

            if (!LocalWalker.TryGetNext(openingKeyword))
                return false;

            while (!LocalWalker.TryGetNext(closingKeyword))
            {
                if (parameters.Any() && !LocalWalker.TryGetNext(seperatorKeyword))
                    return false;

                TryParse(parameterParser, out ExpressionElement expression, LocalWalker);
                parameters.Add(expression);
            }

            if (!parameters.Any() && !allowNoParameters)
                return false;

            return true;
        }
    }
}
