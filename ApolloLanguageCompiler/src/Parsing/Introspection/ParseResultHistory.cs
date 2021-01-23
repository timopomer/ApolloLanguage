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
        public string SuccessfulParsers()
        {
            return string.Join(Environment.NewLine, this.parsingResults.Where(n => n.Success).Select(n => n.ToString()));
        }
    }
}
