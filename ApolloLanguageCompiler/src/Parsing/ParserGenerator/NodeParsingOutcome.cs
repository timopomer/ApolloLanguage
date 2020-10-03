using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Parsing
{
    public class NodeParsingOutcome : Exception
    {
        public static Success Succeded = new Success();
        public class Success : NodeParsingOutcome { }

        public static Failure Failed = new Failure();

        public class Failure : NodeParsingOutcome
        {
            public SourceContext at;
            public Failure(SourceContext at=null)
            {
                this.at = at;
            }
        }
    }

}
