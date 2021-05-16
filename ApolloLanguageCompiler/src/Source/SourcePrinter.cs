using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Crayon;

namespace ApolloLanguageCompiler.Source
{
    public static class SourcePrinter
    {
        public static string WithLineNumbers(this SourceCode source)
        {
            string[] lines = source.Lines.ToArray();
            return SourcePrinter.WithLineNumbers(lines);
        }

        private static string WithLineNumbers(string[] lines)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < lines.Count(); i++)
            {
                builder.AppendLine($"{i}: {lines[i]}");
            }
            return builder.ToString();
        }

        public static string HighlightContext(this SourceCode source, SourceContext context)
        {
            string highlighted = SourceFormatter.Format(source, new SourceTransformation(context, n => string.IsNullOrWhiteSpace(n) ? n : "_"));

            string[] lines = highlighted.Split(Environment.NewLine);
            return SourcePrinter.WithLineNumbers(lines);
        }

        public static string HighlightLocation(this SourceCode source, SourceLocation location)
        {
            string highlighted = SourceFormatter.Format(source, new SourceTransformation(new SourceContext(location.Location, 1), n => $"|{n}"));

            string[] lines = highlighted.Split(Environment.NewLine);
            return SourcePrinter.WithLineNumbers(lines);
        }
    }
}
