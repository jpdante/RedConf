using RedConf.Abstractions;

namespace RedConf.Core.Expressions {
    public class BooleanLiteralExpression : IExpression {
        
        public readonly bool Value;

        public BooleanLiteralExpression(bool value) {
            Value = value;
        }

        public override string ToString() {
            return Value.ToString();
        }
    }
}