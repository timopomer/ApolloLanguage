using NUnit.Framework;
using System;
using System.Linq;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.CLI;
using ApolloLanguageCompiler.Source;
using ApolloLanguageCompiler.Analysis;

namespace ApolloLanguageCompiler.Tests
{
    [TestFixture()]
    public class TestIRBuilder
    {
        public static void TestIR(string representation)
        {
            SourceCode source = new SourceCode(representation);
            TokenWalker walker = new Compiler(source).Walker;
            NodeParser nodeParser = Parsers.Node.Program;
            Node node = null;
            Console.WriteLine(source.WithLineNumbers());

            nodeParser.Parse(ref node, walker, out ParseResultHistory resultHistory);

            Console.WriteLine(node);
            Assert.IsNotNull(node);
            Assert.IsTrue(walker.IsLast());
            IR ir = IR.ParseNode(node);
            Assert.IsInstanceOf<ProgramIR>(ir);
            Console.WriteLine(ir);
        }

        [Test()]
        public void ClassTest() => TestIR(
            @"hidden instance class Program
            {
            }");


        [Test()]
        public void FunctionTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                }
            }
            ");



        [Test()]
        public void FunctionWithParameterTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance func(number a)
                {
                }
            }");

        [Test()]
        public void FunctionWithMultipleParameterTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance func(number a, number b)
                {
                }
            }");

        [Test()]
        public void CallFunctionTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    func(a)(1)(2)(a) + func();
                }
            }");

        [Test]
        public void GenericsTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance func<T>(T variable)
                {
                }
            }");

        [Test()]
        public void ExpressionTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    3+2/3;
                }
            }");

        [Test()]
        public void VariableDeclarationTest() => TestIR(@"
            hidden instance class Program
            {   
                exposed instance GetNumber()
                {
                    number a = 3;
                    a = a + 4;
                }

            }");

        [Test()]
        public void VariableAssignmentTest() => TestIR(@"
            hidden instance class Program
            {   
                exposed instance SetNumber()
                {
                    number a;
                    a = 4;
                }

            }");

        [Test()]
        public void AccessTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    math:tan(90,2);
                }
            }");

        [Test()]
        public void GenericPreAccessTest() => TestIR(@"
            hidden instance class System:Collection:Generic:List<T>
            {
                exposed instance T System:Collection:Generic:List:Get<T>()
                {
                    
                }
            }");

        [Test()]
        public void GenericPostAccessTest() => TestIR(@"
            hidden instance class a
            {
                exposed instance func()
                {
                    List<T>:default;
                }
            }");

        [Test()]
        public void ReturnTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance AFunction()
                {
                    return;
                }
            }");

        [Test()]
        public void ReturnVariableTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance GetNumber(number i)
                {
                    return i;
                }
            }");

        [Test()]
        public void ReturnAfterExpressionTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance GetNumber()
                {
                    1;
                    return;
                }
            }");

        [Test()]
        public void ReturnBeforeExpressionTest() => TestIR(@"
            hidden instance class Program
            {
                exposed instance GetNumber()
                {
                    return;
                    1;
                }
            }");

        [Test()]
        public void PrimitiveTypeTest() => TestIR(@"
            exposed instance class Program
            {
               exposed instance main()
               {
               number b = 6;
               str s = ""abc"";
               letter c = 'a';
               boolean b = true;
               }
            }");

        [Test()]
        public void FunctionWithoutReturnTypeTest() => TestIR(@"
            exposed instance class Program
            {
               exposed instance main()
               {
               }
            }");

        [Test()]
        public void FunctionWithReturnTypeTest() => TestIR(@"
            exposed instance class Program
            {
               exposed instance number main()
               {
               }
            }");

        [Test()]
        public void FunctionWithoutModifiersTypeTest() => TestIR(@"
            exposed instance class Program
            {
               number main()
               {
               }
            }");

        [Test()]
        public void FunctionWithoutModifiersAndReturnTypeTest() => TestIR(@"
            exposed instance class Program
            {
               main()
               {
               }
            }");
    }
}
