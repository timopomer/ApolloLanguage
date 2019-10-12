using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApolloLanguageCompiler.Parsing.TokenWalker;
using static ApolloLanguageCompiler.Parsing.ExpressionParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class ExpressionParser
    {
        public void Parse(ref Expression expression, TokenWalker walker)
        {
            StateWalker walk = null;
            try
            {
                this.Parse(ref expression, out walk, walker);
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

        public abstract void Parse(ref Expression expression, out TokenWalker.StateWalker walk, TokenWalker walker);
        public override string ToString() => $"{this.GetType().BaseType.Name}[{base.ToString()}]";

    }
}
