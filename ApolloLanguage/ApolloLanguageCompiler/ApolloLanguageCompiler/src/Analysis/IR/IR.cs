using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloLanguageCompiler.Analysis.IR.Expression;
using ApolloLanguageCompiler.CodeGeneration;
using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.Parsing.Nodes;
using Newtonsoft.Json;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public abstract class IR : IContainsContext
    {
        public SourceContext Context { get; private set; }

        [JsonIgnore]
        public Scope Scope { get; private set; }

        [JsonIgnore]
        public IR Parent { get; private set; }

        public virtual bool IsLegal => true;

        protected IR(IEnumerable<IContainsContext> containContext, IR parent) :
        this(containContext.Select(n => n.Context), parent)
        { }

        protected IR(IEnumerable<SourceContext> contexts, IR parent) :
        this(contexts.Aggregate((m, n) => m + n), parent)
        { }

        protected IR(IContainsContext containsContext, IR parent) :
        this(containsContext?.Context, parent)
        { }

        protected IR(SourceContext? context, IR parent)
        {
            this.Context = context.GetValueOrDefault();
            this.Parent = parent;
            this.Scope = parent?.Scope.Enter() ?? new Scope();
            parent?.Scope.Add(this);
        }
        public static IR Transform(ASTNode node, IR parent)
        {
            switch (node)
            {
                case FunctionNode FNode: return new FunctionIR(FNode, parent);
                case VariableDeclarationNode VDNode: return new VariableDeclarationIR(VDNode, parent);
                case ReturnNode RNode: return new ReturnIR(RNode, parent);
                case ExpressionNode ENode: return ExpressionIR.TransformExpression(ENode.Expression, parent);
                default: throw new UnexpectedNodeException(node.ToString());
            }
        }
        public void TryEmit<T1>(T1 t1, bool throwOnFailure = false)
        {
            switch (this)
            {
                case IEmitsFor<T1> emittable:
                    emittable.EmitFor(t1);
                    break;
                default:
                    if (throwOnFailure)
                        throw new Exception("Couldnt emit for IR");
                    break;
            }
        }

        public void TryEmit<T1, T2>(T1 t1, T2 t2, bool tryLessParams = false, bool throwOnFailure = false)
        {
            switch (this)
            {
                case IEmitsFor<T1> emittable when tryLessParams:
                    emittable.EmitFor(t1);
                    break;
                case IEmitsFor<T1,T2> emittable:
                    emittable.EmitFor(t1, t2);
                    break;
                default:
                    if (throwOnFailure)
                        throw new Exception("Couldnt emit for IR");
                    break;
            }
        }

        public void TryEmit<T1, T2, T3>(T1 t1, T2 t2, T3 t3, bool tryLessParams = false, bool throwOnFailure = false)
        {
            switch (this)
            {
                case IEmitsFor<T1> emittable when tryLessParams:
                    emittable.EmitFor(t1);
                    break;
                case IEmitsFor<T1, T2> emittable when tryLessParams:
                    emittable.EmitFor(t1, t2);
                    break;
                case IEmitsFor<T1, T2, T3> emittable:
                    emittable.EmitFor(t1, t2, t3);
                    break;
                default:
                    if (throwOnFailure)
                        throw new Exception("Couldnt emit for IR");
                    break;
            }
        }
    }
}
