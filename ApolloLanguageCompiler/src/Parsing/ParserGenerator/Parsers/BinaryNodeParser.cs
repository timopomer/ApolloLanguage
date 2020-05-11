using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    class BinaryNodeParser : NodeParser
    {
        private readonly NodeTypes type;
        protected readonly NodeParser[] Parsers;

        public BinaryNodeParser(NodeTypes type, NodeParser[] parsers)
        {
            this.type = type;
            this.Parsers = parsers;
        }

        public override void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            List<Node> parsedNodes = new List<Node>();
            foreach (NodeParser parser in this.Parsers)
            {
                Node parsedNode = null;
                try
                {
                    parser.Parse(ref parsedNode, out localWalk, LocalWalker);
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
                node = new BinaryNode(this.type, node, parsedNodes[0], parsedNodes[1], node.Context.To(LocalWalker.Context));
                Console.WriteLine($"Parsed {node}");
                throw Succeded;
            }
            throw Failed;
        }

        public static BinaryNodeParser MakeBinary(NodeTypes type, params NodeParser[] parsers) => new BinaryNodeParser(type, parsers);
    }
}
