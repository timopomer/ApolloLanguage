using System;
using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing.Nodes;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Operators
{
    public class AssignmentExpression : BinaryExpression
    {
        public AssignmentExpression(AssignmentElement element, IR parent) : base(element, parent)
        {
        }
        public override void EmitFor(GroboIL il, CompilationState state)
        {
            this.Right.TryEmit(il, state, Stack.Store, tryLessParams: true, throwOnFailure: true);
            this.Left.TryEmit(il, state, Stack.Load, tryLessParams: false, throwOnFailure: true);
        }
    }
}
