using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.ParserGenerator
{
    public class Node
    {
        private readonly Dictionary<Nodes, List<object>> parsed = new Dictionary<Nodes, List<object>>();

        public IEnumerable<object> this[Nodes nodes] => this.parsed[nodes].AsEnumerable();

        public void Add(Nodes node, object parsed)
        {
            if (!this.parsed.ContainsKey(node))
            {
                this.parsed[node] = new List<object>();
            }
            this.parsed[node].Add(parsed);
        }

        public string PrettyPrint(int indentation)
        {
            string identationSpaces = new string(' ', indentation * 4);
            string extraIdentationSpaces = new string(' ', indentation * 5);

            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<Nodes, List<object>> kvp in this.parsed)
            {
                builder.Append($"{identationSpaces}{kvp.Key}");

                bool parsedAnything = kvp.Value.Any();
                if (parsedAnything)
                    builder.Append($":[{Environment.NewLine}");
                
                bool firstIteration = true;
                foreach (object obj in kvp.Value)
                {
                    if (!firstIteration)
                        builder.Append($", {Environment.NewLine}");

                    if (obj is Node node)
                    {
                        builder.Append($"{node.PrettyPrint(indentation + 1)}");
                    }
                    else
                    {
                        builder.Append($"{extraIdentationSpaces}{obj.ToString()}");
                    }
                    firstIteration = false;
                }
                if (parsedAnything)
                    builder.Append($"{Environment.NewLine}{identationSpaces}]{Environment.NewLine}");
            }
            return builder.ToString();
        }

    }
}
