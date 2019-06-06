using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Tokenization;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ApolloLanguageCompiler.Parsing.Nodes;

namespace ApolloLanguageCompiler.Tests
{

    [TestFixture()]
    public class TestParser
    {
        [Test()]
        public void ClassTest()
        {
            string code = @"
            hidden instance class Program
            {   
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }
        [Test()]
        public void FunctionTest()
        {
            string code = @"
            hidden instance class Program
            {
                exposed instance func(number a,number b)
                {
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;

        }
        [Test()]
        public void CallFunctionTest()
        {
            string code = @"
            hidden instance class Program
            {
                exposed instance func()
                {
                    func(a)(1)(2)(a) + func();
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;

        }

        [Test()]
        public void GenericsTest()
        {
            string code = @"
            hidden instance class Program
            {
                exposed instance func<T>(T variable)
                {
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }


        [Test()]
        public void ExpressionTest()
        {
            string code = @"
            hidden instance class Program
            {
                exposed instance func()
                {
                    number n = 3+2/3;
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }

        [Test()]
        public void VariableDeclarationTest()
        {
            string code = @"
            hidden instance class Program
            {   
                exposed instance number GetNumber()
                {
                    number a = 3;
                    a = a + 4;
                }

            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }

        [Test()]
        public void VariableAssignmentTest()
        {
            string code = @"
            hidden instance class Program
            {   
                exposed instance SetNumber()
                {
                    number a;
                    a = 4;
                }

            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }


        [Test()]
        public void AccessTest()
        {
            string code = @"
            hidden instance class Program
            {
                exposed instance func()
                {
                    math:tan(90,2);
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }
        [Test()]
        public void GenericPreAccessTest()
        {
            string code = @"
            hidden instance class System:Collection:Generic:List<T>
            {
                exposed instance T System:Collection:Generic:List:Get<T>()
                {
                    
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }
        [Test()]
        public void GenericPostAccessTest()
        {
            string code = @"
            hidden instance class a
            {
                exposed instance func()
                {
                    List<T>:default;
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }

        [Test()]
        public void ReturnTest()
        {
            string code = @"
            hidden instance class Program
            {
                exposed instance GetNumber(number i)
                {
                    return i;
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }

        [Test()]
        public void ReturnAfterExpressionTest()
        {
            string code = @"
            hidden instance class Program
            {
                exposed instance GetNumber()
                {
                    1;
                    return;
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }
        [Test()]
        public void ReturnBeforeExpressionTest()
        {
            string code = @"
            hidden instance class Program
            {
                exposed instance GetNumber()
                {
                    return;
                    1;
                }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }
        [Test()]
        public void PrimitiveTypeTest()
        {
            string code = @"
            exposed instance class Program
            {
               main()
               {
               number b = 6;
               str s = ""abc"";
               letter c = 'a';
               boolean b = true;
               }
            }";
            ASTNode tree = new Compiler(new SourceCode(code)).AbstractSyntaxTree;
        }


    }

}
