using System;
using System.Collections.Generic;
using System.IO;
using RedConf.Abstractions;
using RedConf.Core;
using RedConf.Deserializer;

namespace RedConf {
    public static partial class RedConf {

        public static T Deserialize<T>(string input) {
            return default(T);
        }

        public static T Deserialize<T>(Stream stream) {
            using var streamReader = new StreamReader(stream);
            string input = streamReader.ReadToEnd();
            return default(T);
        }

        public static void GetTokens(string input) {
            var lexer = new Lexer(input);
            var tokens = new List<Token>();
            while (true) {
                var token = lexer.GetToken();

                if (token == null) break;
                if (token.TokenType != TokenType.Space && token.TokenType != TokenType.NewLine && token.TokenType != TokenType.Unknown) {
                    tokens.Add(token);
                }
            }

            tokens.Add(new Token(TokenType.Eof));
            var tokenList = new TokenList(tokens);
            var parser = new Parser(tokenList);
        }
    }
}