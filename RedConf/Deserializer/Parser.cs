using RedConf.Core;

namespace RedConf.Deserializer {
    public class Parser {

        private readonly TokenList _tokens;

        public Parser(TokenList tokenList) {
            _tokens = tokenList;
        }

        /*
        private BlockStatement _currentBlock;
        private Stack<BlockStatement> _blockStack;
        private List<IStatement> _tree;
        private bool _running;
        private ulong _functionId;

        public delegate void AddImportHandler(object sender, string import);
        public event AddImportHandler OnAddImportHandler;

        public Task<List<IStatement>> Process() {
            Token token = null;
            _functionId = 0;
            _currentBlock = null;
            _blockStack = new Stack<BlockStatement>();
            _tree = new List<IStatement>();
            _running = true;
            while (_running) {
                try {
                    token = _tokens.GetToken();
                } catch {
                    // ignored
                }
                if (token == null) continue;

                if (token.TokenType == TokenType.Import) {
                    OnAddImportHandler?.Invoke(this, ParseImport());
                } else if (token.TokenType == TokenType.Function) {
                    var functionStatement = ParseFunction();
                    if (_currentBlock == null) {
                        _currentBlock = functionStatement;
                    } else {
                        _currentBlock.AddStatement(new ReturnStatement(null));
                        _tree.Add(_currentBlock);
                        _currentBlock = functionStatement;
                    }
                } else if (token.TokenType == TokenType.Ident) {
                    if (_tokens.Peek().TokenType == TokenType.Equal) {
                        _tokens.Position--;
                        var assignStatement = ParseAssign();
                        _currentBlock.AddStatement(assignStatement);
                    } else if (_tokens.Peek().TokenType == TokenType.StartParams) {
                        _tokens.Position--;
                        var callStatement = ParseCall();
                        _currentBlock.AddStatement(callStatement);
                    }
                } else if (token.TokenType == TokenType.Return) {
                    var returnStatement = ParseReturn();
                    _currentBlock.AddStatement(returnStatement);
                } else if (token.TokenType == TokenType.EndParams) {
                    if (_currentBlock is FunctionStatement) {
                        _currentBlock.AddStatement(new ReturnStatement(null));
                        _tree.Add(_currentBlock);
                        _currentBlock = null;
                    }
                    /* else if (_currentBlock is IfBlock || currentBlock is ElseIfBlock || currentBlock is ElseBlock) {
                        _currentBlock.AddStmt(new EndIf());
                        Block block = currentBlock;

                        if (blockstack.Count > 0) {
                            currentBlock = blockstack.Pop();
                            currentBlock.AddStmt(block);
                        }
                    } else if (currentBlock is RepeatBlock) {
                        Block block = currentBlock;

                        if (blockstack.Count > 0) {
                            currentBlock = blockstack.Pop();
                            currentBlock.AddStmt(block);
                        }
                    }#1#
                } else if (token.TokenType == TokenType.Eof) {
                    _tree.Add(_currentBlock);
                    _running = false;
                }
            }
            return Task.FromResult(_tree);
        }

        private string ParseImport() {
            var token = _tokens.GetToken();
            return token.TokenType == TokenType.Ident ? token.Value : "";
        }

        private FunctionStatement ParseFunction() {
            var ident = "";
            var vars = new List<string>();

            if (_tokens.Peek().TokenType == TokenType.Ident) {
                ident = _tokens.GetToken().Value;
            }

            if (_tokens.Peek().TokenType == TokenType.StartParams) {
                _tokens.Position++;
            }

            if (_tokens.Peek().TokenType == TokenType.EndParams) {
                _tokens.Position++;
            } else {
                vars = ParseFunctionArgs();
            }

            if (_tokens.Peek().TokenType == TokenType.StartBlock) {
                _tokens.Position++;
            }

            _functionId++;
            return new FunctionStatement(_functionId, ident, vars);
        }

        private List<string> ParseFunctionArgs() {
            var ret = new List<string>();

            while (true) {
                var token = _tokens.GetToken();
                if (token.TokenType == TokenType.Ident) {
                    ret.Add(token.Value);
                }

                if (_tokens.Peek().TokenType == TokenType.Comma) {
                    _tokens.Position++;
                } else if (_tokens.Peek().TokenType == TokenType.EndParams) {
                    _tokens.Position++;
                    break;
                }
            }
            return ret;
        }

        private AssignStatement ParseAssign() {
            var token = _tokens.GetToken();
            string ident = token.Value;
            _tokens.Position++;
            IExpression value = ParseExpression();
            var assignStatement = new AssignStatement(ident, value);
            return assignStatement;
        }

        private CallStatement ParseCall() {
            var ident = "";
            var token = _tokens.GetToken();
            var args = new List<IExpression>();
            if (token.TokenType == TokenType.Ident) ident = token.Value;
            if (_tokens.Peek().TokenType == TokenType.StartParams) _tokens.Position++;
            if (_tokens.Peek().TokenType == TokenType.EndParams) {
                _tokens.Position++;
            } else {
                args = ParseCallArgs();
            }
            return new CallStatement(ident, args);
        }

        private List<IExpression> ParseCallArgs() {
            var arguments = new List<IExpression>();
            while (true) {
                arguments.Add(ParseExpression());
                if (_tokens.Peek().TokenType == TokenType.Comma) {
                    _tokens.Position++;
                } else if (_tokens.Peek().TokenType == TokenType.EndParams) {
                    _tokens.Position++;
                    break;
                }
            }
            return arguments;
        }

        private IExpression ParseExpression() {
            IExpression returnExpression = null;
            var token = _tokens.GetToken();

            if (_tokens.Peek().TokenType == TokenType.StartParams) {
                var ident = "";
                if (token.TokenType == TokenType.Ident) ident = token.Value;
                _tokens.Position++;
                returnExpression = _tokens.Peek().TokenType == TokenType.EndParams ? new CallExpression(ident, new List<IExpression>()) : new CallExpression(ident, ParseCallArgs());
            } else if (token.TokenType == TokenType.IntLiteral) {
                returnExpression = new IntLiteralExpression(Convert.ToInt32(token.Value));
            } else if (token.TokenType == TokenType.StringLiteral) {
                returnExpression = new StringLiteralExpression(token.Value);
            } else if (token.TokenType == TokenType.Ident) {
                returnExpression = new IdentExpression(token.Value);
            } else if (token.TokenType == TokenType.StartParams) {
                var expression = ParseExpression();
                if (_tokens.Peek().TokenType == TokenType.EndParams) {
                    _tokens.Position++;
                }

                var leftExpression = new ParameterExpression(expression);

                if (_tokens.Peek().TokenType == TokenType.Add) {
                    _tokens.Position++;
                    var rightExpression = ParseExpression();
                    returnExpression = new MathExpression(leftExpression, OperatorType.Add, rightExpression);
                } else if (_tokens.Peek().TokenType == TokenType.Subtract) {
                    _tokens.Position++;
                    var rightExpression = ParseExpression();
                    returnExpression = new MathExpression(leftExpression, OperatorType.Subtract, rightExpression);
                } else if (_tokens.Peek().TokenType == TokenType.Multiply) {
                    _tokens.Position++;
                    var rightExpression = ParseExpression();
                    returnExpression = new MathExpression(leftExpression, OperatorType.Multiply, rightExpression);
                } else if (_tokens.Peek().TokenType == TokenType.Divide) {
                    _tokens.Position++;
                    var rightExpression = ParseExpression();
                    returnExpression = new MathExpression(leftExpression, OperatorType.Divide, rightExpression);
                } else {
                    returnExpression = leftExpression;
                }
            }

            if (_tokens.Peek().TokenType == TokenType.Add || _tokens.Peek().TokenType == TokenType.Subtract || _tokens.Peek().TokenType == TokenType.Multiply || _tokens.Peek().TokenType == TokenType.Divide) {
                var leftExpression = returnExpression;
                var operatorType = OperatorType.Add;
                if (_tokens.Peek().TokenType == TokenType.Add) {
                    operatorType = OperatorType.Add;
                } else if (_tokens.Peek().TokenType == TokenType.Subtract) {
                    operatorType = OperatorType.Subtract;
                } else if (_tokens.Peek().TokenType == TokenType.Multiply) {
                    operatorType = OperatorType.Multiply;
                } else if (_tokens.Peek().TokenType == TokenType.Divide) {
                    operatorType = OperatorType.Divide;
                }
                _tokens.Position++;
                var rightExpression = ParseExpression();
                returnExpression = new MathExpression(leftExpression, operatorType, rightExpression);
            }
            return returnExpression;
        }

        private ReturnStatement ParseReturn() {
            return new ReturnStatement(ParseExpression());
        }*/
    }
}