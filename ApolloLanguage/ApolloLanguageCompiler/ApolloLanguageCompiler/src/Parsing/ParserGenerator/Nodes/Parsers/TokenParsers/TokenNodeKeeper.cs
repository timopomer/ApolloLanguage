
using ApolloLanguageCompiler.Tokenization;
using System;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenNodeKeeper : TokenNodeParser
    {
        public TokenNodeKeeper(SyntaxKeyword keyword) : base(keyword)
        {

        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            if (!walker.TryGetNext(out Token token, this.Keyword))
                throw new CouldNotFindTokenException();
            node.Add(parser.Type, token);
        }

        public static TokenNodeKeeper Keep(SyntaxKeyword keyword) => new TokenNodeKeeper(keyword);
        public override string ToString() => $"TokenKeeper[{base.ToString()}]";
    }
}
