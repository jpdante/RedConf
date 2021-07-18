using RedConf.Abstractions;

namespace RedConf.Core.Statements {
    public class AssignStatement : IStatement {
        public string Ident;
        public IExpression Value;

        public AssignStatement(string ident, IExpression value) {
            Ident = ident;
            Value = value;
        }
    }
}