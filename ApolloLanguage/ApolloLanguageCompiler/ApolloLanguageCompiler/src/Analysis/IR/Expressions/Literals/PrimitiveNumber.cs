using System;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Literals
{
    public class PrimitiveNumber : LiteralExpression, IEquatable<PrimitiveNumber>, IEmitsFor<GroboIL>
    {
        private NumberType innerType;
        public int Value { get; private set; }

        public override TypeExpression Type => this.innerType;

        public PrimitiveNumber(PrimaryElement.LiteralElement element, IR parent) : base(element, parent)
        {
            this.innerType = new NumberType(element, parent);
            this.Value = int.Parse(element.Value);
        }
        public override bool Equals(object obj) => this.Equals(obj as PrimitiveNumber);

        public bool Equals(PrimitiveNumber other)
        {
            if (other is null)
                return false;

            return this.Value.Equals(other.Value);
        }

        public void EmitFor(GroboIL il) => il.Ldc_I4(this.Value);
    }
}