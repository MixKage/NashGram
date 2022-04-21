using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashGramAPI.Model;

namespace NashGramAPI.Repository;

/// <summary>
/// Класс для работы с таблицой account
/// </summary>
public static class AccountRepository
{
    private static string pathDB = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString().Replace("NashGramAPI.dll", "") + @"\sqlite\databaseNashGram.db";

    public static void UpdateLogin(long id, string login)
    {
        try
        {
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"UPDATE Account
                        SET login = '{login}',                        
                        WHERE id_account = '{id}'", connection))
                {
                    cmd.ExecuteNonQuery();
                    Log.AddLog($"UpdateLogin id: {id}, newLogin: {login}", false);
                }
            }
        }
        catch (Exception ex)
        {
            Log.AddLog($"Update Failed login id: {id}| " + ex.Message, true);
        }
    }

    public static void UpdatePassword(long id, string password)
    {
        try
        {
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"UPDATE Account
                        SET password = '{password}',                        
                        WHERE id_account = '{id}'", connection))
                {
                    cmd.ExecuteNonQuery();
                    Log.AddLog($"UpdateLogin id: {id}, newLogin: {password}", false);
                }
            }
        }
        catch (Exception ex)
        {
            Log.AddLog($"Update Failed password id: {id}| " + ex.Message, true);
        }
    }

    /// <summary>
    /// Возвращает логин account по id
    /// </summary>
    public static string? GetLogin(long id)
    {
        var account = GetAccountFromID(id);
        if (account != null)
            return account.Login;
        else
            return null;
    }
    /// <summary>
    /// Возвращает пароль Account по id
    /// </summary>
    public static string? GetPassword(long id)
    {
        var account = GetAccountFromID(id);
        if (account != null)
            return account.Password;
        else
            return null;
    }

    /// <summary>
    /// Создаёт аккаунт с профилем и возвращает его id
    /// </summary>        
    public static long? CreateAccount(ModelClass.AccountInput input)
    {
        string login = input.login;
        string password = input.password;
        long? newId = null;
        try
        {
            //Создание аккаунта
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"INSERT INTO Account (
                        login,
                        password
                    )
                    VALUES (                        
                        '{login}',
                        '{password}'
                    )
                    RETURNING id_account", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newId = reader.GetInt64(0);
                        }
                    }
                }
            }

            ////Получение самого большого id
            //using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            //{
            //    connection.Open();
            //    using (var cmd = new SQLiteCommand(@$"SELECT *
            //            FROM Account
            //            ORDER BY id_account DESC
            //            LIMIT 1", connection))
            //    {
            //        using (var reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                newId = reader.GetInt64(0);
            //            }
            //        }
            //    }
            //}

            if (newId == 0) { return null; }
            Log.AddLog($"Create account id: {newId}", false);
            //Создание Person
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"INSERT INTO Person (
                       id_account,
                       email,
                       name,
                       status,
                       country,
                       age,
                       number
                   )
                   VALUES (
                       '{newId}','NULL','NULL','NULL','NULL','NULL','NULL');", connection))
                {
                    cmd.ExecuteNonQuery();
                    Log.AddLog($"Create Person id: {newId}", false);
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("UNIQUE constraint failed: Account.login"))
            {
                Log.AddLog(ex.Message, true);
                return null;
            }
        }
        return newId;
    }

    /// <summary>
    /// Удаляет аккаунт и профиль по id
    /// </summary>        
    public static void DeleteAccountFromID(long id)
    {
        try
        {
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(@$"PRAGMA foreign_keys = 1;
                        DELETE FROM Account
                        WHERE id_account = '{id}'; ", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
        }
    }

    /// <summary>
    /// Получает Account по id
    /// </summary>        
    public static Account? GetAccountFromID(long id)
    {
        try
        {
            Account? account = null;
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($"SELECT * FROM Account WHERE id_account = {id};", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        account = new Account();

                        while (reader.Read())
                        {
                            account.Id = reader.GetInt64(0);
                            account.Login = reader.GetString(1);
                            account.Password = reader.GetString(2);
                        }
                        Log.AddLog($"GetAccountFromId: id: {account.Id}, login: {account.Login}, password: {account.Password}", false);
                    }
                }
            }
            if (id == 0) return null;
            else return account;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
            return null;
        }
    }


}
