using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Tokenization;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Tests
{
    public class UnexpectedKeywordAmountException : Exception
	{
        public UnexpectedKeywordAmountException(params int[] amounts) : base($"UnexpectedKeywordAmountException: {string.Join("|", amounts)}") { }
	}
    [TestFixture()]
    public class TestTokenization
    {
        [Test()]
        public void AssignTest() => TestTokenFactory(
                "letter abc='c'",
                SyntaxKeyword.Letter,
                SyntaxKeyword.Whitespace,
                SyntaxKeyword.Identifier,
                SyntaxKeyword.Assignment,
                SyntaxKeyword.LiteralLetter
            );

        [Test()]
        public void ClassTest() => TestTokenFactory(
                "instance class Dog{}",
                SyntaxKeyword.Instance,
                SyntaxKeyword.Whitespace,
                SyntaxKeyword.Class,
                SyntaxKeyword.Whitespace,
                SyntaxKeyword.Identifier,
                SyntaxKeyword.OpenCurlyBracket,
                SyntaxKeyword.CloseCurlyBracket
            );

        [Test()]
        public void ExpressionTest() => TestTokenFactory(
                "a=1+2-3*4/5**6//7+c==b",
                SyntaxKeyword.Identifier,
                SyntaxKeyword.Assignment,
                SyntaxKeyword.LiteralNumber,
                SyntaxKeyword.Plus,
                SyntaxKeyword.LiteralNumber,
                SyntaxKeyword.Minus,
                SyntaxKeyword.LiteralNumber,
                SyntaxKeyword.Multiply,
                SyntaxKeyword.LiteralNumber,
                SyntaxKeyword.Divide,
                SyntaxKeyword.LiteralNumber,
                SyntaxKeyword.Power,
                SyntaxKeyword.LiteralNumber,
                SyntaxKeyword.Root,
                SyntaxKeyword.LiteralNumber,
                SyntaxKeyword.Plus,
                SyntaxKeyword.Identifier,
                SyntaxKeyword.Equal,
                SyntaxKeyword.Identifier
            );
        public static void TestTokenFactory(string code, params SyntaxKeyword[] types)
		{

            SyntaxKeyword[] generatedtypes = new TokenFactory(new SourceCode(code)).Select(n => n.Kind).ToArray();
            Assert.AreEqual(types.Length, generatedtypes.Count());

			for (int i = 0; i < types.Length; i++)
			{
                SyntaxKeyword left = generatedtypes.ElementAt(i);
                SyntaxKeyword right = types[i];

                Assert.AreEqual(left, right);
			}
		}
    }

}
