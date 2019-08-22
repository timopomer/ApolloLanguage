


using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class CallFunctionElement : BinaryOperatorElement
    {
        public override object Clone() => new CallFunctionElement(this);

        public ExpressionElement Caller { get; private set; }
        public ParameterElement Arguments { get; private set; }

        private CallFunctionElement(ExpressionElement caller, ParameterElement arguments) : base(caller, arguments)
        {
            this.Caller = caller;
            this.Arguments = arguments;
        }
        private CallFunctionElement(CallFunctionElement element) : base(element)
        {
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(AccessElement.TryParse, out expression, LocalWalker))
                return false;

            while (TryParseParameters(
                out List<ExpressionElement> parameters,
                LocalWalker,
                out StateWalker paramWalker,
                PrimaryElement.TryParse,
                SyntaxKeyword.OpenParenthesis,
                SyntaxKeyword.Comma,
                SyntaxKeyword.CloseParenthesis,
                allowNoParameters: true
            ))
            {
                LocalWalker.Walk(paramWalker);
                expression = new CallFunctionElement(expression, new ParameterElement(parameters, Context));
                expression.SetContext(Context);
            }
            return true;
        }
    }
}