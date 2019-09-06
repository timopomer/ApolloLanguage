using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler
{
    public class SourceCode
    {
        public string Code { get; private set; }
        public string[] Split { get; }

        public SourceCode(string code)
        {
            this.Code = code;
            this.Split = code.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );
        }
        public static SourceCode FromFiles(IEnumerable<string> files)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string path in files)
            {
                string contents = File.ReadAllText(path);
                builder.Append(contents);
            }
            return new SourceCode(builder.ToString());
        }
    }
}
