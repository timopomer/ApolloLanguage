using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Constructs;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using GrEmit;
using BinaryExpression = ApolloLanguageCompiler.Analysis.IR.Expressions.Operators.BinaryExpression;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class FunctionIR : TypeExpression, BinaryExpression.IOperable, IDeclares<Identifier>, IEquatable<FunctionIR>, IEmitsFor<TypeBuilder, CompilationState>
    {
        public ModifierIR Modifier { get; private set; }
        public TypeIR Identifier { get; private set; }
        public TypeIR Return { get; private set; }
        public IEnumerable<FunctionParameterExpression> Parameters { get; private set; }
        public IEnumerable<IR> Actions { get; private set; }

        public FunctionIR(FunctionNode FNode, IR parent) : base(FNode, parent)
        {
            this.Modifier = new ModifierIR(FNode.Modifier, this);
            this.Identifier = new TypeIR(FNode.Identifier, this);
            this.Return = new TypeIR(FNode.ReturnType, this);
            this.Parameters = FNode.Parameters.Select(n => new FunctionParameterExpression(n, this)).ToList();
            this.Actions = FNode.CodeBlock.Actions.Select(n => IR.Transform(n, this)).ToList();
        }

        public TypeExpression Operate(BinaryExpression binaryExpression, TypeExpression other)
        {
            switch (binaryExpression)
            {
                case CallExpression _:
                    switch (other)
                    {
                        case ParameterType otherParmeters: return this.Parameters.Equals(otherParmeters) ? this.Return : TypeExpression.Illegal;
                    }
                    break;
            }
            return TypeExpression.Illegal;
        }

        public override bool Equals(object obj) => this.Equals(obj as FunctionIR);

        public bool Equals(FunctionIR other)
        {
            if (other is null)
                return false;

            return this.Return.Equals(other.Return) && this.Parameters.Equals(other.Parameters);
        }

        public IEnumerable<ReturnIR> ReturnIRs => this.Scope.RecursiveInside.OfType<ReturnIR>();

        private bool HasLegalReturn => this.ReturnIRs.All(n => n.Type.Equals(this.Return.Type));
        private bool AllActionsLegal => this.Actions.All(n => n.IsLegal);



        public override bool IsLegal => this.HasLegalReturn && this.AllActionsLegal;

        public bool Declares(Identifier identifier) => (this.Identifier.Expression as Identifier).Equals(identifier);
        public TypeExpression TypeOf(Identifier identifier) => this.Declares(identifier) ? this.Type : throw new ElementNotDefinedException();

        public void EmitFor(TypeBuilder containingClass, CompilationState state)
        {
            MethodBuilder method;
            state = new CompilationState(parent: state);

            switch (this.Identifier.Expression)
            {
                case Identifier identifier:
                    method = containingClass.DefineMethod(
                        identifier.Value,
                        this.Modifier.ToAttribute(),
                        this.Return.ILType,
                        this.Parameters.Select(n => n.ParameterType.ILType).ToArray()
                    );
                    break;
                //add generic methods
                default:
                    throw new Exception("Illegal function type");
            }
            //emit actual opcodes
            GroboIL il = new GroboIL(method);
            foreach (IR ir in this.Actions)
            {
                ir.TryEmit(il, state, tryLessParams: true, throwOnFailure: true);
            }
            il.Call(method.GetBaseDefinition());
            Console.WriteLine(il.GetILCode());
            il.Ret();
        }
    }
}
