using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals.Types;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Parsing.Nodes;
using GrEmit;
using Newtonsoft.Json;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Types
{
    public class NumberType : PrimitiveType, IEquatable<NumberType>, BinaryExpression.IOperable
    {
        [JsonIgnore]
        public override TypeExpression Type => this;

        public override Type ILType => typeof(int);

        public NumberType(IContainsContext context, IR ir) : base(context, ir)
        {
        }

        public TypeExpression Operate(BinaryExpression binaryExpression, TypeExpression other)
        {
            switch (binaryExpression)
            {
                case AdditionExpression _:
                    switch(other)
                    {
                        case NumberType _ : return new NumberType(this.Context, this.Parent);
                    }
                    break;
                case SubtractionExpression _:
                    switch (other)
                    {
                        case NumberType _: return new NumberType(this.Context, this.Parent);
                    }
                    break;
            }
            return TypeExpression.Illegal;
        }

        public override bool Equals(object obj) => this.Equals(obj as NumberType);

        public bool Equals(NumberType other) => !(other is null);
    }
}
