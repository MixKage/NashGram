using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramBack
{
    internal class PostDB
    {
        private static string pathDB = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString().Replace("NashGramBack.dll", "") + @"\sqlite\databaseNashGram.db";

        public static long GetAuthor(long id)
        {
            var post = GetPostFromIdPost(id);
            if (post != null)
                return post.author;
            else
                return -1;
        }

        public static string? GetUri(long id)
        {
            var post = GetPostFromIdPost(id);
            if (post != null)
                return post.uri;
            else
                return null;
        }

        public static string? GetDescryption(long id)
        {
            var post = GetPostFromIdPost(id);
            if (post != null)
                return post.descryption;
            else
                return null;
        }

        public static string? GetTag(long id)
        {
            var post = GetPostFromIdPost(id);
            if (post != null)
                return post.tag;
            else
                return null;
        }
        /// <summary>
        /// Получает Post по id
        /// </summary>        
        public static Post? GetPostFromIdPost(long id)
        {
            try
            {
                Post? post = null;
                using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand($"SELECT * FROM Post WHERE id_post = {id};", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            post = new Post();

                            while (reader.Read())
                            {
                                post.author = reader.GetInt64(1);
                                post.uri = reader.GetString(2);
                                post.descryption = reader.GetString(3);
                                post.tag = reader.GetString(4);
                            }
                        }
                    }
                }
                return post;
            }
            catch (Exception ex)
            {
                Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
                return null;
            }
        }
    }
}
