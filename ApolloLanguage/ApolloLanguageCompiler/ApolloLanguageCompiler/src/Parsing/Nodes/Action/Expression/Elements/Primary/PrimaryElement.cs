using ApolloLanguageCompiler.Tokenization;
using System;
using System.Linq;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public abstract partial class PrimaryElement : ExpressionElement
    {
        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            expression = null;

            if (TryParse(PrimaryElement.LiteralElement.TryParse, out expression, LocalWalker))
                return true;
            
            if (TryParse(PrimaryElement.TypeElement.TryParse, out expression, LocalWalker))
                return true;
            
            if (TryParse(PrimaryElement.IdentifierElement.TryParse, out expression, LocalWalker))
                return true;
            
            if (TryParse(PrimaryElement.ParanthesizedElement.TryParse, out expression, LocalWalker))
                return true;

            return false;
        }       
	}
}
