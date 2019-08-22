
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class GenericElement : ExpressionElement
    {
        public override object Clone() => new GenericElement(this);

        public ExpressionElement BaseType { get; private set; }
        public ParameterElement Types { get; private set; }

        private GenericElement(ExpressionElement baseType, ParameterElement types)
        {
            this.BaseType = baseType;
            this.Types = types;
        }
        private GenericElement(GenericElement element)
        {
            this.BaseType = element.BaseType.Clone() as ExpressionElement;
            this.Types = element.Types.Clone() as ParameterElement;
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(NamespaceElement.TryParse, out expression, LocalWalker))
                return false;

            while (TryParseParameters(
                out List<ExpressionElement> parameters,
                LocalWalker,
                out StateWalker paramWalker,
                PrimaryElement.TryParse,
                SyntaxKeyword.Lesser,
                SyntaxKeyword.Comma,
                SyntaxKeyword.Greater,
                allowNoParameters: false
            ))
            {
                LocalWalker.Walk(paramWalker);
                expression = new GenericElement(expression, new ParameterElement(parameters, Context));
            }
            return true;
        }
    }
}
