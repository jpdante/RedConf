using System.Collections.Generic;
using System.Text;
using RedConf.Abstractions;
using RedConf.Core.Statements;

namespace RedConf.Core.Expressions {
    public class ArrayExpression : IExpression {

        public readonly List<IExpression> Expressions;

        public ArrayExpression() {
            Expressions = new List<IExpression>();
        }

        public void AddExpression(IExpression expression) {
            Expressions.Add(expression);
        }

        public override string ToString() {
            var builder = new StringBuilder();
            builder.Append("[ ");
            for (var i = 0; i < Expressions.Count; i++) {
                if (i == Expressions.Count - 1) {
                    builder.Append(Expressions[i]);
                } else {
                    builder.Append(Expressions[i]);
                    builder.Append(", ");
                }
            }
            builder.Append(" ]");
            return builder.ToString();
        }
    }
}