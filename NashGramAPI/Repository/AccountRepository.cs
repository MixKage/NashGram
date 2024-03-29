﻿using System;
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
    private static string pathDB = ModelClass.pathDB;

    public static bool UpdateLogin(ModelClass.UpdateInput input)
    {
        long id = input.id;
        string login = sqlite.Encryption.Encr(input.text);
        try
        {
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"UPDATE Account
                        SET login = '{login}'                        
                        WHERE id_account = '{id}'", connection))
                {
                    cmd.ExecuteNonQuery();
                    Log.AddLog($"UpdateLogin id: {id}, newLogin: {login}", false);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Update Failed login id: {id} | " + ex.Message, true);
            return false;
        }
    }

    public static bool UpdatePassword(ModelClass.UpdateInput input)
    {
        long id = input.id;
        string password = sqlite.Encryption.Encr(input.text);
        try
        {
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"UPDATE Account
                        SET password = '{password}'                        
                        WHERE id_account = '{id}'", connection))
                {
                    cmd.ExecuteNonQuery();
                    Log.AddLog($"UpdatePassword id: {id}, newPassword: {password}", false);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Update Failed password id: {id} | " + ex.Message, true);
            return false;
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
    public static long? CreateAccount(ModelClass.AccountCreateInput input)
    {
        string login = sqlite.Encryption.Encr(input.login);
        string password = sqlite.Encryption.Encr(input.password);
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

            if (newId == 0) { return null; }
            Log.AddLog($"Create account id: {newId}, login {login}, password {password}", false);
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
                       number,
                       avatar
                   )
                   VALUES (
                       '{newId}','NULL','name{newId}','NULL','0','0','NULL','NULL');", connection))
                {
                    cmd.ExecuteNonQuery();
                    Log.AddLog($"Create Person id: {newId}", false);
                }
            }
        }
        catch (Exception ex)
        {
            Log.AddLog(ex.Message, true);
            return null;
        }
        return newId;
    }

    /// <summary>
    /// Удаляет аккаунт и профиль по id
    /// </summary>        
    public static bool DeleteAccountFromID(long id)
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
                    Log.AddLog($"Account delete id: {id}", true);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
            return false;
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
                        while (reader.Read())
                        {
                            account = new Account();
                            account.Id = reader.GetInt64(0);
                            account.Login = reader.GetString(1);
                            account.Password = reader.GetString(2);
                        }
                    }
                }
            }
            if (account is null) return null;
            else return account;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
            return null;
        }
    }

    /// <summary>
    /// Получает Account по id
    /// </summary>        
    public static List<Account>? GetAllAccount()
    {
        try
        {
            List<Account> accountList = new List<Account>();
            Account? account = null;
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($"SELECT * FROM Account;", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new Account();
                            account.Id = reader.GetInt64(0);
                            account.Login = reader.GetString(1);
                            account.Password = reader.GetString(2);
                            accountList.Add(account);
                        }
                    }
                }
            }
            if (accountList.Count == 0) return null;
            else return accountList;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Accounts not found | " + ex.Message, true);
            return null;
        }
    }
}
