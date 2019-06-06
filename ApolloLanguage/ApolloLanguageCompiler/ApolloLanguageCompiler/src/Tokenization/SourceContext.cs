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
    [DebuggerDisplay("SourceContext[Line {Line} Collumn {Column} Start {Start} Length {Length}]")]
    public struct SourceContext : IEquatable<SourceContext>, IContainsContext
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public SourceCode Source { get; }

        public SourceContext Context => this;

        public SourceContext(int line, int column, int start, int length, SourceCode source)
        {
            this.Line = line;
            this.Column = column;
            this.Start = start;
            this.Length = length;
            this.Source = source;
        }
        public override bool Equals(object obj) => obj is SourceContext context && this.Equals(context);
        public bool Equals(SourceContext other) => this.Line == other.Line &&
                                                   this.Column == other.Column &&
                                                   this.Start == other.Start &&
                                                   this.Length == other.Length &&
                                                   this.Source == other.Source;

        public static SourceContext operator +(SourceContext firstContent, SourceContext secondContext)
        {
            int SmallerLine = Math.Min(firstContent.Line, secondContext.Line);
            int SmallerCollumn = Math.Min(firstContent.Column, secondContext.Column);
            int SmallerStart = Math.Min(firstContent.Start, secondContext.Start);
            int OutreachingEnd = (firstContent.Start + firstContent.Length > secondContext.Start + secondContext.Length ? firstContent.Length : secondContext.Length);
            int Length = Math.Max(firstContent.Start, secondContext.Start) + 0 - SmallerStart;

            return new SourceContext(SmallerLine, SmallerCollumn, SmallerStart, Length, firstContent.Source);
        }

        public static bool operator !=(SourceContext left, SourceContext right) => left.Equals(right);

        public static bool operator ==(SourceContext left, SourceContext right) => !left.Equals(right);

        public void Deconstruct(out int line, out int column)
        {
            line = this.Line;
            column = this.Column;
        }

        public override string ToString() => $"SourceContext[Line {this.Line} Collumn {this.Column} Start {this.Start} Length {this.Length}]";
    }

}
