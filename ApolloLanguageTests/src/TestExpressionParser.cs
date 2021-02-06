using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ApolloLanguageCompiler.CLI;
using ApolloLanguageCompiler.Source;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Tests
{
    [TestFixture()]
    public class TestExpressionParser
    {
        public static void TestExpression(string representation, string expectedRepresentation=null, bool shouldFail=false)
        {
            SourceCode source = new SourceCode(representation);
            TokenWalker walker = new Compiler(source).Walker;
            Node expression = null;
            Parsers.Expression.Head.Invoke().Parse(ref expression, walker, out ParseResultHistory resultHistory);
            if (shouldFail)
                Assert.IsNull(expression);
            //Console.WriteLine(resultHistory.SuccessfulParsers);
            Assert.AreEqual(expression.ToString(), expectedRepresentation);
            Assert.IsNotNull(expression);
            Assert.AreEqual(new SourceContext(0, representation.Length), expression.Context);
            Assert.IsTrue(walker.IsLast());
        }

        //[Test()] public void NoGenericTypeTest() => TestExpression(@"List<>");
        //[Test()] public void MultipleGenericTypeTest() => TestExpression(@"List<str,str>");
        //[Test()] public void GenericFunctionCallTest() => TestExpression(@"func<T>(T)");
        //[Test()] public void GenericFunctionTest() => TestExpression(@"func<str>");
        [Test()] public void CallAccessMultipleNamespacesTest() => TestExpression(@"System:Console:WriteLine()", "Unary<FunctionCall>[Many[], Binary<Access>[Binary<Access>[Token[Identifier->System], Token[Colon->:], Token[Identifier->Console]], Token[Colon->:], Token[Identifier->WriteLine]]]");
        [Test()] public void AccessMultipleNamespacesTest() => TestExpression(@"System:Console:WriteLine", "Binary<Access>[Binary<Access>[Token[Identifier->System], Token[Colon->:], Token[Identifier->Console]], Token[Colon->:], Token[Identifier->WriteLine]]");
        [Test()] public void AccessNamespaceTest() => TestExpression(@"System:Console", "Binary<Access>[Token[Identifier->System], Token[Colon->:], Token[Identifier->Console]]");
        [Test()] public void CallFunctionWithParanethesisInParametersTest() => TestExpression(@"func((1),(1+2))", "Unary<FunctionCall>[Many[Many[Many[Token[OpenParenthesis->(],Token[LiteralNumber->1],Token[CloseParenthesis->)]]],Many[Token[OpenParenthesis->(],Binary<Addition>[Token[LiteralNumber->1], Token[Plus->+], Token[LiteralNumber->2]],Token[CloseParenthesis->)]]], Token[Identifier->func]]");
        [Test()] public void CallFunctionThatContainsFunctionTest() => TestExpression(@"func1(func2())", "Unary<FunctionCall>[Many[Unary<FunctionCall>[Many[], Token[Identifier->func2]]], Token[Identifier->func1]]");
        [Test()] public void CallFunctionThatReturnsFunctionWithParametersTest() => TestExpression(@"func()(2)", "Unary<FunctionCall>[Many[Token[LiteralNumber->2]], Unary<FunctionCall>[Many[], Token[Identifier->func]]]");
        [Test()] public void CallFunctionThatReturnsFunctionTest() => TestExpression(@"func()()", "Unary<FunctionCall>[Many[], Unary<FunctionCall>[Many[], Token[Identifier->func]]]");
        [Test()] public void CallFunctionWithMultipleParametersTest() => TestExpression(@"func(1,2)", "Unary<FunctionCall>[Many[Many[Token[LiteralNumber->1]],Token[LiteralNumber->2]], Token[Identifier->func]]");
        [Test()] public void CallFunctionWithParameterTest() => TestExpression(@"func(1)", "Unary<FunctionCall>[Many[Token[LiteralNumber->1]], Token[Identifier->func]]");
        [Test()] public void CallFunctionTest() => TestExpression(@"func()", "Unary<FunctionCall>[Many[], Token[Identifier->func]]");
        [Test()] public void NegationNegationTest() => TestExpression(@"!-false", "Unary<Negation>[Unary<Negation>[Token[LiteralFalse->false], Token[Minus->-]], Token[Negate->!]]");
        [Test()] public void NegationEqualityTest() => TestExpression(@"!false==true", "Binary<Equality>[Unary<Negation>[Token[LiteralFalse->false], Token[Negate->!]], Token[Equal->==], Token[LiteralTrue->true]]");
        [Test()] public void NegationTest() => TestExpression(@"!false", "Unary<Negation>[Token[LiteralFalse->false], Token[Negate->!]]");
        [Test()] public void RootTest() => TestExpression(@"3//4", "Binary<Exponentiation>[Token[LiteralNumber->3], Token[Root->//], Token[LiteralNumber->4]]");
        [Test()] public void ExponantionTest() => TestExpression(@"3**4", "Binary<Exponentiation>[Token[LiteralNumber->3], Token[Power->**], Token[LiteralNumber->4]]");
        [Test()] public void MultiplicationTest() => TestExpression(@"3*4/6%2", "Binary<Multiplication>[Binary<Multiplication>[Binary<Multiplication>[Token[LiteralNumber->3], Token[Multiply->*], Token[LiteralNumber->4]], Token[Divide->/], Token[LiteralNumber->6]], Token[Mod->%], Token[LiteralNumber->2]]");
        [Test()] public void EqualityInEqualityTest() => TestExpression(@"(false==true)!=false", "Binary<Equality>[Many[Token[OpenParenthesis->(],Binary<Equality>[Token[LiteralFalse->false], Token[Equal->==], Token[LiteralTrue->true]],Token[CloseParenthesis->)]], Token[InEqual->!=], Token[LiteralFalse->false]]");
        [Test()] public void EqualityTest() => TestExpression(@"1==2", "Binary<Equality>[Token[LiteralNumber->1], Token[Equal->==], Token[LiteralNumber->2]]");
        [Test()] public void AssignAssignmentTest() => TestExpression(@"a=3=1", "Binary<Assignment>[Binary<Assignment>[Token[Identifier->a], Token[Assignment->=], Token[LiteralNumber->3]], Token[Assignment->=], Token[LiteralNumber->1]]");
        [Test()] public void AssignmentTest() => TestExpression(@"a=3", "Binary<Assignment>[Token[Identifier->a], Token[Assignment->=], Token[LiteralNumber->3]]");
        [Test()] public void SimpleNumberTest() => TestExpression(@"3", "Token[LiteralNumber->3]");
        [Test()] public void SimpleParanthesizedNumberTest() => TestExpression(@"(3)", "Many[Token[OpenParenthesis->(],Token[LiteralNumber->3],Token[CloseParenthesis->)]]");
    }
}
