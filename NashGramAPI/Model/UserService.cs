using NashGramAPI.Repository;

namespace NashGramAPI.Model;

public class UserService : IUserService
{
    public bool ValidateCredentials(string username, string password)
    {
        List<Account>? accounts = AccountRepository.GetAllAccount();
        if (accounts is null) return false;
        return accounts.Exists(x => x.Login.Equals(username) && x.Password.Equals(password));
    }

    public long? GetIdAccount(string username, string password)
    {
        List<Account>? accounts = AccountRepository.GetAllAccount();
        if (accounts is null) return null;
        int index = accounts.FindIndex(x => x.Login.Equals(username) && x.Password.Equals(password));
        return accounts[index].Id;
    }
}
