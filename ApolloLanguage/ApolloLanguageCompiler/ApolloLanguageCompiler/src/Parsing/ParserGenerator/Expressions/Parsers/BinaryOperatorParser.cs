using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Tokenization;
using static ApolloLanguageCompiler.Parsing.TokenWalker;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;
using static ApolloLanguageCompiler.Parsing.ReferenceExpressionParser;
using static ApolloLanguageCompiler.Parsing.TokenExpressionEater;
using static ApolloLanguageCompiler.Parsing.TokenExpressionKeeper;
using static ApolloLanguageCompiler.Parsing.UnaryExpressionParser;
using static ApolloLanguageCompiler.Parsing.BinaryExpressionParser;
using static ApolloLanguageCompiler.Parsing.AnyExpressionParser;
using static ApolloLanguageCompiler.Parsing.AllExpressionParser;
using static ApolloLanguageCompiler.Parsing.WhileExpressionParser;

namespace ApolloLanguageCompiler.Parsing
{
    public static class BinaryOperatorParser
    {
        public static ExpressionParser BinaryOperator(Expressions type, ReferenceExpressionParser lowerPrecedence, params SyntaxKeyword[] operators) =>
            All(
                lowerPrecedence,
                While(
                    MakeBinary(
                        type,
                        Keep(operators),
                        lowerPrecedence
                    )
                )
            );
    }
}
