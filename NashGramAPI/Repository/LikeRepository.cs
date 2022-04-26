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
            if (likes.Count == 0) { Log.AddLog($"Likes not found from ID: {id}", true); return null; }
            return likes;
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

    public static Like? GetLikesFromIdAccount(long id)
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
                            like.Id = reader.GetInt64(0);
                            like.IdPost = reader.GetInt64(1);
                            like.IdAccount = reader.GetInt64(2);
                        }
                    }
                }
            }
            if (like.IdAccount != id) return null;
            return like;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Like not found from ID: {id} | " + ex.Message, true);
            return null;
        }
    }

    //public static Like? GetLikeFromIdAccount(long id)
    //{
    //    try
    //    {
    //        Like? like = null;
    //        using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
    //        {
    //            connection.Open();
    //            using (var cmd = new SQLiteCommand($"SELECT * FROM Like WHERE id_account = {id};", connection))
    //            {
    //                using (var reader = cmd.ExecuteReader())
    //                {
    //                    like = new Like();

    //                    while (reader.Read())
    //                    {
    //                        like.Id = reader.GetInt64(0);
    //                        like.IdPost = reader.GetInt64(1);
    //                        like.IdAccount = reader.GetInt64(2);
    //                    }
    //                }
    //            }
    //        }
    //        if (like.IdAccount != id) return null;
    //        return like;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.AddLog($"Like not found from ID: {id} | " + ex.Message, true);
    //        return null;
    //    }
    //}
}
