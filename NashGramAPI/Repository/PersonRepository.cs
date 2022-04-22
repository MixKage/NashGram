using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NashGramAPI.Model;

namespace NashGramAPI.Repository;

public static class PersonRepository
{
    private static string pathDB = ModelClass.pathDB;

    public static string? GetEmailFromId(long id)
    {
        var person = GetPersonFromID(id);
        if (person != null)
            return person.Email;
        else
            return null;
    }

    public static string? GetNameFromId(long id)
    {
        var person = GetPersonFromID(id);
        if (person != null)
            return person.Name;
        else
            return null;
    }

    public static string? GetStatusFromId(long id)
    {
        var person = GetPersonFromID(id);
        if (person != null)
            return person.Status;
        else
            return null;
    }

    public static long? GetCountryFromId(long id)
    {
        var person = GetPersonFromID(id);
        if (person != null)
            return person.Country;
        else
            return null;
    }

    public static long? GetAgeFromId(long id)
    {
        var person = GetPersonFromID(id);
        if (person != null)
            return person.Age;
        else
            return null;
    }

    public static string? GetNumberFromId(long id)
    {
        var person = GetPersonFromID(id);
        if (person != null)
            return person.Number;
        else
            return null;
    }
    /// <summary>
    /// Получает Person по id
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
                            person.Id = reader.GetInt64(0);
                            person.Email = reader.GetString(1);
                            person.Name = reader.GetString(2);
                            person.Status = reader.GetString(3);
                            person.Country = reader.GetInt64(4);
                            person.Age = reader.GetInt64(5);
                            person.Number = reader.GetString(6);
                        }
                    }
                }
            }
            if (person.Id != id) return null;
            return person;
        }
        catch (Exception ex)
        {
            Log.AddLog($"Account not found from ID: {id} | " + ex.Message, true);
            return null;
        }
    }

}
