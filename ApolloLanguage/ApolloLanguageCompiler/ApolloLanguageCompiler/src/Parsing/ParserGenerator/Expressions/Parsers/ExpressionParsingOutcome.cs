using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class ExpressionParsingOutcome : Exception
    {
        public class Success : ExpressionParsingOutcome { }
        public class Failure : ExpressionParsingOutcome { }
    }

}
