using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.CLI;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Tests
{
    [TestFixture()]
    public class TestParser
    {
        public static void TestParsing(string representation, NodeParser parser=null, bool shouldFail = false)
        {
            SourceCode source = new SourceCode(representation);
            TokenWalker walker = new Compiler(source).Walker;
            NodeParser nodeParser = parser ?? Parsers.Node.Program;
            Node node = null;
            nodeParser.Parse(ref node, walker);
            if (shouldFail)
                Assert.IsNull(node);

            Assert.IsNotNull(node);
            Assert.IsTrue(walker.IsLast());
        }

        [Test()]
        public void ClassTest() => TestParsing(@"
            hidden instance class Program
            {   
            }");


        [Test()]
        public void FunctionTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                }
            }
            ");

        [Test()]
        public void PrintParser()
        {
        Console.WriteLine(Parsers.Node.Statements.Function.Deceleration.ToString());
        }
        [Test()]
        public void FunctionWithParameterTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance func(number a)
                {
                }
            }");

        [Test()]
        public void FunctionWithMultipleParameterTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance func(number a, number b)
                {
                }
            }");

        [Test()]
        public void CallFunctionTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    func(a)(1)(2)(a) + func();
                }
            }");

        [Test()]
        public void GenericsTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance func<T>(T variable)
                {
                }
            }");

        [Test()]
        public void ExpressionTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    3+2/3;
                }
            }");

        [Test()]
        public void VariableDeclarationTest() => TestParsing(@"
            hidden instance class Program
            {   
                exposed instance number GetNumber()
                {
                    number a = 3;
                    a = a + 4;
                }

            }");

        [Test()]
        public void VariableAssignmentTest() => TestParsing(@"
            hidden instance class Program
            {   
                exposed instance SetNumber()
                {
                    number a;
                    a = 4;
                }

            }");

        [Test()]
        public void AccessTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    math:tan(90,2);
                }
            }");

        [Test()]
        public void GenericPreAccessTest() => TestParsing(@"
            hidden instance class System:Collection:Generic:List<T>
            {
                exposed instance T System:Collection:Generic:List:Get<T>()
                {
                    
                }
            }");

        [Test()]
        public void GenericPostAccessTest() => TestParsing(@"
            hidden instance class a
            {
                exposed instance func()
                {
                    List<T>:default;
                }
            }");

        [Test()]
        public void ReturnTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance AFunction()
                {
                    return;
                }
            }");

        [Test()]
        public void ReturnVariableTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance GetNumber(number i)
                {
                    return i;
                }
            }");

        [Test()]
        public void ReturnAfterExpressionTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance GetNumber()
                {
                    1;
                    return;
                }
            }");

        [Test()]
        public void ReturnBeforeExpressionTest() => TestParsing(@"
            hidden instance class Program
            {
                exposed instance GetNumber()
                {
                    return;
                    1;
                }
            }");

        [Test()]
        public void PrimitiveTypeTest() => TestParsing(@"
            exposed instance class Program
            {
               main()
               {
               number b = 6;
               str s = ""abc"";
               letter c = 'a';
               boolean b = true;
               }
            }");
    }
}
