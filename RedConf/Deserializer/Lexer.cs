using System;
using System.Collections.Generic;
using RedConf.Abstractions;
using RedConf.Core;

namespace RedConf.Deserializer {
    public class Lexer {

        private readonly Dictionary<TokenType, IEnumerable<Token>> _matchTokens;
        private int _index;
        private string _inputString;

        public Lexer(string inputString) {
            _inputString = inputString;
            _matchTokens = new Dictionary<TokenType, IEnumerable<Token>>();
            PrepareRegex();
        }

        public void PrepareRegex() {
            _matchTokens.Clear();
            foreach (TokenType tokenType in Enum.GetValues(typeof(TokenType))) {
                var tokenAttribute = tokenType.GetTokenAttribute();
                if(tokenAttribute == null) continue;
                _matchTokens.Add(tokenType, tokenAttribute.FindMatches(tokenType, _inputString));
            }
        }

        public void Reset() {
            _index = 0;
            _inputString = string.Empty;
            _matchTokens.Clear();
        }

        public Token GetToken() {
            if (_index >= _inputString.Length) return null;

            foreach (KeyValuePair<TokenType, IEnumerable<Token>> pair in _matchTokens) {
                foreach (var tokenMatch in pair.Value) {
                    if (tokenMatch.StartIndex == _index) {
                        _index += tokenMatch.Length;
                        return tokenMatch;
                    }
                    if (tokenMatch.StartIndex > _index) break;
                }
            }
            _index++;
            return new Token {
                TokenType = TokenType.Unknown,
            };
        }
    }
}
