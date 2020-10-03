using Crayon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ApolloLanguageCompiler.Source
{
    public class SourceFormatter
    {
        private readonly SourceCode source;

        public SourceFormatter(SourceCode source)
        {
            this.source = source;
        }

        public static IEnumerable<SourceTransformation> UnrollNested(IEnumerable<SourceTransformation> transformations)
        {
            IEnumerable<SourceTransformation> sortedTransformations = transformations.OrderBy(n => n.Context.Context);
            int maxEnding = transformations.Max(n => n.Context.End);
            Func<string,string>[] numeratedTransformations = new Func<string, string>[maxEnding];

            //this is ineffecient as the same transformation for the length of two will divide into two different transformations
            foreach (SourceTransformation transformation in sortedTransformations)
            {
                for (int i = transformation.Context.Start; i<transformation.Context.End; i++)
                {
                    numeratedTransformations[i] = transformation.Apply;
                }
            }

            IEnumerable<SourceTransformation> perCharTransformation = numeratedTransformations.Select((n, index) => new SourceTransformation(new SourceContext(index, 1), n)).Where(m=>m.Transformation != null);

            return perCharTransformation;
        }

        public static string Format(SourceCode source, params SourceTransformation[] transformation)
        {
            SourceFormatter formatter = new SourceFormatter(source);
            return formatter.format(transformation);
        }

        private string format(params SourceTransformation[] transformations)
        {
            StringBuilder builder = new StringBuilder();

            IEnumerable<SourceTransformation> unrolledTransformation = UnrollNested(transformations);
            // UnrollNested returns the transformations in an ordered manner

            int index = 0;
            foreach ((SourceContext context, Func<string, string> transformation) in unrolledTransformation)
            {
                if (index != context.Start)
                {
                    string toAppendUntilStart = this.source.Code.Substring(index, context.Start - index);
                    builder.Append(toAppendUntilStart);
                    index = context.Start;
                }

                string toAppend = this.source.Code.Substring(index, context.Length);
                string transformed = transformation(toAppend);
                builder.Append(transformed);

                index += context.Length;
            }

            builder.Append(this.source.Code.Substring(index));

            return builder.ToString();
        }
        
    }
}
