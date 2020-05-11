using System.Collections.Generic;
using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.Tokenization;
using CommandLine;
using System;
using Crayon;

namespace ApolloLanguageCompiler.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Output.Green($"green {Output.Red($"{Output.Bold("bold")} red")} green"));

            SourceCode s = new SourceCode(@"
            hidden instance class Program
        {
        }");
            Compiler compiler = new Compiler(s, null);
            compiler.Compile();
            //Parser.Default.ParseArguments<CompilationOptions>(args)
            //    .WithParsed<CompilationOptions>(RunOptionsAndReturnExitCode)
            //    .WithNotParsed<CompilationOptions>(HandleParseError);
        }

        //private static void HandleParseError(IEnumerable<Error> errors)
        //{
        //}

        private static void RunOptionsAndReturnExitCode(CompilationOptions options)
        {
            SourceCode s = new SourceCode(@"
            hidden instance class Program
        {
        }");
            Compiler compiler = new Compiler(s, null);
            compiler.Compile();
        }
    }
}