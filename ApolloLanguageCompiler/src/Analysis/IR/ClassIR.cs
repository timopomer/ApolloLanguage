using ApolloLanguageCompiler.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Analysis
{
    public class ClassIR : IR
    {
        public List<ClassIR> SubClasses;
        public ClassIR(List<ClassIR> subClasses)
        {
            this.SubClasses = subClasses;
        }

        public static ClassIR Parse(Node node)
        {
            List<ClassIR> subClasses;
            if (node is ManyNode manyNode)
            {
                subClasses = ParserHelpers.ParseNodes<ClassIR>(manyNode.Nodes);
                return new ClassIR(subClasses);
            }
            throw new Exception("Parsing failed");
        }
    }
}
 