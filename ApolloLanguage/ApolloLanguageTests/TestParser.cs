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
    public class TestParser
    {
        public static void TestProgram(string representation, bool shouldFail = false)
        {
            SourceCode source = new SourceCode(representation);
            TokenWalker walker = new Compiler(source).Walker;
            Expression expression = null;
            NodeParsers.Parse(ref expression, walker);
            if (shouldFail)
                Assert.IsNull(expression);

            Assert.IsNotNull(expression);
            Assert.IsTrue(walker.IsLast());
        }

        [Test()]
        public void ClassTest() => TestProgram(@"
            hidden instance class Program
            {   
            }");


        [Test()]
        public void FunctionTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                }
            }");

        [Test()]
        public void FunctionWithParameterTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance func(number a)
                {
                }
            }");

        [Test()]
        public void FunctionWithMultipleParameterTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance func(number a, number b)
                {
                }
            }");

        [Test()]
        public void CallFunctionTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    func(a)(1)(2)(a) + func();
                }
            }");

        [Test()]
        public void GenericsTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance func<T>(T variable)
                {
                }
            }");

        [Test()]
        public void ExpressionTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    3+2/3;
                }
            }");

        [Test()]
        public void VariableDeclarationTest() => TestProgram(@"
            hidden instance class Program
            {   
                exposed instance number GetNumber()
                {
                    number a = 3;
                    a = a + 4;
                }

            }");

        [Test()]
        public void VariableAssignmentTest() => TestProgram(@"
            hidden instance class Program
            {   
                exposed instance SetNumber()
                {
                    number a;
                    a = 4;
                }

            }");

        [Test()]
        public void AccessTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance func()
                {
                    math:tan(90,2);
                }
            }");

        [Test()]
        public void GenericPreAccessTest() => TestProgram(@"
            hidden instance class System:Collection:Generic:List<T>
            {
                exposed instance T System:Collection:Generic:List:Get<T>()
                {
                    
                }
            }");

        [Test()]
        public void GenericPostAccessTest() => TestProgram(@"
            hidden instance class a
            {
                exposed instance func()
                {
                    List<T>:default;
                }
            }");

        [Test()]
        public void ReturnTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance AFunction()
                {
                    return;
                }
            }");

        [Test()]
        public void ReturnVariableTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance GetNumber(number i)
                {
                    return i;
                }
            }");

        [Test()]
        public void ReturnAfterExpressionTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance GetNumber()
                {
                    1;
                    return;
                }
            }");

        [Test()]
        public void ReturnBeforeExpressionTest() => TestProgram(@"
            hidden instance class Program
            {
                exposed instance GetNumber()
                {
                    return;
                    1;
                }
            }");

        [Test()]
        public void PrimitiveTypeTest() => TestProgram(@"
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
