using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramBack
{
    public static class PersonDB
    {
        private static string pathDB = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString().Replace("NashGramBack.dll", "") + @"\sqlite\databaseNashGram.db";
        
        public static string? GetEmailFromId(long id)
        {
            var person = GetPersonFromID(id);
            if (person != null)
                return person.email;
            else
                return null;
        }

        public static string? GetNameFromId(long id)
        {
            var person = GetPersonFromID(id);
            if (person != null)
                return person.name;
            else
                return null;
        }

        public static string? GetStatusFromId(long id)
        {
            var person = GetPersonFromID(id);
            if (person != null)
                return person.status;
            else
                return null;
        }


        /// <summary>
        /// Получает Account по id
        /// </summary>        
        public static Person? GetPersonFromID(long id)
        {
            try
            {
                Person? person = null;
                using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand($"SELECT * FROM Person WHERE id_account = {id};", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            person = new Person();

                            while (reader.Read())
                            {
                                person.email = reader.GetString(1);
                                person.name = reader.GetString(2);
                                person.status = reader.GetString(3);
                                person.country = reader.GetString(4);
                                person.age = reader.GetInt64(5);
                                person.number = reader.GetString(6);
                            }
                        }
                    }
                }
                return person;
            }
            catch (Exception ex)
            {
                Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
                return null;
            }
        }

    }
}
