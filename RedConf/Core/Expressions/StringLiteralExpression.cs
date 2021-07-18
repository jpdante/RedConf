using RedConf.Abstractions;

namespace RedConf.Core.Expressions {
    public class StringLiteralExpression : IExpression {
        public string Value;

        public StringLiteralExpression(string value) {
            Value = value;
        }
    }
}