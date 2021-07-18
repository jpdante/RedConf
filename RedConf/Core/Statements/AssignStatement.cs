using RedConf.Abstractions;

namespace RedConf.Core.Statements {
    public class AssignStatement : IStatement {
        public readonly string Ident;
        public readonly IExpression Value;

        public AssignStatement(string ident, IExpression value) {
            Ident = ident;
            Value = value;
        }

        public override string ToString() {
            return $"{Ident} = {Value}";
        }
    }
}