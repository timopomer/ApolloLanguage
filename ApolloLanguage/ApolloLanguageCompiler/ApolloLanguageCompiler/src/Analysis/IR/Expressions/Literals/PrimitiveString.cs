using System;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Literals
{
    public class PrimitiveString : LiteralExpression, IEquatable<PrimitiveNumber>, IEmitsFor<GroboIL>
    {
        private StringType innerType;
        public string Value { get; private set; }

        public override TypeExpression Type => this.innerType;

        public PrimitiveString(PrimaryElement.LiteralElement element, IR parent) : base(element, parent)
        {
            this.innerType = new StringType(element, parent);
            this.Value = element.Value;
        }
        public override bool Equals(object obj) => this.Equals(obj as PrimitiveNumber);

        public bool Equals(PrimitiveNumber other)
        {
            if (other is null)
                return false;

            return this.Value.Equals(other.Value);
        }

        public void EmitFor(GroboIL il) => il.Ldstr(this.Value);
    }
}