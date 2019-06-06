using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Analysis.IR
{
    [Serializable]
    public class WrongExpressionTypeException : AnalysisException
    {
        public WrongExpressionTypeException() { }
        public WrongExpressionTypeException(string message) : base(message) { }
        public WrongExpressionTypeException(string message, Exception inner) : base(message, inner) { }
        protected WrongExpressionTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
