using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApolloLanguageCompiler.Tokenization
{

    public class TokenFactory : IEnumerable<Token>
    {
        private SourceContext context;

        const string Keywords = "false|true|letter|number|boolean|string|return|class|instance|exposed|hidden|extension";
        readonly TokenParser[] parsers = {
            new TokenParser(@"^""(?:\\""|[^""])*""", SyntaxKeyword.LiteralStr        ),
            new TokenParser(@"^'(?:\\'|[^'])+'",     SyntaxKeyword.LiteralLetter     ),
            new TokenParser(@"^\d+",                 SyntaxKeyword.LiteralNumber     ),
            new TokenParser(@"^false",               SyntaxKeyword.LiteralFalse      ),
            new TokenParser(@"^true",                SyntaxKeyword.LiteralTrue       ),
            new TokenParser(@"^:",                   SyntaxKeyword.Colon             ),
            new TokenParser(@"^;",                   SyntaxKeyword.SemiColon         ),
            new TokenParser(@"^\ +",                 SyntaxKeyword.Whitespace        ),
            new TokenParser(@"^[\r\n|\r|\n]{1}",     SyntaxKeyword.EOL               ),
            new TokenParser(@"^,",                   SyntaxKeyword.Comma             ),
            new TokenParser(@"^\.",                  SyntaxKeyword.Dot               ),
            new TokenParser(@"^=(?!=)",              SyntaxKeyword.Assignment        ),
            new TokenParser(@"^\(",                  SyntaxKeyword.OpenParenthesis   ),
            new TokenParser(@"^\)",                  SyntaxKeyword.CloseParenthesis  ),
            new TokenParser(@"^\{",                  SyntaxKeyword.OpenCurlyBracket  ),
            new TokenParser(@"^\}",                  SyntaxKeyword.CloseCurlyBracket ),
            new TokenParser(@"^\[",                  SyntaxKeyword.OpenSquareBracket ),
            new TokenParser(@"^\]",                  SyntaxKeyword.CloseSquareBracket),
            new TokenParser(@"^\+",                  SyntaxKeyword.Plus              ),
            new TokenParser(@"^\-",                  SyntaxKeyword.Minus             ),
            new TokenParser(@"^!(?!=)",              SyntaxKeyword.Negate            ),
            new TokenParser(@"^\*(?!\*)",            SyntaxKeyword.Multiply          ),
            new TokenParser(@"^\%",                  SyntaxKeyword.Mod               ),
            new TokenParser(@"^\*\*",                SyntaxKeyword.Power             ),
            new TokenParser(@"^\/(?!\/)",            SyntaxKeyword.Divide            ),
            new TokenParser(@"^\/\/",                SyntaxKeyword.Root              ),
            new TokenParser(@"^==",                  SyntaxKeyword.Equal             ),
            new TokenParser(@"^!=",                  SyntaxKeyword.InEqual           ),
            new TokenParser(@"^<(?!=)",              SyntaxKeyword.Lesser            ),
            new TokenParser(@"^<=",                  SyntaxKeyword.LesserEqual       ),
            new TokenParser(@"^>(?!=)",              SyntaxKeyword.Greater           ),
            new TokenParser(@"^>=",                  SyntaxKeyword.GreaterEqual      ),
            new TokenParser(@"^\^",                  SyntaxKeyword.Xor               ),
            new TokenParser(@"^&",                   SyntaxKeyword.And               ),
            new TokenParser(@"^&(?!&)",              SyntaxKeyword.ShortAnd          ),
            new TokenParser(@"^\|",                  SyntaxKeyword.Or                ),
            new TokenParser(@"^\|(?!\|)",            SyntaxKeyword.ShortOr           ),
            new TokenParser(@"^letter",              SyntaxKeyword.Letter            ),
            new TokenParser(@"^number",              SyntaxKeyword.Number            ),
            new TokenParser(@"^boolean",             SyntaxKeyword.Boolean           ),
            new TokenParser(@"^str",                 SyntaxKeyword.Str               ),
            new TokenParser(@"^return",              SyntaxKeyword.Return            ),
            new TokenParser(@"^class",               SyntaxKeyword.Class             ),
            new TokenParser(@"^instance",            SyntaxKeyword.Instance          ),
            new TokenParser(@"^exposed",             SyntaxKeyword.Exposed           ),
            new TokenParser(@"^hidden",              SyntaxKeyword.Hidden            ),
            new TokenParser(@"^extension",           SyntaxKeyword.Extension         ),
            new TokenParser(@"^(?!"+Keywords+@")\w*",SyntaxKeyword.Identifier        ),
            new TokenParser(@"^use",                 SyntaxKeyword.Use               )
};


        public TokenFactory(SourceCode source)
        {
            this.context = new SourceContext(0, 0, 0, source.Code.Length, source);
        }

        public IEnumerator<Token> GetEnumerator()
        {
            while (this.context.Start < this.context.Length)
            {
                Token Parsed = null;
                TokenParser Parser = this.parsers.First(n => n.TryParse(this.context, out Parsed));

                if (Parsed.Kind == SyntaxKeyword.EOL)
                {
                    this.context.Line++;
                    this.context.Column = 0;
                }
                else
                    this.context.Column += Parsed.Context.Length;

                this.context.Start += Parsed.Context.Length;

                yield return Parsed;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
