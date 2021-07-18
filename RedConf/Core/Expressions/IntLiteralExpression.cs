using RedConf.Abstractions;

namespace RedConf.Core.Expressions {
    public class IntLiteralExpression : IExpression {
        
        public int Value;

        public IntLiteralExpression(int value) {
            Value = value;
        }

    }
}