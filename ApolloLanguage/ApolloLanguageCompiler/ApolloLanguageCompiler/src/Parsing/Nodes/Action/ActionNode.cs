﻿using System;
using ApolloLanguageCompiler.Tokenization;

namespace ApolloLanguageCompiler.Parsing.Nodes
{
    public abstract class ActionNode : ASTNode
    {
        public override abstract object Clone();
	}
}
