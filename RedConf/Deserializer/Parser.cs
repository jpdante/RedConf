using System.Collections.Generic;
using RedConf.Abstractions;
using RedConf.Core;
using RedConf.Core.Expressions;
using RedConf.Core.Statements;

namespace RedConf.Deserializer {
    public class Parser {

        private readonly TokenList _tokenList;
        private bool _running;

        public Parser(TokenList tokenList) {
            _tokenList = tokenList;
        }

        public StatementList GetStatementList() {
            Token token = null;
            var currentBlock = new StatementList();
            _running = true;
            while (_running) {
                try {
                    token = _tokenList.GetToken();
                } catch {
                    // ignored
                }
                if (token == null) continue;
                if (token.TokenType == TokenType.Ident && _tokenList.Peek().TokenType == TokenType.Assign) {
                    _tokenList.Position--;
                    currentBlock.AddStatement(ParseAssign());
                } else if (token.TokenType == TokenType.Eof) {
                    _running = false;
                }
            }

            return currentBlock;
        }

        public IStatement ParseAssign() {
            var identToken = _tokenList.GetToken();
            string identName = identToken.Value;
            _tokenList.Position++;
            var valueExpression = ParseExpression();
            var assignStatement = new AssignStatement(identName, valueExpression);
            return assignStatement;
        }

        public IExpression ParseExpression() {
            IExpression returnExpression = null;
            var token = _tokenList.GetToken();

            if (token.TokenType == TokenType.StringLiteral) {
                returnExpression = new StringLiteralExpression(token.Value);
            } else if (token.TokenType == TokenType.IntLiteral) {
                returnExpression = new IntLiteralExpression(int.Parse(token.Value));
            } else if (token.TokenType == TokenType.BooleanLiteral) {
                returnExpression = new BooleanLiteralExpression(bool.Parse(token.Value));
            } else if (token.TokenType == TokenType.StartBlock) {
                returnExpression = ParseBlockExpression();
            }

            return returnExpression;
        }

        public IExpression ParseBlockExpression() {
            var blockExpression = new BlockExpression();
            Token token = null;
            while (_running) {
                try {
                    token = _tokenList.GetToken();
                } catch {
                    // ignored
                }
                if (token == null) continue;
                if (token.TokenType == TokenType.Ident && _tokenList.Peek().TokenType == TokenType.Assign) {
                    _tokenList.Position--;
                    blockExpression.AddStatement(ParseAssign());
                } else if (token.TokenType == TokenType.EndBlock) {
                    _running = false;
                }
            }
            return blockExpression;
        }
    }
}