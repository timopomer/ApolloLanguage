using System;
using GrEmit;

namespace ApolloLanguageCompiler.CodeGeneration
{
    public interface IEmitsFor<T1>
    {
        void EmitFor(T1 t1);
    }
    public interface IEmitsFor<T1, T2>
    {
        void EmitFor(T1 t1, T2 t2);
    }
    public interface IEmitsFor<T1, T2, T3>
    {
        void EmitFor(T1 t1, T2 t2, T3 t3);
    }
}
