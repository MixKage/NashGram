using System.Data.SQLite;
using NashGramBack.ViewModel;

namespace NashGramBack;

public static class MainBack
{
    private static SQLiteConnection? DB = null;

    public static void Main()
    {
        
        long id = AccountDB.CreateAccount("login1","password");
        Console.WriteLine(AccountDB.GetPassword(id));
        Console.WriteLine(PersonDB.GetNameFromId(id));
        Log.SaveLogFile();
        AccountDB.DeleteAccountFromID(id);
    }
}
