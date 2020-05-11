using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class FailedParsingNodeException : ParserException
    {
        public FailedParsingNodeException()
        {
        }

        public FailedParsingNodeException(string message) : base(message)
        {
        }

        public FailedParsingNodeException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
