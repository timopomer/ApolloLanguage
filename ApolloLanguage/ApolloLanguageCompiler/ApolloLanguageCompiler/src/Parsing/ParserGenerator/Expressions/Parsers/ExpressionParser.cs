using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.TokenWalker;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

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
        
        public void Parse(out Expression expression, TokenWalker walker)
        {
            expression = null;
            StateWalker walk = null;
            try
            {
                this.Parse(out expression, out walk, walker);
            }
            catch (Success)
            {
                walk(walker);
                if (expression is null)
                    throw new FailedParsingExpressionException();

            }
            catch (Failure)
            {
                throw new FailedParsingExpressionException();
            }
        }
        
        public void Parse(out Expression expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;

            foreach (IExpressionParser expressionParser in this.parsers)
            {
                expressionParser.Parse(out expression, out walk, LocalWalker);
            }
            throw Failed;
        }
        
        public override string ToString() => $"{this.Type}:({string.Join<IExpressionParser>(" ,", this.parsers)})";

    }
}
