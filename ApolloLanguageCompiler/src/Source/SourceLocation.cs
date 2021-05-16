using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Source
{
    public interface IContainsLocation
    {
        SourceLocation Location { get; }
    }

    public class SourceLocation
    {
        public readonly int Location;

        public SourceLocation(int location)
        {
            this.Location = location;
        }

        public SourceContext To(SourceLocation other)
        {
            if (other.Location < this.Location)
                throw new Exception("Other SourceLocation is before this one");

            return new SourceContext(this.Location, other.Location - this.Location);
        }

        public override bool Equals(object obj) => obj is SourceLocation location && this.Equals(location);
        public bool Equals(SourceLocation other) => this.Location == other.Location;

        public static bool operator !=(SourceLocation left, SourceLocation right) => !left.Equals(right);
        public static bool operator ==(SourceLocation left, SourceLocation right) => left.Equals(right);


        public override string ToString() => $"SourceLocation[Location {this.Location}]";

    }
}
