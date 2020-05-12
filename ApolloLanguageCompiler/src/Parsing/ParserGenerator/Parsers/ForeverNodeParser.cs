using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
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

        public override void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            while (!LocalWalker.IsLast())
            {
                try
                {
                    this.Parser.Parse(ref node, out localWalk, LocalWalker);
                }
                catch (Failure)
                {
                    throw;
                }
                catch (Success)
                {
                    localWalk(LocalWalker);
                    continue;
                }
            }
            walk = localWalk;
            throw Succeded;
        }

        public static ForeverNodeParser Forever(NodeParser parser) => new ForeverNodeParser(parser);
    }
}
