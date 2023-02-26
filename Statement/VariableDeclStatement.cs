using ProgLang.Expression;

namespace ProgLang.Statement
{
    public class VariableDeclStatement: Statement
    {
        public string Name;
        public Expression.Expression Expr;

        public VariableDeclStatement(string name, Expression.Expression expression)
        {
            Name = name;
            Expr = expression;
        }
    }
}