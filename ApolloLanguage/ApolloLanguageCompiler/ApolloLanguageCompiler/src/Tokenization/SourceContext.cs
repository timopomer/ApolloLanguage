using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler
{
    public interface IContainsContext
    {
        SourceContext Context { get; }
    }

    public struct SourceContext : IEquatable<SourceContext>, IContainsContext
    {
        private readonly int start;
        private readonly int length;
        private readonly SourceCode sourceReference;

        private int end => this.start + this.length;

        public SourceContext Context => this;

        public SourceContext(int start, int length, SourceCode sourceReference)
        {
            this.start = start;
            this.length = length;
            this.sourceReference = sourceReference;
        }

        public override bool Equals(object obj) => obj is SourceContext context && this.Equals(context);
        public bool Equals(SourceContext other) => this.start == other.start && this.length == other.length && this.sourceReference == other.sourceReference;

        public static bool operator !=(SourceContext left, SourceContext right) => left.Equals(right);
        public static bool operator ==(SourceContext left, SourceContext right) => !left.Equals(right);

        public SourceContext To(SourceContext other)
        {
            if (this.sourceReference != other.sourceReference)
                throw new Exception("Contexts not pointing to same source");

            if (this.start > other.start)
                throw new Exception("First context after second context");

            int longerEnd = Math.Max(this.end, other.end);
            int combinedLength = longerEnd - this.start;
            return new SourceContext(this.start, combinedLength, this.sourceReference);
        }

        public string ContextLocation
        {
            get
            {
                string beforeContext = this.sourceReference.Code.Substring(0, this.start);
                string context = this.sourceReference.Code.Substring(this.start, this.length);
                string afterContext = this.sourceReference.Code.Substring(this.end, this.sourceReference.Code.Length - this.end);

                return $"{beforeContext}------------>{context}<------------{afterContext}";
            }
        }
        public override string ToString() => $"SourceContext[Start {this.start} Length {this.length}]";
    }

}
