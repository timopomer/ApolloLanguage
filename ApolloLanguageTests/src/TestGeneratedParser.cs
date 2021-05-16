using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ApolloLanguageCompiler.CLI;
using ApolloLanguageCompiler.Source;
using static ApolloLanguageCompiler.Parsing.ReferenceNodeParser;
using static ApolloLanguageCompiler.Parsing.TokenNodeEater;
using static ApolloLanguageCompiler.Parsing.TokenNodeKeeper;
using static ApolloLanguageCompiler.Parsing.UnaryNodeParser;
using static ApolloLanguageCompiler.Parsing.ManyNodeParser;
using static ApolloLanguageCompiler.Parsing.BinaryNodeParser;
using static ApolloLanguageCompiler.Parsing.AnyNodeParser;
using static ApolloLanguageCompiler.Parsing.ContinuousNodeParser;
using static ApolloLanguageCompiler.Parsing.WhileNodeParser;
using static ApolloLanguageCompiler.Parsing.BinaryOperatorParser;
using static ApolloLanguageCompiler.Parsing.ForeverNodeParser;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Tests
{
    [TestFixture()]
    public class TestGeneratedParser
    {
        [Test()]
        public void TestParseSingleToken()
        {
            string source = "variable123";
            TokenWalker Walker = TestTools.GetWalker(source);

            NodeParser Parser = Keep(SyntaxKeyword.Identifier).Name("Identifier");

            Node expression = null;
            Parser.Parse(ref expression, Walker, out _);
            Assert.IsNotNull(expression);
        }

        [Test()]
        public void TestParsingMissingToken()
        {
            string source = "variable123";
            TokenWalker Walker = TestTools.GetWalker(source);

            NodeParser Parser = Keep(SyntaxKeyword.Assignment).Name("Identifier");

            Node expression = null;
            
            Assert.Throws<FailedParsingNodeException>(() => Parser.Parse(ref expression, Walker, out _));
            Assert.IsNull(expression);
        }

        [Test()]
        public void TestAllParser()
        {
            string source = "[]";
            TokenWalker Walker = TestTools.GetWalker(source);

            NodeParser OpenParser = Keep(SyntaxKeyword.OpenSquareBracket).Name("OpenSquareBracket");
            NodeParser CloseParser = Keep(SyntaxKeyword.CloseSquareBracket).Name("CloseSquareBracket");
            NodeParser AllParser = Continuous(OpenParser, CloseParser).Name("OpenCloseParser");
            Node expression = null;

            AllParser.Parse(ref expression, Walker, out ParseResultHistory resultHistory);
            Assert.IsNotNull(expression);
        }

        [Test()]
        public void TestAnyParser()
        {
            string source = "[<]";
            TokenWalker Walker = TestTools.GetWalker(source);

            NodeParser OpenParser = Keep(SyntaxKeyword.OpenSquareBracket).Name("OpenSquareBracket");
            NodeParser LesserParser = Keep(SyntaxKeyword.Lesser).Name("Lesser");
            NodeParser GreaterParser = Keep(SyntaxKeyword.Greater).Name("Greater");
            NodeParser AnyParser = Any(GreaterParser, LesserParser).Name("Lesser or Greater");

            NodeParser CloseParser = Keep(SyntaxKeyword.CloseSquareBracket).Name("CloseSquareBracket");
            NodeParser AllParser = Continuous(OpenParser, AnyParser, CloseParser).Name("OpenCloseParser");
            Node expression = null;

            AllParser.Parse(ref expression, Walker, out ParseResultHistory resultHistory);
            Assert.IsNotNull(expression);
        }
    }
}
