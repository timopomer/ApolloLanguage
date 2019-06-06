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
            SourceCode code = new SourceCode(
@"hidden instance class Program
{   

}");
            Compiler compiler1 = new Compiler(code, options.OutFile);
            compiler1.Compile();
            Compiler compiler = new Compiler(SourceCode.FromFiles(options.InputFiles), options.OutFile);
            compiler.Compile();
        }
    }
}