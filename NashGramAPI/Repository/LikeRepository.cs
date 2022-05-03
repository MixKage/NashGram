using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashGramAPI.Model;

namespace NashGramAPI.Repository;

internal class LikeRepository
{
    private static string pathDB = ModelClass.pathDB;

    /// <summary>
    /// Получает Like по id_post
    /// </summary>        
    public static List<Like>? GetLikesFromIdPost(long id)
    {
        List<Like> likes;
        try
        {
            likes = new List<Like>();
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($"SELECT * FROM Like WHERE id_post = {id};", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Like like = new Like();
                            like.Id = reader.GetInt64(0);
                            like.IdPost = reader.GetInt64(1);
                            like.IdAccount = reader.GetInt64(2);
                            likes.Add(like);
                        }
                    }
                }
            }
            if (likes.Count == 0) { Log.AddLog($"Likes not found from IdPost: {id}", true); return likes; }
            return likes;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Like not found from IdPost: {id} | " + ex.Message, true);
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
                            like.Id = reader.GetInt64(0);
                            like.IdPost = reader.GetInt64(1);
                            like.IdAccount = reader.GetInt64(2);
                        }
                    }
                }
            }
            if (like.Id != id) return null;
            return like;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Like not found from ID: {id} | " + ex.Message, true);
            return null;
        }
    }

    public static List<Like>? GetLikesFromIdAccount(long id)
    {
        List<Like> likes;
        try
        {
            likes = new List<Like>();
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($"SELECT * FROM Like WHERE id_post = {id};", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Like like = new Like();
                            like.Id = reader.GetInt64(0);
                            like.IdPost = reader.GetInt64(1);
                            like.IdAccount = reader.GetInt64(2);
                            likes.Add(like);
                        }
                    }
                }
            }
            if (likes.Count == 0) { Log.AddLog($"Likes not found from IdAccount: {id}", true); return null; }
            return likes;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Like not found from IdAccount: {id} | " + ex.Message, true);
            return null;
        }
    }

    public static long? CreateLikeFromIdAccountIdPost(ModelClass.CreateLike input)
    {
        long? id = null;
        try
        {
            List<Like>? likes = GetLikesFromIdPost(input.id_post);
            if (likes != null)
            {
                foreach (var like in likes)
                {
                    if(like.IdAccount == input.id_account)
                    {
                        Log.AddLog($"Like not create from id_account: {input.id_account}, id_post: {input.id_post} (LIKE EXIST)", true);
                        return null;
                    }
                }
            }

            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"INSERT INTO Like (
                        id_post,
                        id_account
                    )
                    VALUES (                        
                        '{input.id_post}',
                        '{input.id_account}'
                    )
                    RETURNING id", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt64(0);
                        }
                    }
                }
            }
            if (id == null) { Log.AddLog($"Like not create from id_account: {input.id_account}, id_post: {input.id_post}", true); return null; }
            Log.AddLog($"Like create from id_account: {input.id_account}, id_post: {input.id_post}", false);
            return id;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Like not create from id_account: {input.id_account}, id_post: {input.id_post} | " + ex.Message, true);
            return null;
        }
    }

    public static bool DeleteLikeFromId(long id)
    {
        int count = 0;
        try
        {            
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"DELETE FROM Like
                        WHERE id = '{id}';", connection))
                {
                    count = cmd.ExecuteNonQuery();
                }
            }
            if (count == 0) { Log.AddLog($"Likes not delete from Id: {id}", true); return false; }
            Log.AddLog($"Likes delete from Id: {id}", false);
            return true;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Like not delete from Id: {id} | " + ex.Message, true);
            return false;
        }
    }
}
