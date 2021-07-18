using System.Collections.Generic;
using RedConf.Abstractions;

namespace RedConf.Core.Statements {
    public class StatementList {
        public readonly List<IStatement> Statements;

        public StatementList() {
            Statements = new List<IStatement>();
        }

        public void AddStatement(IStatement statements) {
            Statements.Add(statements);
        }
    }
}