using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class ForeverNodeParser : TypedNodeParser, IContainsChildren
    {
        protected readonly NodeParser Parser;
        public IEnumerable<NodeParser> Children => new [] {this.Parser};

        public ForeverNodeParser(NodeParser parser, NodeTypes type) : base(type)
        {
            this.Parser = parser;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;
            Debug.WriteLine(Parsers.Node.Program.ToStringRecursively(highlighted: this, enableHighlighting: false));

            List<Node> parsedNodes = new List<Node>();

            while (!LocalWalker.IsLast())
            {
                Node parsedNode = null;

                try
                {
                    this.Parser.ParseNode(ref parsedNode, out localWalk, LocalWalker, resultHistory);
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
                    LocalWalker.SkipWhitespace();
                    continue;
                }
            }
            walk = LocalWalker.State;
            var context = walker.To(walk);
            node = new ManyNode(this.type, parsedNodes, context);
            resultHistory.AddResult(new SuccessfulParsingResult(context, this));

            throw Succeded;
        }

        public static ForeverNodeParser Forever(NodeParser parser) => new ForeverNodeParser(parser, NodeTypes.Unknown);
        public static ForeverNodeParser Forever(NodeTypes type, NodeParser parser) => new ForeverNodeParser(parser, type);
    }
}
