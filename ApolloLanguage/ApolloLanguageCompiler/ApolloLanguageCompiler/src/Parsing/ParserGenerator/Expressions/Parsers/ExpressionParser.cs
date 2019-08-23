using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionParser
    {
        public Expressions Type { get; }
        private readonly IExpressionParser[] parsers;
        
        public ExpressionParser(Expressions type, params IExpressionParser[] parsers)
        {
            this.Type = type;
            this.parsers = parsers;
        }

        public void Parse(out Expression expression, TokenWalker walker)
        {
            node = new Node();
            this.Parse(this, node, walker);
        }
        
        public void Parse(out Expression expression, out StateWalker walk, TokenWalker walker)
        {
            Node innerNode = new Node();
            foreach (INodeParser singleParser in this.parsers)
            {
                singleParser.Parse(this, innerNode, walker);
            }
            node.Add(this.Type, innerNode);
        }

        public override string ToString() => $"{this.Type}:({string.Join<IExpressionParser>(" ,", this.parsers)})";
    }
}
