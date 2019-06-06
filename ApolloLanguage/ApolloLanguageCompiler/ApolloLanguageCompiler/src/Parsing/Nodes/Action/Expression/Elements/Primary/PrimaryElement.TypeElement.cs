using ApolloLanguageCompiler.Tokenization;
using System.Collections.Generic;
using System.Linq;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing.Nodes
{
    public abstract partial class PrimaryElement
    {
        public class TypeElement : PrimaryElement
        {
            public override object Clone() => new TypeElement(this);

            public Types Type { get; private set; }

            public enum Types
            {
                Number,
                Str,
                Letter,
                Boolean
            }

            public static readonly Dictionary<SyntaxKeyword, Types> TypeConverter = new Dictionary<SyntaxKeyword, Types>
            {
                {SyntaxKeyword.Number,              Types.Number  },
                {SyntaxKeyword.Str,                 Types.Str     },
                {SyntaxKeyword.Letter,              Types.Letter  },
                {SyntaxKeyword.Boolean,             Types.Boolean }
            };

            public TypeElement(Types type)
            {
                this.Type = type;
            }

            public TypeElement(TypeElement element)
            {
                this.Type = element.Type;
            }
            public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
            {
                TokenWalker LocalWalker = new TokenWalker(walker);
                walk = LocalWalker.State;
                SourceContext Context = LocalWalker.Context;

                expression = null;

                if (LocalWalker.TryGetNext(out Token TypeToken, SyntaxKeyword.Number, SyntaxKeyword.Str, SyntaxKeyword.Letter, SyntaxKeyword.Boolean))
                {
                    expression = new TypeElement(TypeConverter[TypeToken.Kind]);
                    expression.SetContext(Context, LocalWalker);
                    return true;
                }
                return false;
            }
        }
    }
}
