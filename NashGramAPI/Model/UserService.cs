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
}
