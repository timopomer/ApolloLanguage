using ApolloLanguageCompiler.Source;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class ContinuousNodeParser : NodeParser, IContainsChildren
    {
        protected readonly NodeParser[] Parsers;
        public IEnumerable<NodeParser> Children => this.Parsers;

        public ContinuousNodeParser(NodeParser[] parsers)
        {
            this.Parsers = parsers;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            Node nodeBackup = node;
            List<Node> parsedNodes = new List<Node>();

            foreach (NodeParser parser in this.Parsers)
            {
                try
                {
                    parser.ParseNode(ref nodeBackup, out localWalk, LocalWalker, resultHistory);
                }
                catch (Failure)
                {
                    resultHistory.AddResult(new FailedParsingResult(walker.Location, this));
                    throw;
                }
                catch (Success)
                {
                    parsedNodes.Add(node);
                    localWalk(LocalWalker);
                    continue;
                }
            }

            node = nodeBackup;
            walk = localWalk;
            var context = walker.To(walk);
            node = new ContextNode(node, context);
            resultHistory.AddResult(new SuccessfulParsingResult(context, this));
            throw Succeded;
        }

        public static ContinuousNodeParser Continuous(params NodeParser[] parsers) => new ContinuousNodeParser(parsers);
    }
}
