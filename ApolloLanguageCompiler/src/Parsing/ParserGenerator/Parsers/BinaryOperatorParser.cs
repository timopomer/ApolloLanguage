using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Tokenization;
using static ApolloLanguageCompiler.Parsing.TokenWalker;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;
using static ApolloLanguageCompiler.Parsing.ReferenceNodeParser;
using static ApolloLanguageCompiler.Parsing.TokenNodeEater;
using static ApolloLanguageCompiler.Parsing.TokenNodeKeeper;
using static ApolloLanguageCompiler.Parsing.UnaryNodeParser;
using static ApolloLanguageCompiler.Parsing.BinaryNodeParser;
using static ApolloLanguageCompiler.Parsing.AnyNodeParser;
using static ApolloLanguageCompiler.Parsing.ContinuousNodeParser;
using static ApolloLanguageCompiler.Parsing.WhileNodeParser;

namespace ApolloLanguageCompiler.Parsing
{
    public static class BinaryOperatorParser
    {
        public static NodeParser BinaryOperator(NodeTypes type, ReferenceNodeParser lowerPrecedence, params SyntaxKeyword[] operators) =>
            Continuous(
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
