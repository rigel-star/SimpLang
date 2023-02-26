namespace ProgLang
{
    public class Token
    {
        public TokenType Tokentype;
        public string Lexeme;
        public object ?Literal;
        public int Line;

        public Token(TokenType type, string lexeme, object ?literal, int line)
        {
            Tokentype = type;
            Lexeme = lexeme;
            Literal = literal;
            Line = line;
        }
    }
}