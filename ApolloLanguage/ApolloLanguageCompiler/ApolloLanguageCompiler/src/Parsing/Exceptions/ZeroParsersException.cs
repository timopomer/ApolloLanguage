using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing.Exceptions
{
    [Serializable]
    public class ZeroParsersException : ParsingException
    {
        public ZeroParsersException() { }
        public ZeroParsersException(string message) : base(message) { }
        public ZeroParsersException(string message, Exception inner) : base(message, inner) { }
        protected ZeroParsersException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
