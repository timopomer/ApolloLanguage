namespace ApolloLanguageCompiler.Parsing
{
    public abstract class UnaryOperatorElement : OperatorElement
    {
        public ExpressionElement Expression { get; private set; }

        protected UnaryOperatorElement(ExpressionElement expression)
        {
            this.Expression = expression;
        }
        protected UnaryOperatorElement(UnaryOperatorElement element)
        {
            this.Expression = element.Expression.Clone() as ExpressionElement;
        }
	}
}
