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
    public class ContinuousNodeParser : TypedNodeParser, IContainsChildren
    {
        protected readonly NodeParser[] Parsers;
        public IEnumerable<NodeParser> Children => this.Parsers;

        public ContinuousNodeParser(NodeParser[] parsers, NodeTypes type) : base(type)
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
                    parsedNodes.Add(nodeBackup);
                    localWalk(LocalWalker);
                    continue;
                }
            }

            node = nodeBackup;
            walk = localWalk;
            SourceContext context = walker.To(walk);
            if (node == null)
                node = new ContextNode(context, this.type);
            else
                node = new ContextContainerNode(node, context, this.type);

            resultHistory.AddResult(new SuccessfulParsingResult(context, this));
            throw Succeded;
        }

        public static ContinuousNodeParser Continuous(NodeTypes type, params NodeParser[] parsers) => new ContinuousNodeParser(parsers, type);
        public static ContinuousNodeParser Continuous(params NodeParser[] parsers) => new ContinuousNodeParser(parsers, NodeTypes.Unknown);
    }
}
