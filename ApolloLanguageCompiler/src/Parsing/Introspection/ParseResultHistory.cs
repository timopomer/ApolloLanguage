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

        public IEnumerable<SuccessfulParsingResult> SuccessfulParsers => this.parsingResults.OfType<SuccessfulParsingResult>();
        public IEnumerable<ParsingResult> Parsers => this.parsingResults;

        public string ParseHistory(SourceCode sourceCode)
        {
            StringBuilder builder = new StringBuilder();
            IEnumerable<ParsingResult> parsers = this.Parsers;
            while (parsers.Any())
            {
                ParsingResult parser = parsers.Take(1).First();
                IEnumerable<ParsingResult> parsersInSameLocation = parsers.TakeWhile(n => n.Location == parser.Location);
                parsers = parsers.Skip(parsersInSameLocation.Count());

                builder.AppendLine("==========");
                foreach (ParsingResult result in parsersInSameLocation)
                {
                    builder.AppendLine(result.ToString());
                }
                if (parser is SuccessfulParsingResult successfulResult)
                {
                    builder.AppendLine(sourceCode.HighlightContext(successfulResult.Context));
                }
                else if (parser is FailedParsingResult failedResult)
                {
                    builder.AppendLine(sourceCode.HighlightLocation(failedResult.Location));
                }
                else
                    throw new Exception("Unknown ParsingResult type");
            }
            return builder.ToString();
        }
    }
}
