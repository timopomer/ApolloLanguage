using System;
using System.Collections.Generic;
using System.Text;
using ApolloLanguageCompiler.Parsing;

namespace ApolloLanguageCompiler.Analysis
{
    public class IRBuilder
    {
        private readonly Node headNode;
        public IRBuilder(Node headNode)
        {
            this.headNode = headNode;
        }

        public IR BuildIR()
        {
            return null;
        }
    }
}
