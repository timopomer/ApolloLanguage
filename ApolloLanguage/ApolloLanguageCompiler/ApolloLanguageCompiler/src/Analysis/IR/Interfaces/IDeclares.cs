using System;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public interface IDeclares<T> where T : IR
    {
        bool Declares(T t);
        TypeExpression TypeOf(T t);
    }
    public struct Declaration<T> where T : IR
    {
        public IDeclares<T> Declarer;
        public TypeExpression Type;
    }
}
