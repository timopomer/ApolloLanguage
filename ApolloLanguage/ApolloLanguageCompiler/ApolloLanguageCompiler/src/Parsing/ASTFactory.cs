using System;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Tokenization;
using ApolloLanguageCompiler.Parsing;

namespace ApolloLanguageCompiler.Parsing
{
    public class ASTFactory
    {
        private readonly TokenWalker walker;
        public ASTFactory(IEnumerable<Token> stream)
        {
            this.walker = new TokenWalker(stream);
        }

        public ASTNode Generate()
        {
			if(!ProgramNode.TryParse(out ProgramNode PNode, this.walker))
				throw new FailedParsingException();
			
            return PNode;
        }
    }
}
