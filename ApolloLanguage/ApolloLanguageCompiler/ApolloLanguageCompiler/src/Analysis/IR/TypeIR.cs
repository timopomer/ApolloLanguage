using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals.Types;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.Parsing.Nodes;
using Newtonsoft.Json;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class TypeIR : IR
    {
        public ExpressionIR Expression { get; private set; }

        [JsonIgnore]
        public TypeExpression Type => this.Expression.Type;

        [JsonIgnore]
        public override bool IsLegal => this.Type.Type != TypeExpression.Illegal;

        public static implicit operator TypeExpression(TypeIR typeIR) => typeIR.Type;

        public TypeIR(TypeNode TNode, IR parent) : base(TNode, parent)
        {
            this.Expression = TNode is null ? new VoidType(TNode, parent) : ExpressionIR.TransformExpression(TNode.Expression, this);
        }

        public override bool Equals(object obj)
        {
            TypeIR other = obj as TypeIR;

            if (other is null)
                return false;
            
            return this.Type.Equals(other.Type);
        }
        
        public Type ILType
        {
            get
            {
                switch (this.Type)
                {
                    case PrimitiveType primitive:
                        return primitive.ILType;

                    default: throw new Exception("Type doesnt have IL equivilant");
                }
            }
        }
    }
}
