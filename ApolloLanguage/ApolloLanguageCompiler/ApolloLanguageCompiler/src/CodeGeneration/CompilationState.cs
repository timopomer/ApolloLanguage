using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GrEmit;


namespace ApolloLanguageCompiler.CodeGeneration
{
    public class CompilationState
    {
        private List<(string, GroboIL.Local)> locals;
        private List<(string, GroboIL.Label)> labels;
        private List<(string, MethodInfo)> methods;
        private readonly CompilationState parent;

        public CompilationState(CompilationState parent = null)
        {
            this.locals = new List<(string, GroboIL.Local)>();
            this.labels = new List<(string, GroboIL.Label)>();
            this.parent = parent;
        }

        public void Add(GroboIL.Local local, string name) => this.locals.Add((name, local));
        public void Add(GroboIL.Label label, string name) => this.labels.Add((name, label));
        public void Add(MethodInfo method, string name) => this.methods.Add((name, method));

        public GroboIL.Local GetLocalByName(string name, bool throwOnFailure = true)
        {
            GroboIL.Local parentLocal = this.parent?.GetLocalByName(name, throwOnFailure: false);
            try
            {
                return this.locals.Last(n => n.Item1.Equals(name)).Item2;
            }
            catch (Exception e)
            {
                if (throwOnFailure)
                    return parentLocal ?? throw new CouldNotFindLocalException();

                return parentLocal;
            }
        }

        public GroboIL.Label GetLabelByName(string name, bool throwOnFailure = true)
        {
            GroboIL.Label parentLabel = this.parent?.GetLabelByName(name, throwOnFailure: false);
            try
            {
                return this.labels.Last(n => n.Item1.Equals(name)).Item2;
            }
            catch (Exception e)
            {
                if (throwOnFailure)
                    return parentLabel ?? throw new CouldNotFindLabelException();

                return parentLabel;
            }
        }

        public MethodInfo GetMethodByName(string name, bool throwOnFailure = true)
        {
            MethodInfo parentMethod = this.parent?.GetMethodByName(name, throwOnFailure: false);
            try
            {
                return this.methods.Last(n => n.Item1.Equals(name)).Item2;
            }
            //todo: search in assembly
            catch (Exception e)
            {
                if (throwOnFailure)
                    return parentMethod ?? throw new CouldNotFindLabelException();

                return parentMethod;
            }
        }
    }
    public class CouldNotFindLabelException : Exception
    { }
    public class CouldNotFindLocalException : Exception
    { }
}
