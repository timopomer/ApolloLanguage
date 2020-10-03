using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Source
{
    public struct SourceTransformation
    {
        public readonly SourceContext Context;
        public readonly Func<string, string> Transformation;

        public string Apply(string input) => this.Transformation(input);

        internal void Deconstruct(out SourceContext context, out Func<string, string> transformation)
        {
            context = this.Context;
            transformation = this.Transformation;
        }

        public SourceTransformation(SourceContext context, Func<string, string> transformation)
        {
            this.Context = context;
            this.Transformation = transformation;
        }
    }
}
