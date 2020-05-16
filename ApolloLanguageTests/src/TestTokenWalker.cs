using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Tokenization;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using static ApolloLanguageCompiler.Parsing.TokenWalker;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Tests
{
    [TestFixture()]
    public class TestTokenWalker
    {

        [Test()]
        public void CreationTest()
        {
            TokenFactory elems = new TokenFactory(new SourceCode("instance class Dog{}"));
            TokenWalker walker = new TokenWalker(elems);
        }
        [Test()]
        public void InitialTest()
        {
            TokenFactory elems = new TokenFactory(new SourceCode("instance class Dog{}"));
            TokenWalker walker = new TokenWalker(elems);

            Assert.AreEqual(walker.CurrentElement.Kind, SyntaxKeyword.Instance);
        }
        [Test()]
        public void MoveWalkerTest()
        {
            TokenFactory elems = new TokenFactory(new SourceCode("instance class Dog{}"));
            TokenWalker walker = new TokenWalker(elems);
            walker.GetNext();
            walker.GetNext();
            Assert.AreEqual(walker.CurrentElement.Kind, SyntaxKeyword.Class);
        }
        [Test()]
        public void FindElementTest()
        {
            TokenFactory elems = new TokenFactory(new SourceCode("instance class Dog{}"));
            TokenWalker walker = new TokenWalker(elems);
            Token instancetoken = walker.GetNext();
            walker.GetNext();

            Assert.AreEqual(walker.CurrentElement.Kind, SyntaxKeyword.Class);

            walker.CurrentElement = instancetoken;

            Assert.AreEqual(walker.CurrentElement.Kind, SyntaxKeyword.Instance);
        }
        [Test()]
        public void SkipWhitespaceTest()
        {
            SourceCode source = new SourceCode("  \nclass");
            TokenFactory elems = new TokenFactory(source);
            TokenWalker walker = new TokenWalker(elems);
            walker.SkipWhitespace();
            Token current = walker.CurrentElement;

            Assert.AreEqual(current.Kind, SyntaxKeyword.Class);
            Assert.AreEqual(current.Context, new SourceContext(3, 5, source));
        }
        [Test()]
        public void OutDelegateTest()
        {
            SourceCode source = new SourceCode("exposed instance");
            TokenFactory elems = new TokenFactory(source);
            TokenWalker walker = new TokenWalker(elems);

            IncrementWalker(out StateWalker Walkfunc, walker);

            walker.Walk(Walkfunc);

            Token current = walker.CurrentElement;

            Assert.AreEqual(current.Context, new SourceContext(7, 1, source));

            void IncrementWalker(out StateWalker Walk, TokenWalker Walker)
            {
                TokenWalker LocalWalker = new TokenWalker(Walker);
				Walk = (OldWalker) => OldWalker.CurrentElement = LocalWalker.CurrentElement;

				LocalWalker.GetNext();
            }
        }

    }
}
