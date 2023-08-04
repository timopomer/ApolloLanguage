using ApolloLanguageCompiler.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace ApolloLanguageCompiler.Analysis
{
    public static class ParserHelpers
    {
        public class PlaceHolderIR : IR { }

        public static (List<T1>,List<T2>) ParseNodes<T1,T2>(List<Node> nodes)
        {
            List<T1> t1List = new List<T1>();
            List<T2> t2List = new List<T2>();

            foreach (Node subNode in nodes)
            {
                IR parsedNode = IR.ParseNode(subNode);
                switch (parsedNode)
                {
                    case T1 t1:
                        t1List.Add(t1);
                        break;
                    case T2 t2:
                        t2List.Add(t2);
                        break;
                    case null:
                        throw new Exception("IR parser returned null");
                    default:
                        throw new Exception("Unknown IR result");
                }
            }

            return (t1List, t2List);

        }
        public static List<T1> ParseNodes<T1>(List<Node> nodes)
        {
            List<T1> t1List;

            (t1List, _) = ParseNodes<T1, PlaceHolderIR>(nodes);

            return t1List;
        }

        public static T FirstNullOrError<T>(this IEnumerable<T> ienumerable)
        {
            if (ienumerable.Count() > 1)
                throw new Exception("Too many elements");
            return ienumerable.FirstOrDefault();
        }
    }
}
