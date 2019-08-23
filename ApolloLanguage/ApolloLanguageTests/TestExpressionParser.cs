using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Tokenization;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ApolloLanguageCompiler.Parsing;

namespace ApolloLanguageCompiler.Tests
{
    [TestFixture()]
    public class TestExpressionParser
    {
        [Test()]
        public void AssignmentTest()
        {
            string code = @"3+3";
            TokenWalker walker = new Compiler(new SourceCode(code)).Walker;
            ExpressionParsers.Head.Invoke().Parse(out IExpression expression, walker);
        }
    }
}
