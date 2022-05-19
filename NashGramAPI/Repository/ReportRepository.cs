using NashGramAPI.Model;
using System.Data.SQLite;

namespace NashGramAPI.Repository;

/// <summary>
/// Класс для работы с таблицой account
/// </summary>
public static class ReportRepository
{
    private static string pathDB = ModelClass.pathDB;

    public static long? CreateReport(ModelClass.UpdateInput input, long idReported)
    {
        long? newId = null;
        try
        {
            //Создание аккаунта
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"INSERT INTO Report (                       
                       id_post,
                       id_author_report,
                       message
                   )
                   VALUES (                       
                       '{input.id}',
                       '{idReported}',
                       '{input.text}'
                   )
                RETURNING id_report", connection))
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

            if (newId == 0 || newId == null) { return null; }
            Log.AddLog($"Create report id: {newId}, id_post {input.id}, id_author_report {idReported}, message {input.text}", false);
        }
        catch (Exception ex)
        {
            Log.AddLog(ex.Message, true);
            return null;
        }
        return newId;
    }

    public static long? GetCountReportByIdPost(long idPost)
    {
        long? newId = null;
        try
        {
            //Создание аккаунта
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"SELECT COUNT(1) FROM Report WHERE {idPost}", connection))
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

            if (newId == 0 || newId == null) { return null; }
        }
        catch (Exception ex)
        {
            Log.AddLog(ex.Message, true);
            return null;
        }
        return newId;
    }
}
