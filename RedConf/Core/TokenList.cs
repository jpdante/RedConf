using System.Collections.Generic;

namespace RedConf.Core {
    public class TokenList {
        public readonly List<Token> Tokens;
        public int Position = 0;

        public TokenList(List<Token> tokens) {
            Tokens = tokens;
        }

        public Token GetToken() {
            var token = Tokens[Position];
            Position++;
            return token;
        }

        public Token Peek() {
            return Tokens[Position];
        }
    }
}