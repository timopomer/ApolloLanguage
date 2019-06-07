using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.Parsing.Nodes;
using ApolloLanguageCompiler.Parsing.ParserGenerator;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler
{
    public class Compiler
    {
        public SourceCode Source { get; }
        public string OutFile { get; }
        public Compiler(SourceCode source, string outFile = null)
        {
            this.Source = source;
            this.OutFile = outFile;
        }
        public void Compile()
        {
            TokenWalker walker = new TokenWalker(this.Tokens);
            Parsers.Parse(out Node node, walker);
            Console.WriteLine(node.PrettyPrint(0));
            ASTNode AbstractSyntaxTree = new ASTFactory(Tokens).Generate();
            ProgramIR IR = new ProgramIR(AbstractSyntaxTree as ProgramNode);
            AssemblyBuilder Compiled = new CodeGenerator(IR, this.OutFile).Compile();

            Compiled.Save(this.OutFile);
        }

        public IEnumerable<Token> Tokens => new TokenFactory(this.Source);
        public TokenWalker Walker => new TokenWalker(this.Tokens);
        public ASTNode AbstractSyntaxTree => new ASTFactory(this.Tokens).Generate();
        public Node ParseTree
        {
            get
            {
                Parsers.Parse(out Node node, this.Walker);
                return node;
            }
        }

        public ProgramIR IR => new ProgramIR(this.AbstractSyntaxTree as ProgramNode);
        public AssemblyBuilder Compiled => new CodeGenerator(this.IR, this.OutFile).Compile();

    }
}
