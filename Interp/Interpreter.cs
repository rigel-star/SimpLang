using ProgLang.Error;
using ProgLang.Expression;
using ProgLang.Statement;

namespace ProgLang.Interp
{
    public class Interpreter
    {
        public void Interpret(List<object> expressions)
        {
            StatementVisitor statementVisitor = new StatementVisitor();
            foreach(object e in expressions)
            {
                if(e is VariableDeclStatement)
                    statementVisitor.VisitVariableDeclStatement((VariableDeclStatement) e);
            }
        }

        private object ?VisitBinaryExpression(BinaryExpression be)
        {
            if(be.Operator.Equals("+"))
            {
                if(be.A is int && be.A is int) return (int) be.A + (int) be.B;
                else if(be.A is string && be.B is string) return (string) be.A + (string) be.B;
                else if(be.A is string && be.B is int) return (string) be.A + Convert.ToString(be.B);
                else if(be.A is int && be.B is string) return Convert.ToString(be.A) + (string) be.B;
            } 
            throw new InvalidOperationException(String.Format("Operator {0} can't applied for types '{1}' and '{2}'.", be.Operator, be.A, be.B));
        }
    }
}