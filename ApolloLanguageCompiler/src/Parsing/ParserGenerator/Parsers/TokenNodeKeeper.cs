using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crayon;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Parsing
{
    public class TokenNodeKeeper : NodeParser
    {
        protected readonly SyntaxKeyword[] Keywords;

        public override string ToString(bool enableHighlighting = false)
        {
            return enableHighlighting ? $"{this.GetType().Name}{Output.Yellow(":")}[{Output.Cyan(string.Join(", ", this.Keywords))}]" : this.ToString();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}:[{(string.Join(", ", this.Keywords))}]";
        }


        public TokenNodeKeeper(SyntaxKeyword[] keywords)
        {
            this.Keywords = keywords;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            if (LocalWalker.TryGetNext(out Token token, this.Keywords))
            {
                var Walked = walker.To(LocalWalker);
                localWalk(LocalWalker);
                node = new TokenNode(token, Walked);
                walk = localWalk;
                resultHistory.AddResult(new ParsingResult(walker.To(localWalk), this, true));
                throw Succeded;
            }
            resultHistory.AddResult(new ParsingResult(walker.To(localWalk), this, false));
            throw Failed;
        }

        public static TokenNodeKeeper Keep(params SyntaxKeyword[] keywords) => new TokenNodeKeeper(keywords);
    }
}
