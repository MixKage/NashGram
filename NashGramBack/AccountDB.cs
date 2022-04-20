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
