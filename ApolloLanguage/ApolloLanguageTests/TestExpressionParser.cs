using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Tokenization;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace ApolloLanguageCompiler.Tests
{
    [TestFixture()]
    public class TestExpressionParser
    {
        public static void TestExpression(string representation)
        {
            TokenWalker walker = new Compiler(new SourceCode(representation)).Walker;
            ExpressionParsers.Head.Invoke().Parse(out Expression expression, walker);
            Assert.IsNotNull(expression);
            Assert.IsTrue(walker.IsLast());
        }

        [Test()] public void AssignmentTest() => TestExpression(@"a=3");
        [Test()] public void SimpleNumberTest() => TestExpression(@"3");
        [Test()] public void SimpleParanthesizedNumberTest() => TestExpression(@"(3)");
    }
}
