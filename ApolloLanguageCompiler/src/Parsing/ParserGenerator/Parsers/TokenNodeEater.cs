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

        public override string ToString()
        {
            return $"{this.GetType().Name}{Output.Yellow(":")}[{Output.Cyan(string.Join(", ", this.Keywords))}]";
        }

        public override void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            //Console.WriteLine(Parsers.Node.Program.ToStringRecursively(highlighted: this));
            //Console.WriteLine(walker.Context.ContextLocation);

            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            if (LocalWalker.TryGetNext(out Token token, this.Keywords))
            {
                Console.WriteLine($"Parsed {token}");
                Console.WriteLine($"parsed with {this}");
                Console.WriteLine(LocalWalker.Context.ContextLocation);
                throw Succeded;
            }
            throw new Failure(at: LocalWalker.Context);
        }
        public static TokenNodeEater Eat(params SyntaxKeyword[] keywords) => new TokenNodeEater(keywords);
    }
}
