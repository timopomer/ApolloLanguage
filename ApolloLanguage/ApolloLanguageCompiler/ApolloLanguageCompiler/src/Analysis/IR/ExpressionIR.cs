using System;
using System.Diagnostics.Contracts;
using ApolloLanguageCompiler.Analysis.IR.Expressions;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Operators;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.Parsing.Nodes;
using Newtonsoft.Json;
using GrEmit;
using System.Collections.Generic;

namespace ApolloLanguageCompiler.Analysis.IR.Expression
{
    public abstract class ExpressionIR : IR
    {
        [JsonIgnore]
        public abstract TypeExpression Type { get; }

        [JsonIgnore]
        public virtual IEnumerable<ExpressionIR> Flattened => new[] { this };

        public bool IsType() => this is TypeExpression;

        protected ExpressionIR(IContainsContext context, IR parent) : base(context, parent)
        {
        }

        public static ExpressionIR TransformExpression(ExpressionElement element, IR parent)
        {
            switch (element)
            {
                case OperatorElement operatorElement: return OperatorExpression.Transform(operatorElement, parent);
                case PrimaryElement primaryElement: return LiteralExpression.Transform(primaryElement, parent);
                case ConstructElement constructElement: return ConstructExpression.Transform(constructElement, parent);
            }
            throw new UnexpectedNodeException(element.ToString());
        }
    }
}
