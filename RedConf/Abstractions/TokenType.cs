using RedConf.Core;

namespace RedConf.Abstractions {
    public enum TokenType {
        Unknown,

        // Data
        [Token("\".*?\"", 1)]
        StringLiteral,

        [Token("[0-9][0-9]*", 1)]
        IntLiteral,

        [Token("true|false", 1)]
        BooleanLiteral,

        // Data Management
        [Token("\\=", 1)]
        Equal,

        // Structure
        [Token("[a-zA-Z_][a-zA-Z0-9_]*", 1)]
        Ident,
        [Token("[ \\t]+", 1)]
        Space,
        [Token("\\n", 1)]
        NewLine,

        [Token("\\{", 1)]
        StartBlock,
        [Token("\\}", 1)]
        EndBlock,

        [Token("\\[", 1)]
        StartArray,
        [Token("\\]", 1)]
        EndArray,

        
        [Token("\\,", 1)]
        Comma,

        Eof,
    }

    public static class TokensExtension {

        public static TokenAttribute GetTokenAttribute(this TokenType value) {
            var attributes = (TokenAttribute[])value
                .GetType()
                .GetField(value.ToString())
                ?.GetCustomAttributes(typeof(TokenAttribute), false);
            return attributes != null && attributes.Length > 0 ? attributes[0] : null;
        }

    }
}