using System;
using System.Collections.Generic;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals.Types;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Parsing;
using GrEmit;
using Newtonsoft.Json;
using BinaryExpression = ApolloLanguageCompiler.Analysis.IR.Expressions.Operators.BinaryExpression;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Types
{
    public class StringType : PrimitiveType, IEquatable<StringType>, BinaryExpression.IOperable
    {
        [JsonIgnore]
        public override TypeExpression Type => this;

        public override Type ILType => typeof(int);

        public StringType(IContainsContext context, IR ir) : base(context, ir)
        {
        }

        public TypeExpression Operate(BinaryExpression binaryExpression, TypeExpression other)
        {
            switch (binaryExpression)
            {
                case AdditionExpression _:
                    switch(other)
                    {
                        case NumberType _: return new StringType(this.Context, this.Parent);
                        case StringType _: return new StringType(this.Context, this.Parent);
                    }
                    break;
            }
            return TypeExpression.Illegal;
        }

        public override bool Equals(object obj) => this.Equals(obj as StringType);

        public bool Equals(StringType other) => !(other is null);
    }
}
