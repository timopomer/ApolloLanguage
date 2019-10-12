using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
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
            Expression expression = null;
            NodeParsers.Parse(ref expression, walker);
            Console.WriteLine(string.Join(Environment.NewLine, expression));
            ASTNode AbstractSyntaxTree = new ASTFactory(Tokens).Generate();
            ProgramIR IR = new ProgramIR(AbstractSyntaxTree as ProgramNode);
            AssemblyBuilder Compiled = new CodeGenerator(IR, this.OutFile).Compile();

            Compiled.Save(this.OutFile);
        }

        public IEnumerable<Token> Tokens => new TokenFactory(this.Source);
        public TokenWalker Walker => new TokenWalker(this.Tokens);
        public ASTNode AbstractSyntaxTree => new ASTFactory(this.Tokens).Generate();
        public Expression ParseTree
        {
            get
            {
                Expression expression = null;
                NodeParsers.Parse(ref expression, this.Walker);
                return expression;
            }
        }

        public ProgramIR IR => new ProgramIR(this.AbstractSyntaxTree as ProgramNode);
        public AssemblyBuilder Compiled => new CodeGenerator(this.IR, this.OutFile).Compile();

    }
}
