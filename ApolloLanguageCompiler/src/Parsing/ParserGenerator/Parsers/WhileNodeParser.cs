using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class WhileNodeParser : NodeParser, IContainsChildren
    {
        protected readonly NodeParser Parser;
        public IEnumerable<NodeParser> Children => new [] {this.Parser};

        public WhileNodeParser(NodeParser parser)
        {
            this.Parser = parser;
        }

        public override void ParseNode(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker, ParseResultHistory resultHistory)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            while (true)
            {
                try
                {
                    this.Parser.ParseNode(ref node, out localWalk, LocalWalker, resultHistory);
                }
                catch (Failure)
                {
                    resultHistory.AddResult(new ParsingResult(walker.To(localWalk), this, true));
                    throw Succeded;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    walk = localWalk;
                    continue;
                }
                throw new ParserException("Parser didnt specifiy if it succeded or not");
            }
        }

        public static WhileNodeParser While(NodeParser parser) => new WhileNodeParser(parser);
    }
}
