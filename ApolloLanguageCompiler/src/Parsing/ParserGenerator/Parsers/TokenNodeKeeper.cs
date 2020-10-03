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

        public override void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (LocalWalker.TryGetNext(out Token token, this.Keywords))
            {
                localWalk(LocalWalker);
                node = new TokenNode(token, Context.To(LocalWalker.Context));
                walk = localWalk;

                Console.WriteLine($"Parsed {node}");
                Console.WriteLine($"parsed with {this}");
                //SourceFormatter(Context);
                //Console.WriteLine(LocalWalker.Context.ContextLocation);
                throw Succeded;
            }
            throw new Failure(at: LocalWalker.Context);
        }

        public static TokenNodeKeeper Keep(params SyntaxKeyword[] keywords) => new TokenNodeKeeper(keywords);
    }
}
