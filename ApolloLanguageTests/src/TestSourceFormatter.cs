using NUnit.Framework;
using System;
using ApolloLanguageCompiler.Parsing;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ApolloLanguageCompiler.CLI;
using ApolloLanguageCompiler.Source;

namespace ApolloLanguageCompiler.Tests
{
    [TestFixture()]
    public class TestSourceFormatter
    {
        [Test()]
        public void TestUnrolling()
        {
            SourceCode source = new SourceCode("abc123");
            SourceTransformation transformation = new SourceTransformation(new SourceContext(0, 4), n => "a");
            SourceTransformation transformationNested = new SourceTransformation(new SourceContext(1, 1), n => "b");
            var unrolled = SourceFormatter.UnrollNested(new[] { transformation, transformationNested }).ToArray();
            Assert.IsNotNull(unrolled);
            Assert.AreEqual(unrolled.Length, 4);
            Assert.AreEqual(unrolled[0].Context, new SourceContext(0, 1));
            Assert.AreEqual(unrolled[1].Context, new SourceContext(1, 1));
            Assert.AreEqual(unrolled[2].Context, new SourceContext(2, 1));
            Assert.AreEqual(unrolled[3].Context, new SourceContext(3, 1));

        }

        [Test()]
        public void TestUnrollingWithHigherIndexThanZero()
        {
            SourceCode source = new SourceCode("abc123");
            SourceTransformation transformation1 = new SourceTransformation(new SourceContext(2, 3), n => "2");
            SourceTransformation transformation2 = new SourceTransformation(new SourceContext(3, 1), n => "3");
            var unrolled = SourceFormatter.UnrollNested(new[] { transformation1, transformation2 }).ToArray();
            Assert.IsNotNull(unrolled);
            Assert.AreEqual(unrolled.Length, 3);
            Assert.AreEqual(unrolled[0].Context, new SourceContext(2, 1));
            Assert.AreEqual(unrolled[1].Context, new SourceContext(3, 1));
            Assert.AreEqual(unrolled[2].Context, new SourceContext(4, 1));

        }
        [Test()]
        public void TestBasicTransformation()
        {
            SourceCode source = new SourceCode("abc123");
            SourceTransformation transformation = new SourceTransformation(new SourceContext(0, 3), n => "a");
            string transformed = SourceFormatter.Format(source, transformation);
            Assert.AreEqual(transformed, "aaa123");
        }

        [Test()]
        public void TestNestedTransformation()
        {
            SourceCode source = new SourceCode("1111111");
            SourceTransformation transformation1 = new SourceTransformation(new SourceContext(2, 3), n => "2");
            SourceTransformation transformation2 = new SourceTransformation(new SourceContext(3, 1), n => "3");
            string transformed = SourceFormatter.Format(source, transformation1, transformation2);
            Assert.AreEqual(transformed, "1123211");
        }
    }
}
