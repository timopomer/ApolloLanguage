using ApolloLanguageCompiler.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Parsing
{
    public class ParsingResult
    {
        private readonly SourceContext context;
        private readonly NodeParser parser;
        private readonly bool success;

        public ParsingResult(SourceContext context, NodeParser parser, bool success)
        {
            this.context = context;
            this.parser = parser;
            this.success = success;
        }
    }
}
