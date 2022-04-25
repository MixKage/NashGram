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
            return Convert.ToHexString(post.Uri);
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
    public static long? CreatePost(ModelClass.PostCreate postCreate)
    {
        long idAuthor = postCreate.idAuthor;
        byte[]? uri = null;
        string description = postCreate.descryption;
        string tag = postCreate.tag;

        long? postId = null;
        try
        {
            uri = Convert.FromHexString(postCreate.uri);
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"INSERT INTO Post (
                     author,
                     uri,
                     descryption,
                     tag
                 )
                 VALUES (                    
                     '{idAuthor}',
                     '{uri}',
                     '{description}',
                     '{tag}'
                 )
                RETURNING id_post
", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            postId = reader.GetInt64(0);
                        }
                    }
                }
            }
            Log.AddLog($"Post create: id {postId}, idAuthor {idAuthor}", false);
            return postId;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Post no create: {idAuthor} | " + ex.Message, true);
            return null;
        }
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
                            post.Uri = (byte[])reader["uri"];
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
