using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RedConf.Abstractions;

namespace RedConf.Core {
    public class TokenAttribute : Attribute {

        public readonly string RegexString;
        public readonly Regex Regex;
        public readonly int Precedence;

        public TokenAttribute(string regex, int precedence) {
            RegexString = regex;
            Regex = new Regex(RegexString);
            Precedence = precedence;
        }

        public IEnumerable<Token> FindMatches(TokenType tokenType, string data) {
            var matches = Regex.Matches(data);
            for (var i = 0; i < matches.Count; i++) {
                yield return new Token {
                    StartIndex = matches[i].Index,
                    EndIndex = matches[i].Index + matches[i].Length,
                    Length = matches[i].Length,
                    TokenType = tokenType,
                    Value = matches[i].Value,
                    Precedence = Precedence
                };
            }
        }

    }
}