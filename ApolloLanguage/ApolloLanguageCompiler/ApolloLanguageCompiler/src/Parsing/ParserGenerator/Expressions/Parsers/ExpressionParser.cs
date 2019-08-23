using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionParser : IExpressionParser
    {
        public Expressions Type { get; }
        private readonly IExpressionParser[] parsers;
        
        public ExpressionParser(Expressions type, params IExpressionParser[] parsers)
        {
            this.Type = type;
            this.parsers = parsers;
        }
        
        public bool Parse(out IExpression expression, TokenWalker walker)
        {
            bool parsed = this.Parse(out expression, out StateWalker walk, walker);
            if (parsed)
                walk(walker);
            return parsed;
        }
        
        public bool Parse(out IExpression expression, out StateWalker walk, TokenWalker walker)
        {
            walk = walker.State;
            SourceContext Context = walker.Context;
            expression = null;

            foreach (IExpressionParser expressionParser in this.parsers)
            {
                if (!expressionParser.Parse(out expression, out walk, walker))
                    return false;
            }
            if (expression == null)
                throw new Exception("This shouldnt happen, expression unassigned");
            return true;
        }
        
        public override string ToString() => $"{this.Type}:({string.Join<IExpressionParser>(" ,", this.parsers)})";

    }
}
