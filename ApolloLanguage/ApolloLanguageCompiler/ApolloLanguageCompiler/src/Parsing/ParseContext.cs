using System;
using System.Diagnostics;
using System.Text;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler
{
    public class ParseContext
    {
        public StackFrame Frame { get; private set; }
        public SyntaxKeyword[] PossibleKeywords { get; private set; }
        public SourceContext Context { get; private set; }
        public bool Parsed { get; private set; }
        public Token Recieved { get; private set; }

        public ParseContext(StackFrame frame, SyntaxKeyword[] possibleKeywords, Token recieved, SourceContext context, bool parsed)
        {
            this.Frame = frame;
            this.PossibleKeywords = possibleKeywords;
            this.Context = context;
            this.Parsed = parsed;
            this.Recieved = recieved;
        }
        public override string ToString()
        {
            StringBuilder Builder = new StringBuilder();
            Builder.AppendLine($"[ParseContext]");
            Builder.AppendLine($"Requested: {string.Join("|", this.PossibleKeywords)}");
            Builder.AppendLine($"Recieved: {this.Recieved}");
            Builder.AppendLine($"Context: {this.Context}");
            Builder.AppendLine($"Called by: {this.Frame.GetMethod().DeclaringType.Name}");
            Builder.AppendLine($"HasParsed: {this.Parsed}");
            return Builder.ToString();
        }


    }
}
