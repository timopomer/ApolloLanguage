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
    public class AnyNodeParser : TypedNodeParser, IContainsChildren
    {
        protected readonly NodeParser[] Parsers;
        public IEnumerable<NodeParser> Children => this.Parsers;

        public AnyNodeParser(NodeParser[] parsers, NodeTypes type) : base(type)
        {
            this.Parsers = parsers;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            foreach (NodeParser parser in this.Parsers)
            {
                try
                {
                    parser.ParseNode(ref node, out localWalk, LocalWalker, resultHistory);
                }
                catch (Failure)
                {
                    continue;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    walk = localWalk;
                    SourceContext context = walker.To(walk);

                    node = new ContextContainerNode(node, context, this.type);
                    resultHistory.AddResult(new SuccessfulParsingResult(walker.To(localWalk), this));
                    throw;
                }
            }
            resultHistory.AddResult(new FailedParsingResult(walker.Location, this));
            throw Failed;
        }

        public static AnyNodeParser Any(params NodeParser[] parsers) => new AnyNodeParser(parsers, NodeTypes.Unknown);
        public static AnyNodeParser Any(NodeTypes type, params NodeParser[] parsers) => new AnyNodeParser(parsers, type);
    }
}
