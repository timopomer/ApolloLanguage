using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public class CouldNotFindTokenException : ParserException
    {
        public CouldNotFindTokenException()
        {
        }

        public CouldNotFindTokenException(string message) : base(message)
        {
        }

        public CouldNotFindTokenException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
