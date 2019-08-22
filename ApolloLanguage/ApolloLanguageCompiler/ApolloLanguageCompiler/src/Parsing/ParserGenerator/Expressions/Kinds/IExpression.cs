
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApolloLanguageCompiler.Parsing
{
    public interface IExpression
    {
        void AddChild(IExpression parsed);
    }
}
