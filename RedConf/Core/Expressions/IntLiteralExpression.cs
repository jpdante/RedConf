using RedConf.Abstractions;

namespace RedConf.Core.Expressions {
    public class IntLiteralExpression : IExpression {
        
        public readonly int Value;

        public IntLiteralExpression(int value) {
            Value = value;
        }

        public override string ToString() {
            return Value.ToString();
        }

    }
}