using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashGramAPI.Model;

namespace NashGramAPI.Repository;

public static class PostRepository
{
    private static string pathDB = ModelClass.pathDB;

    public static long? GetAuthor(long id)
    {
        var post = GetPostFromIdPost(id);
        if (post != null)
            return post.Author;
        else
            return null;
    }

    public static string? GetUri(long id)
    {
        var post = GetPostFromIdPost(id);
        if (post != null)
            return post.Uri;
        else
            return null;
    }

    public static string? GetDescryption(long id)
    {
        var post = GetPostFromIdPost(id);
        if (post != null)
            return post.Descryption;
        else
            return null;
    }

    public static string? GetTag(long id)
    {
        var post = GetPostFromIdPost(id);
        if (post != null)
            return post.Tag;
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
                            post.Id = reader.GetInt64(0);
                            post.Author = reader.GetInt64(1);
                            post.Uri = reader.GetString(2);
                            post.Descryption = reader.GetString(3);
                            post.Tag = reader.GetString(4);
                        }
                    }
                }
            }
            if (post.Id != id) return null;
            return post;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
            return null;
        }
    }
}
