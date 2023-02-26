using ProgLang.Error;

namespace ProgLang.Lexer
{
    public class Lexer
    {
        private readonly string _Source;
        private readonly int _SourceLength;
        private int _Current = 0;
        private int _Start = 0;
        private int _Line = 1;

        List<Token> tokens = new();

        private Dictionary<string, TokenType> keywords = new()
        {
            {"for", TokenType.FOR},
            {"while", TokenType.WHILE},
            {"do", TokenType.DO},
            {"void", TokenType.VOID},
            {"break", TokenType.BREAK},
            {"continue", TokenType.CONTINUE},
            {"goto", TokenType.GOTO},
            {"var", TokenType.VAR},
            {"null", TokenType.NULL},
        };

        public Lexer(string source)
        {
            _Source = source;
            _SourceLength = _Source.Length;
        }

        public List<Token> ScanTokens()
        {
            while(!_IsAtEnd())
            {
                _Start = _Current;
                _ScanToken();
            }

            _AddToken(TokenType.EOF);
            return tokens;
        }
    
        private void _ScanToken()
        {
            char c = _AdvancePointer();

            if(c == '.')
            {
                if(!_IsAtEnd() && Char.IsAsciiDigit(_PeekNext()))
                {
                    _AdvancePointer();
                    while(Char.IsAsciiDigit(_Peek()) && !_IsAtEnd())
                        _AdvancePointer();
                    string num = _Source.Substring(_Start, _Current - _Start);
                    _AddToken(TokenType.FLOAT_NUMBER, num);
                }
                else
                    _AddToken(TokenType.DOT);
            }
            else if(c == '*')
                _AddToken(TokenType.STAR);
            else if(c == ';')
                _AddToken(TokenType.SEMICOLON);
            else if(c == ',')
                _AddToken(TokenType.COMMA);
            else if(c == '/')
                _AddToken(TokenType.SLASH);
            else if(c == '+')
                _AddToken(TokenType.PLUS);
            else if(c == '-')
                _AddToken(TokenType.MINUS);
            else if(c == '!')
                _AddToken(TokenType.NEGATE);
            else if(c == '=')
                _AddToken(TokenType.EQUAL);
            else if(c == '"')
            {
                char peek;
                while((peek = _Peek()) != '"' && !_IsAtEnd())
                {
                    if(peek == '\n')
                        _Line++;
                    _AdvancePointer();
                }

                if(_IsAtEnd()) throw new UnterminatedStringError("Lexer Error: Unterminated string!");
                string str = _Source.Substring(_Start + 1, _Current - _Start - 1);
                _AddToken(TokenType.STRING, literal: str);
                _AdvancePointer();
            }
            else if(Char.IsAsciiDigit(c))
            {
                while(Char.IsAsciiDigit(_Peek()) && !_IsAtEnd())
                    _AdvancePointer();
                if(_Peek() == '.' && Char.IsAsciiDigit(_PeekNext()))
                {
                    _AdvancePointer();
                    while(Char.IsAsciiDigit(_Peek()) && !_IsAtEnd())
                        _AdvancePointer();
                    string floatVal = _Source.Substring(_Start, _Current - _Start);
                    _AddToken(TokenType.FLOAT_NUMBER, literal: floatVal);
                }
                else{
                    string intVal = _Source.Substring(_Start, _Current - _Start);
                    _AddToken(TokenType.INT_NUMBER, literal: intVal);
                }
            }
            else if(Char.IsAsciiLetter(c) || c == '_')
            {
                while(Char.IsAsciiLetterOrDigit(_Peek()) || _Peek() == '_')
                    _AdvancePointer();
                string text = _Source.Substring(_Start, _Current - _Start);
                if(keywords.Keys.Contains(text))
                {
                    TokenType typ = keywords[text];
                    _AddToken(typ);
                }
                else 
                    _AddToken(TokenType.IDENTIFIER, lexeme: text);
            }
            else if(c == '\'')
            {
                while(_Peek() != '\'' && !_IsAtEnd())
                    _AdvancePointer();
                string chr = _Source.Substring(_Start + 1, _Current - _Start - 1);
                if(chr.Length > 1)
                    throw new Exception("Character type can't contain more than one character.");
                else if(chr.Length == 0)
                    chr = "\0";
                _AddToken(TokenType.CHARACTER, literal: chr);
                _AdvancePointer();
            }
            else if(c == '\n') _Line++;
        }

        private char _Peek()
        {
            return _IsAtEnd() ? '\0' : _Source[_Current];
        }

        private char _PeekNext()
        {
            int index = _Current + 1;
            return index < _SourceLength ? _Source[index] : '\0';
        }

        private void _AddToken(TokenType type, string lexeme = "", object ?literal = null)
        {
            Token token = new(type, lexeme, literal, _Line);
            tokens.Add(token);
        }

        private char _AdvancePointer()
        {
            return _Current < _SourceLength ? _Source[_Current++] : '\0';
        }

        private bool _IsAtEnd()
        {
            return _Current >= _SourceLength;
        }
    }
}


/* MAKING SYNTAX HIGHLIGHTER */