using System;
using System.Collections.Generic;
using System.Linq;

namespace ApolloLanguageCompiler.Parsing
{
    public class ParameterElement : ConstructElement
    {
        public override object Clone() => new ParameterElement(this);
        public List<ExpressionElement> Parameters { get; private set; }
        public ParameterElement(IEnumerable<ExpressionElement> parameters, SourceContext context)
        {
            this.Parameters = parameters.ToList();
            this.SetContext(context);
        }

        public ParameterElement(ParameterElement element)
        {
            this.Parameters = element.Parameters.Clone();
        }
    }
}
