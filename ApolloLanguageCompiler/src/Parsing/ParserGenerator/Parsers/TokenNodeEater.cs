using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Tokenization;
using Crayon;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenNodeEater : NodeParser
    {
        protected readonly SyntaxKeyword[] Keywords;

        public TokenNodeEater(SyntaxKeyword[] keywords)
        {
            this.Keywords = keywords;
        }

        public override string ToString(bool enableHighlighting=false)
        {
            return enableHighlighting ? $"{this.GetType().Name}{Output.Yellow(":")}[{Output.Cyan(string.Join(", ", this.Keywords))}]" : this.ToString();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}:[{(string.Join(", ", this.Keywords))}]";
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            if (LocalWalker.TryGetNext(out Token token, this.Keywords))
            {
                var walked = walker.To(LocalWalker);
                localWalk(LocalWalker);
                //node = new DiscardedTokenNode(token, walked);
                walk = localWalk;
                resultHistory.AddResult(new SuccessfulParsingResult(walker.To(localWalk), this));
                throw Succeded;
            }

            resultHistory.AddResult(new FailedParsingResult(walker.Location, this));
            throw Failed;
        }
        public static TokenNodeEater Eat(params SyntaxKeyword[] keywords) => new TokenNodeEater(keywords);
    }
}
