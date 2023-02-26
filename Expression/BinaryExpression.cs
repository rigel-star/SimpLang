namespace ProgLang.Expression
{
    public class BinaryExpression: Expression
    {
        public object A;
        public object B;
        public string Operator;

        public BinaryExpression(object a, object b, string op)
        {
            A = a;
            B = b;
            Operator = op;
        }
    }
}