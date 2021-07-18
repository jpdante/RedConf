using System.Collections.Generic;
using System.Text;
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

        public override string ToString() {
            var builder = new StringBuilder();
            builder.Append("{ ");
            for (var i = 0; i < Statements.Count; i++) {
                if (i == Statements.Count - 1) {
                    builder.Append(Statements[i]);
                } else {
                    builder.Append(Statements[i]);
                    builder.Append(", ");
                }
            }
            builder.Append(" }");
            return builder.ToString();
        }
    }
}