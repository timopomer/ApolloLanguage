using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Parsing.Nodes;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class ProgramIR : IR
    {
        public IEnumerable<ClassIR> Classes => this.Scope.OfType<ClassIR>();
        public override bool IsLegal => this.Classes.All(n => n.IsLegal);

        public ProgramIR(ProgramNode PNode) : base(PNode, null)
        {
            PNode.Classes.ForEach(n => new ClassIR(n, this));
        }
    }
}
