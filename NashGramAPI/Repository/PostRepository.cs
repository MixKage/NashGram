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
        //return Convert.ToBase64String(post.Uri);
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
        string uri = postCreate.uri;
        string description = postCreate.descryption;
        string tag = postCreate.tag;

        long? postId = null;
        try
        {
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
    /// Получает все посты аккаунта
    /// </summary>
    public static List<Post>? GetPostsFromIdAccount(long id)
    {
        List<Post> posts;
        try
        {
            posts = new List<Post>();
            Post? post = null;
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($"SELECT * FROM Post WHERE author = {id};", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            post = new Post();
                            post.Id = reader.GetInt64(0);
                            post.Author = reader.GetInt64(1);
                            post.Uri = reader.GetString(2);
                            post.Descryption = reader.GetString(3);
                            post.Tag = reader.GetString(4);
                            posts.Add(post);
                        }
                    }
                }
            }
            if (posts.Count == 0) return null;
            return posts;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Posts not found from IdAccount: {id} | " + ex.Message, true);
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
            Log.AddLog($"Post not found from ID: {id} | " + ex.Message, true);
            return null;
        }
    }

    /// <summary>
    /// Получает все посты
    /// </summary>        
    public static List<Post>? GetAllPosts()
    {
        List<Post>? posts;
        try
        {
            posts = new List<Post>();
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($"SELECT * FROM Post;", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Post post = new Post();
                            post.Id = reader.GetInt64(0);
                            post.Author = reader.GetInt64(1);
                            post.Uri = reader.GetString(2);
                            post.Descryption = reader.GetString(3);
                            post.Tag = reader.GetString(4);
                            posts.Add(post);
                        }
                    }
                }
            }
            if (posts.Count == 0) return null;
            return posts;
        }
        catch (Exception ex)
        {
            Log.AddLog($"GetAllPosts failed | " + ex.Message, true);
            return null;
        }
    }

    /// <summary>
    /// Удаляет Post по id
    /// </summary>        
    public static bool DeletePostFromIdPost(long id)
    {
        int count = 0;
        try
        {

            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(@$"DELETE FROM Post
                    WHERE id_post = '{id}'; ", connection))
                {
                    count = cmd.ExecuteNonQuery();
                }
            }
            if (count == 0) return false;
            return true;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Post not delete from ID post: {id} | " + ex.Message, true);
            return false;
        }
    }
}
