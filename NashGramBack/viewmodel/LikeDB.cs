using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashGramBack.Model;

namespace NashGramBack.ViewModel
{
    internal class LikeDB
    {
        private static string pathDB = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString().Replace("NashGramBack.dll", "") + @"\sqlite\databaseNashGram.db";

        /// <summary>
        /// Получает Like по id_post
        /// </summary>        
        public static Like? GetLikeFromIdPost(long id)
        {
            try
            {
                Like? like = null;
                using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand($"SELECT * FROM Like WHERE id_post = {id};", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            like = new Like();

                            while (reader.Read())
                            {
                                like.id = reader.GetInt64(0);
                                like.idPost = reader.GetInt64(1);
                                like.idAccount = reader.GetInt64(2);  
                            }
                        }
                    }
                }
                return like;
            }
            catch (Exception ex)
            {
                Log.AddLog($"Like not found from ID: {id} | " + ex.Message, true);
                return null;
            }
        }

        public static Like? GetLikeFromId(long id)
        {
            try
            {
                Like? like = null;
                using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand($"SELECT * FROM Like WHERE id = {id};", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            like = new Like();

                            while (reader.Read())
                            {
                                like.id = reader.GetInt64(0);
                                like.idPost = reader.GetInt64(1);
                                like.idAccount = reader.GetInt64(2);
                            }
                        }
                    }
                }
                return like;
            }
            catch (Exception ex)
            {
                Log.AddLog($"Like not found from ID: {id} | " + ex.Message, true);
                return null;
            }
        }

        public static Like? GetLikeFromIdAccount(long id)
        {
            try
            {
                Like? like = null;
                using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand($"SELECT * FROM Like WHERE id_account = {id};", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            like = new Like();

                            while (reader.Read())
                            {
                                like.id = reader.GetInt64(0);
                                like.idPost = reader.GetInt64(1);
                                like.idAccount = reader.GetInt64(2);
                            }
                        }
                    }
                }
                return like;
            }
            catch (Exception ex)
            {
                Log.AddLog($"Like not found from ID: {id} | " + ex.Message, true);
                return null;
            }
        }
    }
}
