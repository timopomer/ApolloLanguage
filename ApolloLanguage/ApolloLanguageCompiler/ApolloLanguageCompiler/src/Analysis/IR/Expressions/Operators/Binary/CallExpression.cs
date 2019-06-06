using System;
using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing.Nodes;
using GrEmit;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Constructs;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Operators
{
    public class CallExpression : BinaryExpression
    {
        private readonly ExpressionIR function;
        private readonly ParameterExpression parameters;

        public CallExpression(CallFunctionElement element, IR parent) : base(element, parent)
        {
            this.function = this.Left;
            this.parameters = this.Right as ParameterExpression;
        }

        public override void EmitFor(GroboIL il, CompilationState state)
        {
            
        }
    }
}
