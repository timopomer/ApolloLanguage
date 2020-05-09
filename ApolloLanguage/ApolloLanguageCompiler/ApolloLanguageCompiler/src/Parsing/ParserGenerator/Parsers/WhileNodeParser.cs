using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public class WhileNodeParser : NodeParser
    {
        protected readonly NodeParser Parser;

        public WhileNodeParser(NodeParser parser)
        {
            this.Parser = parser;
        }

        public override void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            TokenWalker.StateWalker localWalk = LocalWalker.State;

            while (true)
            {
                try
                {
                    this.Parser.Parse(ref node, out localWalk, LocalWalker);
                }
                catch (Failure)
                {
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
        public override string ToString(ref IEnumerable<NodeParser> referenced) => this.In(ref referenced) ?? this.Parser.ToString(ref referenced);
    }
}