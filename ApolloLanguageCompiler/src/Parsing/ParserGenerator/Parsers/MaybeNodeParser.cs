using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class MaybeNodeParser : NodeParser, IContainsChildren
    {
        protected readonly NodeParser Parser;
        public IEnumerable<NodeParser> Children => new [] {this.Parser};

        public MaybeNodeParser(NodeParser parser)
        {
            this.Parser = parser;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            try
            {
                this.Parser.ParseNode(ref node, out localWalk, LocalWalker, resultHistory);
            }
            catch (Failure)
            {
            }
            catch (Success)
            {
                localWalk(LocalWalker);
                walk = localWalk;
                resultHistory.AddResult(new SuccessfulParsingResult(walker.To(localWalk), this));
            }

            throw Succeded;
        }

        public static MaybeNodeParser Maybe(NodeParser parser) => new MaybeNodeParser(parser);
    }
}
