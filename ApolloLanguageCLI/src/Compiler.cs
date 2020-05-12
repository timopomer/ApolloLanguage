
using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace ApolloLanguageCompiler.CLI
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
            Node expression = null;
            Parsers.Node.Parse(ref expression, walker);
            Console.WriteLine(expression);
            Console.WriteLine(Parsers.Node.Program.ToStringRecursively());

            //ASTNode AbstractSyntaxTree = new ASTFactory(Tokens).Generate();
            //ProgramIR IR = new ProgramIR(AbstractSyntaxTree as ProgramNode);
            //AssemblyBuilder Compiled = new CodeGenerator(IR, this.OutFile).Compile();

            //Compiled.Save(this.OutFile);
        }

        public IEnumerable<Token> Tokens => new TokenFactory(this.Source);
        public TokenWalker Walker => new TokenWalker(this.Tokens);
        //public ASTNode AbstractSyntaxTree => new ASTFactory(this.Tokens).Generate();
        public Node ParseTree
        {
            get
            {
                Node expression = null;
                Parsers.Node.Parse(ref expression, this.Walker);
                return expression;
            }
        }

        //public ProgramIR IR => new ProgramIR(this.AbstractSyntaxTree as ProgramNode);
        //public AssemblyBuilder Compiled => new CodeGenerator(this.IR, this.OutFile).Compile();

    }
}
