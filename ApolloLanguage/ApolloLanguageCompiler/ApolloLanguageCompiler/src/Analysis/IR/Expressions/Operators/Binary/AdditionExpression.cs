using System;
using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Operators
{
    public class AdditionExpression : BinaryExpression, IEmitsFor<GroboIL, CompilationState>
    {
        public AdditionExpression(AdditionElement element, IR parent) : base(element, parent)
        {

        }

        public override void EmitFor(GroboIL il, CompilationState state)
        {
            this.EmitSides(il, state);
            il.Add();
        }
    }
}
