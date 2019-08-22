using ApolloLanguageCompiler.Tokenization;
using System.Collections.Generic;
using System.Linq;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract partial class PrimaryElement
    {
        public class IdentifierElement : PrimaryElement
        {
            public override object Clone() => new IdentifierElement(this);

            public string Value { get; private set; }

            public IdentifierElement(string value)
            {
                this.Value = value;
            }

            public IdentifierElement(IdentifierElement element)
            {
                this.Value = element.Value;
            }

            public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
            {
                TokenWalker LocalWalker = new TokenWalker(walker);
                walk = LocalWalker.State;
                SourceContext Context = LocalWalker.Context;

                expression = null;

                if (LocalWalker.TryGetNext(out Token IdentifierToken, SyntaxKeyword.Identifier))
                {
                    expression = new IdentifierElement(IdentifierToken.Value);
                    expression.SetContext(Context, LocalWalker);
                    return true;
                }
                return false;
            }
        }
    }
}
