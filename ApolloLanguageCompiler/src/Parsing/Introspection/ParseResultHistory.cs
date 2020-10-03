using System;
using System.Collections.Generic;
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

    }
}
