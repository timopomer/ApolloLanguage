using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public enum Expressions
    {
        Access,
        Addition,
        Assignment,
        CallFunction,
        Comparison,
        Equality,
        Exponentiation,
        Multiplication,
        Namespace,
        Negation,
        Identifier,
        Literal,
        Paranthesized,
        PrimitiveType,
        Primary
    }
}
