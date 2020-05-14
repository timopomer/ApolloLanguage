using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Crayon;
using static ApolloLanguageCompiler.Parsing.TokenWalker;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class NodeParser
    {
        private string name;

        public NodeParser Name(string name)
        {
            this.name = name;
            return this;
        }

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
            catch (Failure failure)
            {
                throw new FailedParsingNodeException($"Failed parsing node at: \n{failure.at.ContextLocation}");
            }
        }

        public List<NodeParser> PathTo(NodeParser lookingFor)
        {
            if (this == lookingFor)
                return new List<NodeParser>() {this};

            if (this is IContainsChildren containsChildren)
            {
                foreach (NodeParser child in containsChildren.Children)
                {
                    List<NodeParser> path = child.PathTo(lookingFor);
                    if (path.Count() > 0)
                    {
                        path.Insert(0, this);
                        return path;
                    }
                }
            }
            return new List<NodeParser>();
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

        private string formattedName => this.name == null
            ? string.Empty
            : $"{Output.Red("<")}{Output.Yellow(this.name)}{Output.Red(">")}";


        private void ToStringRecursively(StringBuilder builder, string indent, bool isLast, List<NodeParser> referenced, NodeParser highlighted)
        {

            if (this is ReferenceNodeParser referenceNodeParser)
            {
                NodeParser referencedParser = referenceNodeParser.Referenced;
                referencedParser.ToStringRecursively(builder, indent, isLast, referenced, highlighted);
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

            if (referenced.Contains(this))
            {
                string referencedLine = $"{Output.BrightMagenta("referenced")}({this.formattedName})";
                if (highlighted == this)
                    builder.AppendLine(referencedLine.Reversed());
                else
                    builder.AppendLine(referencedLine);

                return;
            }
            referenced.Add(this);

            string prefix = this.formattedName;
            if (this.name != null)
                prefix += Output.BrightGreen(":");

            string fullLine = $"{prefix}{this.ToString()}";

            if (highlighted == this)
                builder.AppendLine($"{fullLine.Reversed()}{Output.Red(" <------ current parser")}");
            else
                builder.AppendLine(fullLine);

            if (this is IContainsChildren recursiveNode)
            {
                IEnumerable<NodeParser> children = recursiveNode.Children.Except(referenced);
                int childrenCount = children.Count();

                IEnumerable<(NodeParser Child, bool)> joinedWithLast = children.Select((Child, Index) => ( Child, Index == childrenCount-1 ));
                foreach ((NodeParser child, bool childIsLast) in joinedWithLast)
                {
                    child.ToStringRecursively(builder, indent, childIsLast, referenced, highlighted);
                }
            }
        }

        public string ToStringRecursively(NodeParser highlighted=null)
        {
            List<NodeParser> referenced = new List<NodeParser>();
            StringBuilder builder = new StringBuilder();
            this.ToStringRecursively(builder, indent: "", isLast: true, referenced, highlighted);
            return builder.ToString();
        }

        public override string ToString() => $"{this.GetType().Name}";
    }
}
