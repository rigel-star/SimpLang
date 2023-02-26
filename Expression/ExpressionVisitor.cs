namespace ProgLang.Expression
{
    public class ExpressionVisitor
    {
        public object VisitLiteralExpression(LiteralExpression expr)
        {
            return expr.Value;
        }
    }
}