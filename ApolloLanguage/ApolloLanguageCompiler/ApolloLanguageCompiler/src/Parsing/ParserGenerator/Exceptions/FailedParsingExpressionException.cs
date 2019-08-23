using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class FailedParsingExpressionException : ParserException
    {
        public FailedParsingExpressionException()
        {
        }

        public FailedParsingExpressionException(string message) : base(message)
        {
        }

        public FailedParsingExpressionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
