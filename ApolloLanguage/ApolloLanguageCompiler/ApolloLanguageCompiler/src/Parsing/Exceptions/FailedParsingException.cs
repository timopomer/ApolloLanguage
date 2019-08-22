using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    [Serializable]
    public class FailedParsingException : ParsingException
    {
        public FailedParsingException() { }
        public FailedParsingException(string message) : base(message) { }
        public FailedParsingException(string message, Exception inner) : base(message, inner) { }
        protected FailedParsingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
