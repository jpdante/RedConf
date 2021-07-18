using RedConf.Abstractions;

namespace RedConf.Core {
    public class Token {

        public TokenType TokenType { get; set; }
        public string Value { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Length { get; set; }
        public int Precedence { get; set; }

        public Token() { }

        public Token(TokenType tokenType) {
            TokenType = tokenType;
        }

    }
}