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
    public class ForeverNodeParser : NodeParser, IContainsChildren
    {
        protected readonly NodeParser Parser;
        public IEnumerable<NodeParser> Children => new [] {this.Parser};

        public ForeverNodeParser(NodeParser parser)
        {
            this.Parser = parser;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;
            Debug.WriteLine(Parsers.Node.Program.ToStringRecursively(highlighted: this, enableHighlighting: false));

            while (!LocalWalker.IsLast())
            {
                try
                {
                    this.Parser.ParseNode(ref node, out localWalk, LocalWalker, resultHistory);
                }
                catch (Failure)
                {
                    resultHistory.AddResult(new FailedParsingResult(walker.Location, this));
                    throw;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    LocalWalker.SkipWhitespace();
                    continue;
                }
            }
            walk = LocalWalker.State;
            resultHistory.AddResult(new SuccessfulParsingResult(walker.To(localWalk), this));
            throw Succeded;
        }

        public static ForeverNodeParser Forever(NodeParser parser) => new ForeverNodeParser(parser);
    }
}
