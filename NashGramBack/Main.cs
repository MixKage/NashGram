using System.Data.SQLite;

namespace NashGramBack;

public static class MainBack
{
    private static SQLiteConnection? DB = null;

    public static void Main()
    {
        //ConnectToBD();
        //Log.AddLog("Initialization was successful", false);

        AccountDB.GetAccountFromID(1);
        AccountDB.CreateAccount("login12","password4");
    }

    //public static void ConnectToBD()
    //{
    //    DB = new SQLiteConnection("Data Source=database.db;Version=3;");
    //    DB.Open();
    //}

    //public static void DisconnectToBD()
    //{
    //    DB.Close();
    //}
}
