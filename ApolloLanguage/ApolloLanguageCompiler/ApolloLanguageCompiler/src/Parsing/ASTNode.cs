using System;
using ApolloLanguageCompiler.Tokenization;
using ApolloLanguageCompiler.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract class ASTNode : IContainsContext, ICloneable
    {
        public abstract object Clone();
        public SourceContext Context => this._oldContext + this._contextContainer.Context;
        private SourceContext _oldContext;

        private IContainsContext _contextContainer;

        protected ASTNode()
        {
        }

        public void SetContext(IContainsContext container)
        {
            this._oldContext = container.Context;
            this._contextContainer = container;
        }
        public void SetContext(SourceContext context, IContainsContext container)
        {
            this._oldContext = context;
            this._contextContainer = container;
        }

        public delegate bool Parser<T>(T parentNode, TokenWalker walker);
        public static bool Parse<T>(T parentNode, TokenWalker walker, params Parser<T>[] parsers) where T : ASTNode
        {
            SourceContext Context = walker.Context;
            if (!parsers.Any())
                throw new ZeroParsersException();

            Dictionary<Parser<T>, (string name, bool success, TokenWalker localwalker)> ParserRecord = new Dictionary<Parser<T>, (string, bool, TokenWalker)>();
            foreach (Parser<T> parser in parsers)
            {
                TokenWalker LocalWalker = new TokenWalker(walker);
                bool parsed = parser(parentNode, LocalWalker);
                ParserRecord[parser] = (parser.Method.DeclaringType.Name, parsed, LocalWalker);
            }

            int successfulParsers = ParserRecord.Count(n => n.Value.success);
            bool succeededParsing = successfulParsers > 0;

            if (successfulParsers > 1)
                throw new AmbiguousParserException();

            if (succeededParsing)
                walker.CurrentElement = ParserRecord.First(n => n.Value.success).Value.localwalker.CurrentElement;

            return succeededParsing;
        }

    }

}
