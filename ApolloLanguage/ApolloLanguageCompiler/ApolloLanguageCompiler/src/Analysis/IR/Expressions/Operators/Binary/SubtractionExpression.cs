using System;
using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using GrEmit;

namespace ApolloLanguageCompiler.Analysis.IR.Expressions.Operators
{
    public class SubtractionExpression : BinaryExpression, IEmitsFor<GroboIL, CompilationState>
    {
        public SubtractionExpression(AdditionElement element, IR parent) : base(element, parent)
        {
            throw new NotImplementedException("no subtraction node yet");
        }

        public override void EmitFor(GroboIL il, CompilationState state)
        {
            this.EmitSides(il, state);
            il.Sub();
        }
    }
}
