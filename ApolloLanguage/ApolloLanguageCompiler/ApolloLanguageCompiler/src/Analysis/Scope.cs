using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Types;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class Scope : IEnumerable<IR>
    {
        private List<IR> declaredInside;
        private Scope parent;

        public Scope(Scope parent=null)
        {
            this.declaredInside = new List<IR>();
            this.parent = parent;
        }

        public static bool operator <(Scope left, Scope right) => left.Select(n => n.Scope).Contains(right);
        public static bool operator >(Scope left, Scope right) => right.Select(n => n.Scope).Contains(left);

        public Declaration<T> GetDeclaration<T>(T element) where T : IR
        {
            List<IDeclares<T>> declarations = this.InScope.OfType<IDeclares<T>>().Where(n => n.Declares(element)).ToList();

            if (!declarations.Any())
                throw new IdentifierDeclarationNotFoundException();

            IDeclares<T> declarer = declarations.First();
            return new Declaration<T> { Declarer = declarer, Type = declarer.TypeOf(element) };
        }


        public void Add(IR ir) => this.declaredInside.Add(ir);
        public void AddRange(IEnumerable<IR> irs) => this.declaredInside.AddRange(irs);

        public Scope Enter() => new Scope(parent: this);

        public IEnumerable<IR> InScope => this.RecursiveInside.Concat(this.parent?.InScope ?? new IR[] { }).Distinct();

        public IEnumerable<IR> RecursiveInside => this.SelectMany(n => n.Scope.RecursiveInside).Distinct();
        public IEnumerable<IR> Inside => this;

        public IEnumerator<IR> GetEnumerator() => this.declaredInside.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
