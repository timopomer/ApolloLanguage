using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using static ApolloLanguageCompiler.Parsing.TokenWalker;

namespace ApolloLanguageCompiler.Parsing
{
    public class AssignmentElement : BinaryOperatorElement
    {
        public override object Clone() => new AssignmentElement(this);

        public AssignmentOperator Operator { get; private set; }

        public enum AssignmentOperator
        {
            Assignment
        }
        private static readonly Dictionary<SyntaxKeyword, AssignmentOperator> TypeConverter = new Dictionary<SyntaxKeyword, AssignmentOperator>
        {
            {SyntaxKeyword.Assignment, AssignmentOperator.Assignment  }
        };

        private AssignmentElement(ExpressionElement left, AssignmentOperator @operator, ExpressionElement right) : base(left, right)
        {
            this.Operator = @operator;
        }
        private AssignmentElement(AssignmentElement element) : base(element)
        {
            this.Operator = element.Operator;
        }

        public static bool TryParse(out ExpressionElement expression, out StateWalker walk, TokenWalker walker)
        {
            TokenWalker LocalWalker = new TokenWalker(walker);
            walk = LocalWalker.State;
            SourceContext Context = LocalWalker.Context;

            if (!TryParse(EqualityElement.TryParse, out expression, LocalWalker))
                return false;

            while (LocalWalker.TryGetNext(out Token AssignmentToken, SyntaxKeyword.Assignment))
            {
                if (EqualityElement.TryParse(out ExpressionElement RightExpresion, out StateWalker RightWalk, LocalWalker))
                {
                    LocalWalker.Walk(RightWalk);

                    expression = new AssignmentElement(expression, TypeConverter[AssignmentToken.Kind], RightExpresion);
                    expression.SetContext(Context, LocalWalker);
                }
                else
                    return false;
            }
            return true;
        }       
	}
}
