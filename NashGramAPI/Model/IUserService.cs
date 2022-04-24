namespace NashGramAPI.Model;
public interface IUserService
{
    bool ValidateCredentials(string username, string password);
    long? GetIdAccount(string username, string password);
}