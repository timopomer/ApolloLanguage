using ApolloLanguageCompiler.Parsing;
using ApolloLanguageCompiler.Tokenization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Tokenization
{
    public class SourceCode
    {
        public string Code { get; private set; }

        public SourceCode(string code)
        {
            this.Code = code;
        }

        public IEnumerable<string> Lines => this.Code.Split(Environment.NewLine);

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
