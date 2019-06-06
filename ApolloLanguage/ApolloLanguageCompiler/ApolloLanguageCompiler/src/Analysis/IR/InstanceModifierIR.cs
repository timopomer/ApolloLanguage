using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using ApolloLanguageCompiler.Parsing.Nodes;

namespace ApolloLanguageCompiler.Analysis.IR
{
    public class InstanceModifierIR : IR
    {
        public enum Instanceness
        {
            Instance,
            Extension
        }
        public Instanceness Modifier { get; private set; }

        public InstanceModifierIR(InstanceModifierNode IMNode, IR parent) : base(IMNode, parent)
        {
            this.Modifier = IMNode.Instanceness;
        }
        public static implicit operator Instanceness(InstanceModifierIR modifierIR) => modifierIR.Modifier;

        public MethodAttributes ToAttribute()
        {
            switch(this.Modifier)
            {
                case Instanceness.Instance:
                    return MethodAttributes.ReuseSlot;
                case Instanceness.Extension:
                    return MethodAttributes.Static;
                default:
                    throw new Exception("Illegal attribute cannot be converted");
            }
        }
    }
}
