using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing.Nodes;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class ReturnIR : IR, IEmitsFor<GroboIL, CompilationState>
    {
        public ExpressionIR Expression { get; private set; }
        public TypeExpression Type => this.Expression.Type;
        public ReturnIR(ReturnNode RNode, IR parent) : base(RNode, parent)
        {
            this.Expression = ExpressionIR.TransformExpression(RNode.Expression.Expression, this);
        }

        public void EmitFor(GroboIL il, CompilationState state)
        {
            this.Expression.TryEmit(il, state, tryLessParams: true, throwOnFailure: true);
            il.Ret();
        }
    }
}
