using ProgLang.Expression;
using ProgLang.Statement;

namespace ProgLang.Parser
{
    public class Parser
    {
        private List<Token> _Tokens;

        private int _Current = 0;

        public Parser(List<Token> tokens)
        {
            _Tokens = tokens;
        }

        public List<object> StartParsing()
        {
            List<object> items = new();
            while(!_IsAtEnd()) 
            {
                items.Add(ParseStatement());
            }
            return items;
        }

        private Statement.Statement ?ParseStatement()
        {
            if(_TokenMatch(TokenType.VAR))
            {
                _AdvancePointer();
                return (Statement.Statement) _ParseVariableDeclStatement();
            }
            return null;
        }

        private Statement.Statement _ParseVariableDeclStatement()
        {
            Token token = _Consume(TokenType.IDENTIFIER, String.Format("Invalid token '{0}'", _Peek().Tokentype));
            Expression.Expression expression = null;
            if(_TokenMatch(TokenType.EQUAL))
            {
                _AdvancePointer();
                expression = _ParsePrimary();
            }
            
            _Consume(TokenType.SEMICOLON, String.Format("Expected ';' but got '{0}'", _Peek().Tokentype));
            return new VariableDeclStatement(token.Lexeme, expression);
        }

        private Expression.Expression _ParsePrimary()
        {
            if(_TokenMatch(TokenType.INT_NUMBER, TokenType.FLOAT_NUMBER, TokenType.STRING))
            {
                Expression.Expression expr = new LiteralExpression(_Peek().Literal);
                _AdvancePointer();
                return expr;
            }
            _AdvancePointer();
            return new LiteralExpression(null);
        }

        private bool _TokenMatch(params TokenType[] types)
        {
            foreach(TokenType type in types)
                if(type == _Peek().Tokentype) return true;
            return false;
        }

        private Token _Peek()
        {
            return _Tokens.ElementAt(_Current);
        }

        private Token _Consume(TokenType type, string msg)
        {
            if(_Peek().Tokentype == type)
                return _AdvancePointer();
            throw new Exception(msg);
        }

        private Token _AdvancePointer()
        {
            return _Tokens.ElementAt(_Current++);
        }

        private bool _IsAtEnd()
        {
            return _Tokens.ElementAt(_Current).Tokentype == TokenType.EOF;
        }
    }
}