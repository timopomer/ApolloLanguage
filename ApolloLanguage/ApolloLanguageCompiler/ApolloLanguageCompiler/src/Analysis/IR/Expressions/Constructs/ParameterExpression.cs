using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.Parsing.Nodes.Action.Expression.Elements.Constructs;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Constructs
{
    public class ParameterExpression : ConstructExpression
    {
        public List<ExpressionIR> Parameters { get; private set; }
        public ParameterExpression(ParameterElement element, IR parent) : base(element, parent)
        {
            this.Parameters = element.Parameters.Select(n => ExpressionIR.TransformExpression(n, parent)).ToList();
        }
        public ParameterExpression(FunctionIR element, IR parent) : base(element, parent)
        {
            this.Parameters = element.Parameters.Select(n => n.ParameterType.Expression).ToList();
        }

        public override TypeExpression Type => new ParameterType(this, this.Parent);
    }
}
