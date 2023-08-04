using ApolloLanguageCompiler.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApolloLanguageCompiler.Analysis
{
    public class ProgramIR : IR
    {
        public List<ClassIR> Classes;
        public ProgramIR(List<ClassIR> classes)
        {
            this.Classes = classes;
        }

        public static ProgramIR Parse(Node node)
        {
            List<ClassIR> classes;
            if (node is ManyNode manyNode)
            {
                classes = ParserHelpers.ParseNodes<ClassIR>(manyNode.Nodes);
                return new ProgramIR(classes);
            }
            throw new Exception("Parsing failed");
        }
    }
}
