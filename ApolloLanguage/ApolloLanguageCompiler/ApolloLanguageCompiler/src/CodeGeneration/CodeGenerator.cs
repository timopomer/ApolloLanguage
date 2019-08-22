using System;
using System.Reflection;
using System.Reflection.Emit;
using ApolloLanguageCompiler.Analysis.IR;
using ApolloLanguageCompiler.Analysis.IR.Expressions.Literals;
using GrEmit;

namespace ApolloLanguageCompiler.CodeGeneration
{
    public class CodeGenerator
    {
        private readonly ProgramIR program;
        private readonly string filename;

        public CodeGenerator(ProgramIR program, string filename)
        {
            this.program = program;
            this.filename = filename;
        }
        public AssemblyBuilder Compile()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            AssemblyName assemblyName = new AssemblyName
            {
                Name = this.filename
            };
            AssemblyBuilder assembly = domain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
            ModuleBuilder module = assembly.DefineDynamicModule(assemblyName.Name, this.filename);

            foreach (ClassIR @class in this.program.Classes)
            {
                @class.EmitFor(module);
            }
            var EntryPoint = module.GetType("Program").GetMethod("Main");
            assembly.SetEntryPoint(EntryPoint, PEFileKinds.ConsoleApplication);

            return assembly;
        }
    }
}
