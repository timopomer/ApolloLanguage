using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private const string Cross = " ├─";
        private const string Corner = " └─";
        private const string Vertical = " │ ";
        private const string Space = "   ";

        public void ToStringRecursively(StringBuilder builder, string indent, bool isLast, List<NodeParser> referenced)
        {
            if (this is ReferenceNodeParser referenceNodeParser)
            {
                NodeParser referencedParser = referenceNodeParser.Referenced;
                referencedParser.ToStringRecursively(builder, indent, isLast, referenced);
                return;
            }

            builder.Append(indent);
            if (isLast)
            {
                builder.Append(Corner);
                indent += Space;
            }
            else
            {
                builder.Append(Cross);
                indent += Vertical;
            }

            //TODO change to internal name + info
            builder.AppendLine(this.ToString());
            if (this is IContainsChildren recursiveNode)
            {
                IEnumerable<NodeParser> children = recursiveNode.Children.Except(referenced);
                int childrenCount = children.Count();

                IEnumerable<(NodeParser Child, bool)> joinedWithLast = children.Select((Child, Index) => ( Child, Index == childrenCount-1 ));
                foreach ((NodeParser child, bool childIsLast) in joinedWithLast)
                {
                    referenced.Add(child);
                    child.ToStringRecursively(builder, indent, childIsLast, referenced);
                }
            }
        }

        public string ToStringRecursively()
        {
            List<NodeParser> referenced = new List<NodeParser>();
            StringBuilder builder = new StringBuilder();
            this.ToStringRecursively(builder, indent: "", isLast: true, referenced);
            return builder.ToString();
        }

        public override string ToString() => $"{this.GetType().Name}";
    }
}
