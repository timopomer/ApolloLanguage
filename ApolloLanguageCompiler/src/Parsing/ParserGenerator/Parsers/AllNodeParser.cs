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
    public class AllNodeParser : NodeParser, IContainsChildren
    {
        protected readonly NodeParser[] Parsers;
        public IEnumerable<NodeParser> Children => this.Parsers;

        public AllNodeParser(NodeParser[] parsers)
        {
            this.Parsers = parsers;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            List<Node> parsedNodes = new List<Node>();

            foreach (NodeParser parser in this.Parsers)
            {
                Node parsedNode = null;

                try
                {
                    parser.ParseNode(ref parsedNode, out localWalk, LocalWalker, resultHistory);
                }
                catch (Failure)
                {
                    resultHistory.AddResult(new FailedParsingResult(walker.Location, this));
                    throw;
                }
                catch (Success)
                {
                    if (parsedNode != null)
                        parsedNodes.Add(parsedNode);
                    localWalk(LocalWalker);
                    continue;
                }
            }

            walk = localWalk;
            var context = walker.To(walk);
            node = new ManyNode(parsedNodes, context);
            resultHistory.AddResult(new SuccessfulParsingResult(context, this));
            throw Succeded;
        }

        public static AllNodeParser All(params NodeParser[] parsers) => new AllNodeParser(parsers);
    }
}
