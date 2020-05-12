using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApolloLanguageCompiler.Tokenization
{
    public class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException(params SyntaxKeyword[] keywords) : base($"UnexpectedTokenException: {string.Join("|", keywords)}") { }
        public UnexpectedTokenException(params Token[] tokens) : base($"UnexpectedTokenException: {string.Join("|", tokens.Select(n => n.Kind))}") { }

    }
    //[DebuggerDisplay("Token[Value = {EscapedString} Context = {Context} Kind = {Kind}]")]
    public class Token : IEquatable<Token>, IContainsContext
    {
        public SourceContext Context { get; private set; }
        public string Value { get; }

        public string EscapedString => Regex.Escape(this.Value);

        public SyntaxKeyword Kind { get; }
        public bool OfType(params SyntaxKeyword[] possibleKeywords) => possibleKeywords.Contains(this.Kind);

        public bool Equals(Token other)
        {
            if (other is null)
                return false;

            return this.Context.Equals(other.Context) && this.Value == other.Value && this.Kind == other.Kind;
        }
        public override bool Equals(object obj) => this.Equals(obj as Token);

        public Token(string innerValue, SyntaxKeyword type, SourceContext context)
        {
            this.Value = innerValue;
            this.Kind = type;
            this.Context = context;
        }
        public override string ToString() => $"Token[{this.Kind}->{this.Value}]";
    }

    public enum SyntaxKeyword
    {
        LiteralStr,
        LiteralLetter,
        LiteralNumber,
        LiteralFalse,
        LiteralTrue,

        Colon,
        SemiColon,
        Whitespace,
        EOL,

        Comma,
        Dot,

        Assignment,
        OpenParenthesis,
        CloseParenthesis,
        OpenCurlyBracket,
        CloseCurlyBracket,
        OpenSquareBracket,
        CloseSquareBracket,

        Plus,
        Minus,
        Negate,
        Multiply,
        Mod,
        Power,
        Divide,
        Root,
        Equal,
        InEqual,
        Lesser,
        LesserEqual,
        Greater,
        GreaterEqual,
        Xor,
        And,
        ShortAnd,
        Or,
        ShortOr,

        Letter,
        Number,
        Boolean,
        Str,

        Return,
        //Skip,
        //Stop,

        Class,
        Instance,
        Extension,

        Exposed,
        Hidden,

        Identifier,

        Use
    }



}
