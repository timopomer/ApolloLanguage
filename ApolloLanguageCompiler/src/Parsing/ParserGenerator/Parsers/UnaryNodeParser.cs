using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class UnaryNodeParser : NodeParser, IContainsChildren
    {
        private readonly NodeTypes type;
        protected readonly NodeParser[] Parsers;
        public IEnumerable<NodeParser> Children => this.Parsers;

        public UnaryNodeParser(NodeTypes type, NodeParser[] parsers)
        {
            this.type = type;
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
                    continue;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    if (parsedNode != null)
                    {
                        parsedNodes.Add(parsedNode);
                    }
                }
            }

            if (parsedNodes.Count == 1)
            {
                node = new UnaryNode(this.type, parsedNodes[0], node, walker.To(LocalWalker));
                walk = localWalk;
                resultHistory.AddResult(new ParsingResult(walker.To(localWalk), this, true));
                throw Succeded;
            }

            resultHistory.AddResult(new ParsingResult(walker.To(localWalk), this, false));
            throw Failed;
        }

        public static UnaryNodeParser MakeUnary(NodeTypes type, params NodeParser[] parsers) => new UnaryNodeParser(type, parsers);
    }
}
