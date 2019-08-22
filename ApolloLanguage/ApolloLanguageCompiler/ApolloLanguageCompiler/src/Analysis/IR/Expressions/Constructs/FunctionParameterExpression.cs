using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.Parsing;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Constructs
{
    public class FunctionParameterExpression : ConstructExpression
    {
        public TypeIR ParameterType { get; private set; }
        public ExpressionIR Parameter { get; private set; }

        public override TypeExpression Type => TypeExpression.Illegal;

        public FunctionParameterExpression(FunctionParameterNode element, IR parent) : base(element, parent)
        {
            this.ParameterType = new TypeIR(element.Type, this);
            this.Parameter = ExpressionIR.TransformExpression(element.Expression, this);
        }
    }
}
