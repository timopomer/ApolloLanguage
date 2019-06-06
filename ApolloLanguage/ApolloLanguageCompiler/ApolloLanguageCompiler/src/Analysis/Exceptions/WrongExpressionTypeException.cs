using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Analysis.IR
{
    [Serializable]
    public class UnexpectedNodeException : AnalysisException
    {
        public UnexpectedNodeException() { }
        public UnexpectedNodeException(string message) : base(message) { }
        public UnexpectedNodeException(string message, Exception inner) : base(message, inner) { }
        protected UnexpectedNodeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
