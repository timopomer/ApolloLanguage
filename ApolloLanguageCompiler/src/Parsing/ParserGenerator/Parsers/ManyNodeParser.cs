using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Parsing
{
    public class ManyNodeParser : NodeParser, IContainsChildren
    {
        protected readonly NodeParser Parser;
        public IEnumerable<NodeParser> Children => new [] {this.Parser};

        private readonly bool allowEmpty;

        public ManyNodeParser(NodeParser parser, bool allowEmpty=true)
        {
            this.Parser = parser;
            this.allowEmpty = allowEmpty;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            SourceContext Context = LocalWalker.Context;

            List<Node> parsedNodes = new List<Node>();
            while (true)
            {
                Node parsedNode = null;
                try
                {
                    this.Parser.ParseNode(ref parsedNode, out localWalk, LocalWalker, resultHistory);
                }
                catch (Failure)
                {
                    break;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);

                    if (parsedNode != null)
                    {
                        parsedNodes.Add(parsedNode);
                    }
                    else
                    {
                        throw Failed;
                    }
                }
            }

            if (!this.allowEmpty && parsedNodes.Count == 0)
            {
                throw Failed;
            }

            node = new ManyNode(parsedNodes, Context.To(LocalWalker.Context));
            Console.WriteLine($"Parsed {node}");
            walk = localWalk;
            throw Succeded;
        }

        public static ManyNodeParser MakeMany(NodeParser parser) => new ManyNodeParser(parser);
    }
}
