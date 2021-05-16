using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Crayon;
using Microsoft.VisualBasic.CompilerServices;

namespace ApolloLanguageCompiler.Source
{
    public interface IContainsContext
    {
        SourceContext Context { get; }
    }

    public class SourceContext : IEquatable<SourceContext>, IComparable<SourceContext>, IContainsContext, IContainsLocation
    {
        public readonly int Start;
        public readonly int Length;

        public int End => this.Start + this.Length;

        public SourceContext Context => this;

        public SourceLocation Location => new SourceLocation(this.Start);
        public SourceLocation EndLocation => new SourceLocation(this.Start + this.Length);

        public SourceContext(int start, int length)
        {
            this.Start = start;
            this.Length = length;
        }

        public override bool Equals(object obj) => obj is SourceContext context && this.Equals(context);
        public bool Equals(SourceContext other) => this.Start == other.Start && this.Length == other.Length;

        public static bool operator !=(SourceContext left, SourceContext right) => !left.Equals(right);
        public static bool operator ==(SourceContext left, SourceContext right) => left.Equals(right);

        public SourceContext To(IContainsContext other) => this.To(other.Context);

        public SourceContext To(SourceContext other)
        {
            if (this.Start > other.Start)
                throw new Exception("First context after second context");

            int longerEnd = Math.Max(this.End, other.End);
            int combinedLength = longerEnd - this.Start;
            return new SourceContext(this.Start, combinedLength);
        }
        public static bool operator <(SourceContext left, SourceContext right)
        {
            switch (left.CompareTo(right))
            {
                case -1:
                    return true;
                case 0:
                    return true;
                case 1:
                    return false;
            }
            throw new Exception("Illegal return value from CompareTo");
        }

        public static bool operator >(SourceContext left, SourceContext right) => !(left < right);
        
        public override string ToString() => $"SourceContext[Start {this.Start} Length {this.Length}]";

        public int CompareTo(SourceContext other)
        {
            if (this.Start < other.Start)
                return -1;
            else if (this.Start > other.Start)
                return 1;
            else
                return 0;
        }
    }

}
