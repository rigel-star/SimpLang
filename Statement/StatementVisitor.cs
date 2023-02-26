using ProgLang.Expression;

namespace ProgLang.Statement
{
    public class StatementVisitor
    {
        public void VisitVariableDeclStatement(VariableDeclStatement stmt)
        {
            Console.WriteLine("'{0}' is assigned to '{1}'", stmt.Name, ((LiteralExpression) stmt.Expr).Value);
        }
    }
}