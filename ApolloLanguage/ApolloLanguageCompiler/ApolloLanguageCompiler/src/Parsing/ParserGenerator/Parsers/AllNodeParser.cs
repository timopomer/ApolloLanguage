using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class AllNodeParser : NodeParser
    {
        protected readonly NodeParser[] Parsers;

        public AllNodeParser(NodeParser[] parsers)
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
                    throw;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    continue;
                }
            }
            walk = localWalk;
            throw Succeded;
        }

        public static AllNodeParser All(params NodeParser[] parsers) => new AllNodeParser(parsers);
        public override string ToString(ref IEnumerable<NodeParser> referenced) => this.In(ref referenced) ?? $"{Environment.NewLine}{this.GetType().Name}[{JoinParsers(ref referenced, this.Parsers)}";
    }
}
