using ApolloLanguageCompiler.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Parsing
{
    public class ParsingResult
    {
        public readonly SourceContext Context;
        public readonly NodeParser Parser;
        public readonly bool Success;

        public ParsingResult(SourceContext context, NodeParser parser, bool success)
        {
            this.Context = context;
            this.Parser = parser;
            this.Success = success;
        }

        public override string ToString()
        {
            return $"ParsingResult [{this.Context} {this.Parser} ({(this.Success ? "Success" : "Failure")})]";
        }
    }
}
