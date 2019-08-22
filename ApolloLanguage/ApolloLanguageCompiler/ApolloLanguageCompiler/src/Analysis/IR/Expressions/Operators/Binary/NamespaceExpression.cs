using System;
using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Operators
{
    public class NamespaceExpression : BinaryExpression, IEmitsFor<GroboIL, CompilationState>
    {
        public NamespaceExpression(NamespaceElement element, IR parent) : base(element, parent)
        {

        }

        public override void EmitFor(GroboIL il, CompilationState state)
        {

        }
    }
}
