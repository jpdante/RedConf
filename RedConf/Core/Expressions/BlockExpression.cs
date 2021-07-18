using RedConf.Abstractions;
using RedConf.Core.Statements;

namespace RedConf.Core.Expressions {
    public class BlockExpression : IExpression {

        public readonly StatementList StatementList;

        public BlockExpression() {
            StatementList = new StatementList();
        }

        public void AddStatement(IStatement statement) => StatementList.AddStatement(statement);

        public override string ToString() {
            return StatementList.ToString();
        }
    }
}