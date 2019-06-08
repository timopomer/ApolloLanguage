using System.Collections.Generic;
using ApolloLanguageCompiler.Parsing.ParserGenerator;
using CommandLine;

namespace ApolloLanguageCompiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CompilationOptions>(args)
                .WithParsed<CompilationOptions>(RunOptionsAndReturnExitCode)
                .WithNotParsed<CompilationOptions>(HandleParseError);
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
        }

        private static void RunOptionsAndReturnExitCode(CompilationOptions options)
        {
            Compiler compiler = new Compiler(SourceCode.FromFiles(options.InputFiles), options.OutFile);
            compiler.Compile();
        }
    }
}