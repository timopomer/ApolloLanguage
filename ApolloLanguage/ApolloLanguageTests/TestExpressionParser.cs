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
            Expression expression = null;
            ExpressionParsers.Head.Invoke().Parse(ref expression, walker);
            Assert.IsNotNull(expression);
            Assert.IsTrue(walker.IsLast());
        }
        [Test()] public void CallFunctionWithParanethesisInParametersTest() => TestExpression(@"func((1),(1+2))");
        [Test()] public void CallFunctionThatReturnsFunctionTest() => TestExpression(@"func()()");
        [Test()] public void CallFunctionWithMultipleParametersTest() => TestExpression(@"func(1,2)");
        [Test()] public void CallFunctionWithParameterTest() => TestExpression(@"func(1)");
        [Test()] public void CallFunctionTest() => TestExpression(@"func()");
        [Test()] public void NegationNegationTest() => TestExpression(@"!-false");
        [Test()] public void NegationEqualityTest() => TestExpression(@"!false==true");
        [Test()] public void NegationTest() => TestExpression(@"!false");
        [Test()] public void ExponantionTest() => TestExpression(@"3^4//3");
        [Test()] public void MultiplicationTest() => TestExpression(@"3*4/6%2");
        [Test()] public void EqualityInEqualityTest() => TestExpression(@"(false==true)!=False");
        [Test()] public void EqualityTest() => TestExpression(@"1==2");
        [Test()] public void AssignAssignmentTest() => TestExpression(@"a=3=1");
        [Test()] public void AssignmentTest() => TestExpression(@"a=3");
        [Test()] public void SimpleNumberTest() => TestExpression(@"3");
        [Test()] public void SimpleParanthesizedNumberTest() => TestExpression(@"(3)");
    }
}
