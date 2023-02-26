using ProgLang;

public class Program 
{
    public static void Main(string[] args)
    {
        ProgLang.Lexer.Lexer lexer = new ProgLang.Lexer.Lexer("var a = 45;");
        ProgLang.Parser.Parser parser = new ProgLang.Parser.Parser(lexer.ScanTokens());
        List<object> objs = parser.StartParsing();
        ProgLang.Interp.Interpreter interpreter = new ProgLang.Interp.Interpreter();
        interpreter.Interpret(objs);
    }
}