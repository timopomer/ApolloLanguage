using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Constructs;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using GrEmit;
using Newtonsoft.Json;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Types
{
    public class ParameterType : TypeExpression, IEquatable<ParameterType>, BinaryExpression.IOperable
    {
        public List<TypeExpression> Parameters { get; private set; }

        public ParameterType(ParameterExpression parameters, IR ir) : base(parameters.Context, ir)
        {
            this.Parameters = parameters.Parameters.Select(n => n.Type).ToList();
        }

        public TypeExpression Operate(BinaryExpression binaryExpression, TypeExpression other) => TypeExpression.Illegal;


        public override bool Equals(object obj)=>this.Equals(obj as ParameterType);

        public bool Equals(ParameterType other)
        {
            if (other is null)
                return false;
            
            bool areSameCount = this.Parameters.Count() == other.Parameters.Count();
            bool areSameElements = this.Parameters.Zip(other.Parameters, Equals).All(n => n);
            return areSameCount && areSameElements;
        }
    }
}
