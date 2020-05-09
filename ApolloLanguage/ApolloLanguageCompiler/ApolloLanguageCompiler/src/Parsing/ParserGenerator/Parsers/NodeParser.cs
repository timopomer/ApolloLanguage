using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.TokenWalker;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class NodeParser
    {
        public void Parse(ref Node node, TokenWalker walker)
        {
            StateWalker walk = null;
            try
            {
                this.Parse(ref node, out walk, walker);
            }
            catch (Success)
            {
                walk(walker);
                if (node is null)
                    throw new FailedParsingNodeException("Success, but result node is empty");

            }
            catch (Failure)
            {
                throw new FailedParsingNodeException("Failed parsing node");
            }
        }

        public string In(ref IEnumerable<NodeParser> referenced)
        {
            Console.WriteLine(referenced.Count());
            if (referenced.Contains(this))
            {
                return "printed";
            }
            else
            {
                referenced = referenced.Concat(new[] { this }).Distinct();
                return null;
            }
        }
        public abstract void Parse(ref Node node, out TokenWalker.StateWalker walk, TokenWalker walker);
        public override string ToString()
        {
            IEnumerable<NodeParser> referenced = new NodeParser[] { };
            return $"{this.GetType().Name}[{this.ToString(ref referenced)}]";
        }
        public string JoinParsers(ref IEnumerable<NodeParser> referenced, IEnumerable<NodeParser> toJoin)
        {
            List<string> stringifiedParsers = new List<string>();
            foreach (NodeParser parser in toJoin)
            {
                stringifiedParsers.Add(parser.ToString(ref referenced));
            }
            return string.Join(",", stringifiedParsers);
        }
        public abstract string ToString(ref IEnumerable<NodeParser> referenced);
    }
}
