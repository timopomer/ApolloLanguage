using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Analysis.IR
{
    [Serializable]
    public class AmbiguousIdentifierException : AnalysisException
    {
        public AmbiguousIdentifierException() { }
        public AmbiguousIdentifierException(string message) : base(message) { }
        public AmbiguousIdentifierException(string message, Exception inner) : base(message, inner) { }
        protected AmbiguousIdentifierException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
