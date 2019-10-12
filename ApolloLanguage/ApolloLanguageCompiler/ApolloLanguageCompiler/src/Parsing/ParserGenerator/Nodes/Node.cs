using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    //public class Node
    //{
    //    private readonly Dictionary<Nodes, List<object>> parsed = new Dictionary<Nodes, List<object>>();
    //
    //    public IEnumerable<object> this[Nodes nodes] => this.parsed[nodes].AsEnumerable();
    //
    //    public void Add(Nodes node, object parsed)
    //    {
    //        if (!this.parsed.ContainsKey(node))
    //        {
    //            this.parsed[node] = new List<object>();
    //        }
    //        this.parsed[node].Add(parsed);
    //    }
    //
    //
    //    private static readonly string Indetation = new string(' ', 4);
    //    public IEnumerable<string> Representation()
    //    {
    //        List<string> representation = new List<string>();
    //        foreach (KeyValuePair<Nodes, List<object>> kvp in this.parsed)
    //        {
    //            if (kvp.Value.Any())
    //            {
    //                representation.Add($"{kvp.Key}[");
    //                foreach (object obj in kvp.Value)
    //                {
    //                    if (obj is Node node)
    //                    {
    //                        IEnumerable<string> nodeRepresentation = node.Representation().Select(n => $"{Indetation}{n}");
    //                        representation.AddRange(nodeRepresentation);
    //                    }
    //                    else
    //                    {
    //                        representation.Add($"{Indetation}{obj}");
    //                    }
    //                }
    //                representation.Add("]");
    //            }
    //            else
    //                representation.Add($"{kvp.Key}[]");
    //        }
    //        return representation;
    //    }
    //}
}
