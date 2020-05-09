using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class AnyNodeParser : NodeParser
    {
        protected readonly NodeParser[] Parsers;

        public AnyNodeParser(NodeParser[] parsers)
        {
            this.Parsers = parsers;
        }

        public override void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            foreach (NodeParser parser in this.Parsers)
            {
                try
                {
                    parser.Parse(ref node, out localWalk, LocalWalker);
                }
                catch (Failure)
                {
                    continue;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    walk = localWalk;
                    throw;
                }
            }
            throw Failed;
        }

        public static AnyNodeParser Any(params NodeParser[] parsers) => new AnyNodeParser(parsers);
        public override string ToString(ref IEnumerable<NodeParser> referenced) => this.In(ref referenced) ?? JoinParsers(ref referenced, this.Parsers);
    }
}
