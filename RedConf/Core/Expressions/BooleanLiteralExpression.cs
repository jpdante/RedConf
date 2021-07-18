using RedConf.Abstractions;

namespace RedConf.Core.Expressions {
    public class BooleanLiteralExpression : IExpression {
        
        public bool Value;

        public BooleanLiteralExpression(bool value) {
            Value = value;
        }

    }
}