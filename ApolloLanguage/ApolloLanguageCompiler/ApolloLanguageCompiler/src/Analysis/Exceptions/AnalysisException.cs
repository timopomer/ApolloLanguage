using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Analysis.IR
{
    [Serializable]
    public class AnalysisException : Exception
    {
        public AnalysisException() { }
        public AnalysisException(string message) : base(message) { }
        public AnalysisException(string message, Exception inner) : base(message, inner) { }
        protected AnalysisException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
