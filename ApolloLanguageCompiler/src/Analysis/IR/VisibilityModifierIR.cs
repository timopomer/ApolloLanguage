using ApolloLanguageCompiler.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Analysis
{
    public class VisibilityModifierIR : IR
    {
        public enum VisibilityModifier
        {
            Exposed,
            Hidden
        }
        public VisibilityModifier Visibility;

        public VisibilityModifierIR(VisibilityModifier visibility)
        {
            this.Visibility = visibility;
        }

        public static VisibilityModifierIR Parse(Node node)
        {
            return null;
            //if (node is ManyNode manyNode)
            //{
            //    subClasses = ParserHelpers.ParseNodes<ClassIR>(manyNode.Nodes);
            //    return new ClassIR(subClasses);
            //}
            //throw new Exception("Parsing failed");
        }
    }
}
