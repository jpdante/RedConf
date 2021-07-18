using RedConf.Abstractions;

namespace RedConf.Core.Statements {
    public class ObjectAssignStatement : IStatement {
        public string Ident;
        public IStatement Value;

        public ObjectAssignStatement(string ident, IStatement value) {
            Ident = ident;
            Value = value;
        }
    }
}