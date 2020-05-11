using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenNodeKeeper : NodeParser
    {
        protected readonly SyntaxKeyword[] Keywords;

        public TokenNodeKeeper(SyntaxKeyword[] keywords)
        {
            this.Keywords = keywords;
        }

        public override void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (LocalWalker.TryGetNext(out Token token, this.Keywords))
            {
                Console.WriteLine($"Parsed {token}");
                localWalk(LocalWalker);
                node = new TokenNode(token, Context.To(LocalWalker.Context));
                walk = localWalk;
                throw Succeded;
            }
            throw Failed;
        }

        public static TokenNodeKeeper Keep(params SyntaxKeyword[] keywords) => new TokenNodeKeeper(keywords);
    }
}
