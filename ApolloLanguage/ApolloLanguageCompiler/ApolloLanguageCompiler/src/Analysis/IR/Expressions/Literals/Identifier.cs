using System;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using GrEmit;
using ApolloLanguageCompiler.CodeGeneration;
using static ApolloLanguageCompiler.Parsing.PrimaryElement;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Literals
{
    public enum Stack
    {
        Load,
        Store
    }
    public class Identifier : LiteralExpression, IEmitsFor<GroboIL, CompilationState, Stack>, IEmitsFor<GroboIL, CompilationState>
    {
        public string Value { get; private set; }

        [JsonIgnore]
        public override TypeExpression Type => this.Scope.GetDeclaration(this).Type;

        public Identifier(IdentifierElement element, IR parent) : base(element, parent)
        {
            this.Value = element.Value;
        }
        public override bool Equals(object obj)
        {
            Identifier other = obj as Identifier;

            if (other is null)
                return false;

            return this.Value.Equals(other.Value);
        }

        public void EmitFor(GroboIL il, CompilationState state, Stack stack)
        {
            GroboIL.Local local = state.GetLocalByName(this.Value);
            switch (stack)
            {
                case Stack.Load:
                    il.Ldloc(local);
                    break;
                case Stack.Store:
                    il.Stloc(local);
                    break;
                default:
                    throw new Exception("Illegal identifier operation exception");
            }
        }

        public void EmitFor(GroboIL il, CompilationState state) => this.EmitFor(il, state, Stack.Load); //default implementation
    }
}