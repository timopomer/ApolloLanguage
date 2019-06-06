using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Analysis.IR
{
    [Serializable]
    public class IdentifierDeclarationNotFoundException : AnalysisException
    {
        public IdentifierDeclarationNotFoundException() { }
        public IdentifierDeclarationNotFoundException(string message) : base(message) { }
        public IdentifierDeclarationNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected IdentifierDeclarationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
