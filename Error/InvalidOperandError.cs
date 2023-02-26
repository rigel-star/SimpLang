namespace ProgLang.Error
{
    public class InvalidOperandException: Exception
    {
        public InvalidOperandException(string msg): base(msg) {}
    }
}