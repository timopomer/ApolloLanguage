using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.Source;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Tests
{
    public static class TestTools
    {
        public static TokenWalker GetWalker(string source)
        {
            IEnumerable<Token> Tokens = GetTokens(source);
            TokenWalker Walker = new TokenWalker(Tokens);
            return Walker;
        }

        public static IEnumerable<Token> GetTokens(string source)
        {
            SourceCode sourceCode = new SourceCode(source);
            IEnumerable<Token> Tokens = new TokenFactory(sourceCode);
            return Tokens;
        }
    }
}
