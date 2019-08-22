using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Operators
{
    public abstract class BinaryExpression : OperatorExpression, IEmitsFor<GroboIL, CompilationState>
    {
        public interface IOperable
        {
            TypeExpression Operate(BinaryExpression binaryExpression, TypeExpression other);
        }
        public override TypeExpression Type => (this.Left.Type as BinaryExpression.IOperable)?.Operate(this, this.Right.Type) ?? TypeExpression.Illegal;

        public ExpressionIR Left { get; private set; }
        public ExpressionIR Right { get; private set; }

        protected BinaryExpression(BinaryOperatorElement element, IR parent) : base(element, parent)
        {
            this.Left = ExpressionIR.TransformExpression(element.Left, this);
            this.Right = ExpressionIR.TransformExpression(element.Right, this);
        }

        public static BinaryExpression Transform(BinaryOperatorElement element, IR parent)
        {
            switch (element)
            {
                case AdditionElement additionElement:
                    switch (additionElement.Operator)
                    {
                        case AdditionElement.AdditionOperator.Plus: return new AdditionExpression(additionElement, parent);
                        case AdditionElement.AdditionOperator.Minus: return new SubtractionExpression(additionElement, parent);
                    }
                    break;
                case AssignmentElement assignmentElement: return new AssignmentExpression(assignmentElement, parent);
                case CallFunctionElement callFunctionElement: return new CallExpression(callFunctionElement, parent);
                case NamespaceElement namespaceElement: return new NamespaceExpression(namespaceElement, parent);
            }
            throw new UnexpectedNodeException(element.ToString());
        }
        public abstract void EmitFor(GroboIL il, CompilationState state);

        public void EmitSides(GroboIL il, CompilationState state)
        {
            this.Left.TryEmit(il, state, tryLessParams: true, throwOnFailure: true);
            this.Right.TryEmit(il, state, tryLessParams: true, throwOnFailure: true);
        }

        public override IEnumerable<ExpressionIR> Flattened => this.Left.Flattened.Concat(this.Right.Flattened);
    }
}