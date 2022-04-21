using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashGramBack.Model;

namespace NashGramBack.ViewModel
{
    /// <summary>
    /// Класс для работы с таблицой Accaunt
    /// </summary>
    public static class AccountDB
    {
        private static string pathDB = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString().Replace("NashGramBack.dll", "") + @"\sqlite\databaseNashGram.db";
        /// <summary>
        /// Возвращает логин Accaunt по id
        /// </summary>
        public static string? GetLogin(long id)
        {
            var accaunt = GetAccountFromID(id);
            if (accaunt != null)
                return accaunt.login;
            else
                return null;
        }
        /// <summary>
        /// Возвращает пароль Account по id
        /// </summary>
        public static string? GetPassword(long id)
        {
            var accaunt = GetAccountFromID(id);
            if (accaunt != null)
                return accaunt.password;
            else
                return null;
        }

        /// <summary>
        /// Создаёт аккаунт с профилем и возвращает его id (-1: login уже существует)
        /// </summary>        
        public static long CreateAccount(string login, string password)
        {
            long newId = 0;
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
                    );", connection))
                    {
                        cmd.ExecuteNonQuery(); //Создание аккаунта
                    }
                }

                //Получение самого большого id
                using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand(@$"SELECT *
                        FROM Account
                        ORDER BY id_account DESC
                        LIMIT 1", connection))
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
                if (newId == 0)
                {
                    return -1;
                }
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
                        cmd.ExecuteNonQuery(); //Создание аккаунта
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE constraint failed: Account.login"))
                    return -1;
                Log.AddLog(ex.Message, true);
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
                    using (var cmd = new SQLiteCommand(@$"DELETE FROM Account
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
                                account.id = reader.GetInt64(0);
                                account.login = reader.GetString(1);
                                account.password = reader.GetString(2);
                            }
                        }
                    }
                }
                return account;
            }
            catch (Exception ex)
            {
                Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
                return null;
            }
        }


    }
}
