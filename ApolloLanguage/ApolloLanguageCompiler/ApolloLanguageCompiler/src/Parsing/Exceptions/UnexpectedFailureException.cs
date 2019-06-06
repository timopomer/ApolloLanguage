using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.Exceptions
{
    [Serializable]
    public class UnexpectedFailureException : ParsingException
    {
        public UnexpectedFailureException() { }
        public UnexpectedFailureException(string message) : base(message) { }
        public UnexpectedFailureException(string message, Exception inner) : base(message, inner) { }
        protected UnexpectedFailureException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
