namespace ApolloLanguageCompiler.Parsing
{
    public abstract class BinaryOperatorElement : OperatorElement
    {
        public ExpressionElement Left { get; private set; }
        public ExpressionElement Right { get; private set; }

        protected BinaryOperatorElement(ExpressionElement left, ExpressionElement right)
        {
            this.Left = left;
            this.Right = right;
        }
        protected BinaryOperatorElement(BinaryOperatorElement element)
        {
            this.Left = element.Left.Clone() as ExpressionElement;
            this.Right = element.Right.Clone() as ExpressionElement;
        }
	}
}
