using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public enum NodeTypes
    {
        Access,
        Addition,
        Assignment,
        FunctionCall,
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
        Primary,
        FunctionParameter,


        Program,
        Class,
        Function,
        Modifier,
        VisibillityModifier,
        InstanceModifier,
        Type,
        Expression,
        CodeBlock,
        ReturnElement,
    }
}
