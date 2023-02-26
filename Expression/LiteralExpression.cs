namespace ProgLang.Expression
{
    public class LiteralExpression: Expression
    {
        public object ?Value;

        public LiteralExpression(object ?o)
        {
            Value = o;
        }
    }
}