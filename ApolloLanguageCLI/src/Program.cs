using System.Collections.Generic;
using ApolloLanguageCompiler.Parsing;
using CommandLine;
using System;
using Crayon;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SourceCode s = new SourceCode(@"
            hidden instance class Program
            {
                exposed instance func(number a)
                {
                }
            }");
            Compiler compiler = new Compiler(s, null);
            compiler.Compile();
            //Parser.Default.ParseArguments<CompilationOptions>(args)
            //    .WithParsed<CompilationOptions>(RunOptionsAndReturnExitCode)
            //    .WithNotParsed<CompilationOptions>(HandleParseError);
            Console.WriteLine(Output.Green($"green {Output.Red($"{Output.Bold("bold")} red")} green"));

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