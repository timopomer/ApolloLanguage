using ApolloLanguageCompiler.Parsing.ParserGenerator.Exceptions;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator.Nodes.Parsers
{
    public class TokenEater : TokenParser
    {
        public TokenEater(SyntaxKeyword keyword) : base(keyword)
        {
            
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            if (!walker.TryGetNext(this.Keyword))
                throw new CouldNotFindTokenException();
        }

        public static TokenEater Eat(SyntaxKeyword keyword) => new TokenEater(keyword);
        public override string ToString() => $"TokenEater[{base.ToString()}]";
    }
}
