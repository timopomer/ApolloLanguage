using ApolloLanguageCompiler.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class ParsingResult
    {
        public readonly NodeParser Parser;
        public readonly bool Success;

        public ParsingResult(NodeParser parser, bool success)
        {
            this.Parser = parser;
            this.Success = success;
        }

        public abstract SourceLocation Location { get; }
    }

    public class SuccessfulParsingResult : ParsingResult
    {
        public readonly SourceContext Context;

        public SuccessfulParsingResult(SourceContext context, NodeParser parser) : base(parser, success: true)
        {
            this.Context = context;
        }

        public override SourceLocation Location => this.Context.Location;

        public override string ToString()
        {
            return $"SuccessfulParsingResult [{this.Context} {this.Parser} (Success)]";
        }
    }

    public class FailedParsingResult : ParsingResult
    {
        private readonly SourceLocation location;

        public FailedParsingResult(SourceLocation location, NodeParser parser) : base(parser, success: true)
        {
            this.location = location;
        }

        public override SourceLocation Location => this.location;

        public override string ToString()
        {
            return $"FailedParsingResult [{this.Location} {this.Parser} (Failed)]";
        }
    }
}
