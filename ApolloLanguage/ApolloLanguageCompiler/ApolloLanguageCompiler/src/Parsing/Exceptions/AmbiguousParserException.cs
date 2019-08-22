using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    [Serializable]
    public class AmbiguousParserException : ParsingException
    {
        public AmbiguousParserException() { }
        public AmbiguousParserException(string message) : base(message) { }
        public AmbiguousParserException(string message, Exception inner) : base(message, inner) { }
        protected AmbiguousParserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
