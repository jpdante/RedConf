using RedConf.Abstractions;

namespace RedConf.Core.Expressions {
    public class StringLiteralExpression : IExpression {
        public readonly string Value;

        public StringLiteralExpression(string value) {
            Value = value;
        }

        public override string ToString() {
            return Value;
        }
    }
}