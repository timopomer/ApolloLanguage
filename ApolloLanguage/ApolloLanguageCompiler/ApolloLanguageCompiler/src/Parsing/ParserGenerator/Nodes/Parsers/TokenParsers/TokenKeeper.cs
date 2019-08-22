
using ApolloLanguageCompiler.Tokenization;
using System;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenKeeper : TokenParser
    {
        public TokenKeeper(SyntaxKeyword keyword) : base(keyword)
        {
            
        }

        public override void Parse(NodeParser parser, Node node, TokenWalker walker)
        {
            if (!walker.TryGetNext(out Token token, this.Keyword))
                throw new CouldNotFindTokenException();
            node.Add(parser.Type, token);
        }
        
        public static TokenKeeper Keep(SyntaxKeyword keyword) => new TokenKeeper(keyword);
        public override string ToString() => $"TokenKeeper[{base.ToString()}]";
    }
}
