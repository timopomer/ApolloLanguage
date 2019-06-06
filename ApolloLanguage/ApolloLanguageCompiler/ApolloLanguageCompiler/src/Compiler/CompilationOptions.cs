using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler
{
    public class CompilationOptions
    {
        [Option('f', "files", Required = true, HelpText = "Input files to be compiled.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option(Default = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option("out", HelpText = "Output filename")]
        public string OutFile { get; set; }
    }
}
