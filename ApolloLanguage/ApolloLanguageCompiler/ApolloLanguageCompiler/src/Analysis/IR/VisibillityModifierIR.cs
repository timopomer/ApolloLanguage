using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApolloLanguageCompiler.Parsing.Nodes;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class VisibillityModifierIR : IR
    {
        [Flags]
        public enum Visibillity
        {
            Exposed = 0x0,
            Hidden = 0x1
        }
        public Visibillity Modifier { get; private set; }

        public VisibillityModifierIR(IEnumerable<VisibillityModifierNode> VMNodes, IR parent) : base(VMNodes, parent)
        {
            this.Modifier = VMNodes.Select(n => n.Visibillity).Aggregate((n, m) => n & m);
        }

        public static implicit operator Visibillity(VisibillityModifierIR visibillityIR) => visibillityIR.Modifier;

        public MethodAttributes ToAttribute()
        {
            switch (this.Modifier)
            {
                case Visibillity.Exposed:
                    return MethodAttributes.Public;
                case Visibillity.Hidden:
                    return MethodAttributes.Private;
                default:
                    throw new Exception("Illegal attribute cannot be converted");
            }
        }
    }
}
