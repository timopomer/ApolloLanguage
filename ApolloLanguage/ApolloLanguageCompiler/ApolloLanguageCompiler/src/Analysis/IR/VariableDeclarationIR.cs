using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class VariableDeclarationIR : ExpressionIR, IDeclares<Identifier>, IEmitsFor<GroboIL,CompilationState>
    {
        public TypeIR DeclaredType { get; private set; }
        public Identifier Assigned { get; private set; }
        public ExpressionIR Expression { get; private set; }

        public VariableDeclarationIR(VariableDeclarationNode VDNode, IR parent) : base(VDNode, parent)
        {
            this.DeclaredType = new TypeIR(VDNode.Type, this);
            //todo: add empty decleration support (number a;)
            AssignmentExpression assignment = ExpressionIR.TransformExpression(VDNode.Expression.Expression, this) as AssignmentExpression ?? throw new WrongExpressionTypeException();
            this.Assigned = assignment.Left as Identifier ?? throw new WrongExpressionTypeException();
            this.Expression = assignment.Right;
        }

        public bool Declares(Identifier identifier) => this.Assigned.Equals(identifier);

        public TypeExpression TypeOf(Identifier identifier) => this.Declares(identifier) ? this.DeclaredType : throw new ElementNotDefinedException();

        public void EmitFor(GroboIL il, CompilationState state)
        {
            GroboIL.Local local = il.DeclareLocal(this.DeclaredType.ILType, this.Assigned.Value);
            state.Add(local, this.Assigned.Value);
            this.Expression.TryEmit(il, state, tryLessParams: true, throwOnFailure: true);
            this.Assigned.EmitFor(il, state, Stack.Store);
        }

        public override bool IsLegal => this.DeclaredType.Type.Equals(this.Expression.Type);

        public override TypeExpression Type => throw new NotImplementedException();
    }
}
