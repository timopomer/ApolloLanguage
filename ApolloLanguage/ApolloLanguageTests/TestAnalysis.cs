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
    public class TestAnalysis
    {
        [Test()]
        public void ProgramTest()
        {
            string code = @"
hidden instance class Program
{   
}";
            SourceCode source = new SourceCode(code);
            Compiler compiler = new Compiler(source);
            var all = compiler.IR.Scope.RecursiveInside;
        }

        [Test()]
        public void FunctionTest()
        {
            string code = @"
hidden instance class Program
{   
    exposed instance func1()
    {
    }
}";
            SourceCode source = new SourceCode(code);
            Compiler compiler = new Compiler(source);
            var all = compiler.IR.Scope.RecursiveInside;
        }
    }
 }
