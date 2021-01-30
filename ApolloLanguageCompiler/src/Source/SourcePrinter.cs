using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Crayon;

namespace ApolloLanguageCompiler.Source
{
    public class SourcePrinter
    {
        private readonly SourceCode source;

        public SourcePrinter(SourceCode source)
        {
            this.source = source;
        }

        public string WithLines()
        {
            StringBuilder builder = new StringBuilder();
            string[] lines = this.source.Lines.ToArray();

            for (int i = 0; i < lines.Count(); i++)
            {
                builder.AppendLine($"{i}: {lines[i]}");
            }
            return builder.ToString();
        }
        public string HighlightContext(SourceContext context)
        {
            string highlighted = SourceFormatter.Format(this.source, new SourceTransformation(context, n => string.IsNullOrWhiteSpace(n) ? n : "_"));

            StringBuilder builder = new StringBuilder();
            string[] lines = highlighted.Split(Environment.NewLine);

            for (int i = 0; i < lines.Count(); i++)
            {
                builder.AppendLine($"{i}: {lines[i]}");
            }
            return builder.ToString();
        }
        private bool isAlphaNum(string input) => !string.IsNullOrEmpty(input) && input.All(c => char.IsLetterOrDigit(c) && (c < 128));
            
        
    }
}
