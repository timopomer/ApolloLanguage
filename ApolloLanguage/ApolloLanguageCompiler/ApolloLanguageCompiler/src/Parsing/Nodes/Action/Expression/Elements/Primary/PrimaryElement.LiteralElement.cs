using ApolloLanguageCompiler.Tokenization;
using System.Collections.Generic;
using System.Linq;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing.Nodes
{
    public abstract partial class PrimaryElement
    {
        public class LiteralElement : PrimaryElement
        {
            public override object Clone() => new LiteralElement(this);

            public string Value { get; private set; }
            public Literal Type { get; private set; }

            public enum Literal
            {
                Str,
                Letter,
                Number,
                Boolean,
            }

            public static readonly Dictionary<SyntaxKeyword, Literal> TypeConverter = new Dictionary<SyntaxKeyword, Literal>
            {
                {SyntaxKeyword.LiteralNumber,       Literal.Number      },
                {SyntaxKeyword.LiteralLetter,       Literal.Letter      },
                {SyntaxKeyword.LiteralStr,          Literal.Str         },
                {SyntaxKeyword.LiteralTrue,         Literal.Boolean     },
                {SyntaxKeyword.LiteralFalse,        Literal.Boolean     }
            };

            public LiteralElement(Literal type, string value)
            {
                this.Type = type;
                this.Value = value;
            }

            public LiteralElement(LiteralElement element)
            {
                this.Type = element.Type;
                this.Value = element.Value;
            }
            public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
            {
                TokenWalker LocalWalker = new TokenWalker(walker);
                walk = LocalWalker.State;
                SourceContext Context = LocalWalker.Context;

                expression = null;

                if (LocalWalker.TryGetNext(out Token LiteralToken, SyntaxKeyword.LiteralNumber, SyntaxKeyword.LiteralLetter, SyntaxKeyword.LiteralStr, SyntaxKeyword.LiteralTrue, SyntaxKeyword.LiteralFalse))
                {
                    expression = new LiteralElement(TypeConverter[LiteralToken.Kind], LiteralToken.Value);
                    expression.SetContext(Context, LocalWalker);
                    return true;
                }
                return false;
            }
        }
    }
}
