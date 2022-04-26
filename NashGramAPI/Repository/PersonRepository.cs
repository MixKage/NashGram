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
    /// Изменяет данные Person в зависимости от полученных данных
    /// </summary>
    /// <param name="input">{id, text}</param>
    /// <param name="mode">0-Email,1-Name,2-Status,3-Country,4-age,5-number</param>
    /// <returns></returns>
    public static bool UpdateInfoFromId(ModelClass.UpdateInput input, int mode)
    {
        string param = SwitchParam(mode);
        long _id = input.id;
        string _input = input.text;
        int count = 0;
        try
        {
            using (var connection = new SQLiteConnection(@$"Data Source={pathDB};Version=3;"))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand($@"UPDATE Person
                        SET {param} = '{_input}'
                        WHERE id_account = '{_id}'", connection))
                {
                    count = cmd.ExecuteNonQuery();
                }
            }
            if (count == 0) return false;
            Log.AddLog($"{param} update: id {_id}, new {param} {_input}", false);
            return true;
        }
        catch (Exception ex)
        {
            Log.AddLog($"{param} not update: id {_id}, {param} {_input} | " + ex.Message, true);
            return false;
        }
    }

    private static string SwitchParam(int mode)
    {
        switch (mode)
        {
            case 0:
                return "email";
            case 1:
                return "name";
            case 2:
                return "status";
            case 3:
                return "country";
            case 4:
                return "age";
            case 5:
                return "number";
            default:
                return "error";
        }
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
