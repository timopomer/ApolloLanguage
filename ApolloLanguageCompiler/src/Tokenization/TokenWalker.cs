using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Parsing
{
    //[DebuggerDisplay("TokenWalker[CurrentElement = {CurrentElement}]")]
    public class TokenWalker : IContainsContext
    {
        private readonly List<Token> streamReference;
        private IEnumerator<Token> tokenStream;

        public TokenWalker(IEnumerable<Token> stream)
        {
            this.streamReference = stream.ToList();
            this.CurrentElement = this.streamReference.First();
        }

        public TokenWalker(TokenWalker walker)
        {
            this.streamReference = walker.streamReference;
            this.CurrentElement = walker.CurrentElement;
        }

        public SourceContext Context => this.CurrentElement.Context;

        public Token CurrentElement
        {
            get => this.tokenStream.Current;
            set => this.tokenStream = this.GetEnumerator(value);
        }

        public IEnumerable<Token> GetWhile(params SyntaxKeyword[] possibleKeywords)
        {
            while (this.UpcomingIsType(possibleKeywords))
                yield return this.GetNext();
        }

        public void SkipWhitespace()
        {
            while (this.UpcomingIsType(SyntaxKeyword.Whitespace) || this.UpcomingIsType(SyntaxKeyword.EOL))
                this.GetNext();
        }

        public Token GetNext()
        {
            Token Last = this.CurrentElement;
            this.tokenStream.MoveNext();
            return Last;
        }

        public bool TryGetNext(params SyntaxKeyword[] possibleKeywords) => this.TryGetNext(out _, possibleKeywords);

        public bool TryGetNext(out Token result, params SyntaxKeyword[] possibleKeywords)
        {
            result = null;
            this.SkipWhitespace();
            bool successfullyParsed = this.UpcomingIsType(possibleKeywords);

            if (!successfullyParsed)
                return false;

            result = this.GetNext();
            return true;
        }

        public void Walk(StateWalker walker) => walker(this);

        public bool UpcomingIsType(params SyntaxKeyword[] possibleKeywords) => this.CurrentElement.OfType(possibleKeywords);

        public bool IsLast() => this.CurrentElement == this.streamReference.Last();

        public delegate void StateWalker(TokenWalker walker);

        public StateWalker State => (oldWalker) => oldWalker.CurrentElement = this.CurrentElement;

        private IEnumerator<Token> GetEnumerator(Token token)
        {
            IEnumerator<Token> LocatedEnumerator = this.streamReference.SkipWhile(n => n != token).GetEnumerator();
            LocatedEnumerator.MoveNext();
            return LocatedEnumerator;
        }

        public override string ToString()
        {
            StringBuilder Builder = new StringBuilder();
            Builder.AppendLine("[TokenWalker]");
            Builder.AppendLine($"Context: {this.Context}");
            Builder.AppendLine($"CurrentElement: {this.CurrentElement}");
            return Builder.ToString();
        }
    }
}
