using ApolloLanguageCompiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class BinaryNodeParser : NodeParser, IContainsChildren
    {
        private readonly NodeTypes type;
        protected readonly NodeParser[] Parsers;
        public IEnumerable<NodeParser> Children => this.Parsers;

        public BinaryNodeParser(NodeTypes type, NodeParser[] parsers)
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
                    walk = localWalk;
                    if (parsedNode != null)
                    {
                        parsedNodes.Add(parsedNode);
                    }
                }
            }

            if (parsedNodes.Count == 2)
            {
                Node left = node;
                Node binaryModifier = parsedNodes[0];
                Node right = parsedNodes[1];
                SourceContext binaryNodeContext = left.Context.To(right);

                node = new BinaryNode(this.type, left, binaryModifier, right, binaryNodeContext);
                resultHistory.AddResult(new SuccessfulParsingResult(binaryNodeContext, this));
                throw Succeded;
            }
            resultHistory.AddResult(new FailedParsingResult(walker.Location, this));
            throw Failed;
        }

        public static BinaryNodeParser MakeBinary(NodeTypes type, params NodeParser[] parsers) => new BinaryNodeParser(type, parsers);
    }
}
