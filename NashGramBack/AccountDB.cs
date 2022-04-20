        /// <summary>
        /// Создаёт аккаунт и возвращает его id (-1: login уже существует)
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
                                account.id_account = reader.GetInt64(0);
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
