namespace ProgLang.Error
{
    public class UnterminatedStringError: Exception
    {
        public UnterminatedStringError(string msg): base(msg) {}
    }
}