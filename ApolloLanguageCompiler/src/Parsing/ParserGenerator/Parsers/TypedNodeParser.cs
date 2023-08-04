using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Crayon;
using static ApolloLanguageCompiler.Parsing.TokenWalker;
using static ApolloLanguageCompiler.Parsing.NodeParsingOutcome;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class TypedNodeParser : NodeParser
    {
        protected NodeTypes type;
        public TypedNodeParser(NodeTypes type)
        {
            this.type = type;
        }

        protected new string ParserName => base.ParserName ?? this.type.ToString();
    }
}
