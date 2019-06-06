using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Parsing.Nodes;
using System.Linq;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using System.Reflection.Emit;
using System.Reflection;
using GrEmit;
using ApolloLanguageCompiler.CodeGeneration;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class ClassIR : TypeExpression, IEquatable<ClassIR>, BinaryExpression.IOperable, IDeclares<Identifier>
    {
        public TypeIR Identifier { get; }

        public IEnumerable<FunctionIR> Methods => this.Scope.OfType<FunctionIR>();
        public IEnumerable<VariableDeclarationIR> Members => this.Scope.OfType<VariableDeclarationIR>();

        public ClassIR(ClassNode CNode, IR parent) : base(CNode, parent)
        {
            CNode.CodeBlock.Actions.ForEach(n => Transform(n, this));
            this.Identifier = new TypeIR(CNode.Identifier, this);
        }
        public TypeExpression Operate(BinaryExpression binaryExpression, TypeExpression other)
        {
            //todo: implement accessing static members
            return TypeExpression.Illegal;
        }

        public override bool Equals(object obj) => this.Equals(obj as ClassIR);

        public bool Equals(ClassIR other)
        {
            if (other is null)
                return false;

            return this.Identifier.Equals(other.Identifier);
        }

        public override bool IsLegal => this.Methods.All(n => n.IsLegal) && this.Members.All(n => n.IsLegal);

        public bool Declares(Identifier identifier) => (this.Identifier.Expression as Identifier).Equals(identifier);
        public TypeExpression TypeOf(Identifier identifier) => this.Declares(identifier) ? this.Type : throw new ElementNotDefinedException();

        public void EmitFor(ModuleBuilder containingModule)
        {
            CompilationState state = new CompilationState();

            TypeBuilder type;
            switch (this.Identifier.Expression)
            {
                case Identifier identifier:
                    type = containingModule.DefineType(identifier.Value, TypeAttributes.Public | TypeAttributes.Class);
                    break;
                //add generic methods
                default:
                    throw new Exception("Illegal class type");
            }
            foreach (FunctionIR method in this.Methods)
            {
                method.EmitFor(type, state);
            }
            type.CreateType();
        }
    }
}
