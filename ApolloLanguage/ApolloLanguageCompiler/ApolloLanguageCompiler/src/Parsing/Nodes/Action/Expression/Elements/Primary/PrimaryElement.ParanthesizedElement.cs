using ApolloLanguageCompiler.Tokenization;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract partial class PrimaryElement
    {
        public class ParanthesizedElement : PrimaryElement
        {
            public override object Clone() => new ParanthesizedElement(this);

            public ExpressionElement Element { get; private set; }

            public ParanthesizedElement(ExpressionElement element)
            {
                this.Element = element;
            }
            public ParanthesizedElement(ParanthesizedElement element)
            {
                this.Element = element.Element.Clone() as ExpressionElement;
            }

            public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
            {
                TokenWalker LocalWalker = new TokenWalker(walker);
                walk = LocalWalker.State;
                SourceContext Context = LocalWalker.Context;

                expression = null;

                if (LocalWalker.TryGetNext(SyntaxKeyword.OpenParenthesis))
                {
                    if (TryParse(HeadExpressionElement.TryParse, out expression, LocalWalker))
                    {
                        if (!LocalWalker.TryGetNext(SyntaxKeyword.CloseParenthesis))
                            return false;

                        expression = new ParanthesizedElement(expression);
                        expression.SetContext(Context, LocalWalker);
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
