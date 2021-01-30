using ApolloLanguageCompiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApolloLanguageCompiler.Parsing
{
    public class ParseResultHistory
    {
        private readonly List<ParsingResult> parsingResults;

        public ParseResultHistory()
        {
            this.parsingResults = new List<ParsingResult>();
        }

        public void AddResult(ParsingResult result)
        {
            this.parsingResults.Add(result);
        }

        public IEnumerable<ParsingResult> SuccessfulParsers => this.parsingResults.Where(n => n.Success);
        public IEnumerable<ParsingResult> Parsers => this.parsingResults;

        public string ParseHistory(SourceCode sourceCode)
        {
            SourcePrinter printer = sourceCode.Printer;
            StringBuilder builder = new StringBuilder();

            foreach (IEnumerable<ParsingResult> groupedResults in this.parsersGroupedBySimilarContext())
            {
                builder.AppendLine("==========");
                SourceContext context = null;
                foreach (ParsingResult result in groupedResults)
                {
                    context = result.Context;
                    builder.AppendLine(result.ToString());
                }
                builder.AppendLine(printer.HighlightContext(context));

            }

            return builder.ToString();
        }

        private IEnumerable<IEnumerable<ParsingResult>> parsersGroupedBySimilarContext()
        {
            IEnumerator<ParsingResult> enumerator = this.Parsers.GetEnumerator();
            while(enumerator.MoveNext())
            {
                if (enumerator.Current.Context.Length > 0)
                    yield return this.yieldParsersWithSameContext(enumerator);
            }
        }

        private IEnumerable<ParsingResult> yieldParsersWithSameContext(IEnumerator<ParsingResult> enumerator)
        {
            SourceContext groupContext = enumerator.Current.Context;
            while (enumerator.Current.Context == groupContext)
            {
                yield return enumerator.Current;
                if (!enumerator.MoveNext())
                    break;
            }
        }
    }
}
