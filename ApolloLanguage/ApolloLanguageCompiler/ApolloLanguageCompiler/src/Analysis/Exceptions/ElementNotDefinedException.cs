using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Analysis.IR
{
    [Serializable]
    public class ElementNotDefinedException : AnalysisException
    {
        public ElementNotDefinedException() { }
        public ElementNotDefinedException(string message) : base(message) { }
        public ElementNotDefinedException(string message, Exception inner) : base(message, inner) { }
        protected ElementNotDefinedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
