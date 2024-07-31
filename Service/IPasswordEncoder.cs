namespace Settimana_3_Manuel.Service
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
        bool IsSame(string plainText, string codedText);

    }
}
