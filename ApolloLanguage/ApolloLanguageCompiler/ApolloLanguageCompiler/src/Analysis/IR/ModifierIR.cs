using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ApolloLanguageCompiler.Parsing;
using System.Reflection;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class ModifierIR : IR
    {
        public InstanceModifierIR Instanceness { get; private set; }
        public VisibillityModifierIR Visibillity { get; private set; }

        public ModifierIR(ModifierNode MNode, IR parent) :
            this(MNode.InstanceModifier, parent, MNode.VisibilityModifiers)
        { }

        public ModifierIR(InstanceModifierNode IMNode, IR parent, params VisibillityModifierNode[] VMNodes) :
            this(IMNode, parent, VMNodes.AsEnumerable())
        { }

        public ModifierIR(InstanceModifierNode IMNode, IR parent, IEnumerable<VisibillityModifierNode> VMNodes) : base(IMNode, parent)
        {
            this.Instanceness = new InstanceModifierIR(IMNode, this);
            this.Visibillity = new VisibillityModifierIR(VMNodes, this);
        }

        public MethodAttributes ToAttribute() => this.Instanceness.ToAttribute() | this.Visibillity.ToAttribute();
    }
}
