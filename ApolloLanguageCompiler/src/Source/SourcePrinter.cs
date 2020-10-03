using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
