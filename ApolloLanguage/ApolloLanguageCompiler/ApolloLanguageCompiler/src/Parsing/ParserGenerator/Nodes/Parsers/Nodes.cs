using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public enum Nodes
    {
        Program,
        Class,
        Function,
        Modifier,
        VisibillityModifier,
        InstanceModifier,
        Type,
        Expression,
        Element,
        PrimaryElement,
        IdentifierElement,
        CodeBlock,
        FunctionParameter,
        PrimitiveTypeElement,
        ReturnElement,
        Assignment,
        PrimitiveElement
    }
}
