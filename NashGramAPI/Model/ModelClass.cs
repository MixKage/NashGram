using System.ComponentModel.DataAnnotations;

namespace NashGramAPI.Model;

public class ModelClass
{
    public static readonly string pathDB = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString().Replace("NashGramAPI.dll", "") + @"\sqlite\databaseNashGram.db";
    public record AccountCreateInput(
    [StringLength(50)][Required] string login,
    [StringLength(50)][Required] string password
);
    public record AccountUpdateInput(
    [Required] long id,
    [StringLength(50)][Required] string text
);
    public record PostCreate(
    [Required] long idAuthor,
    [Required] string uri,
    [StringLength(500)] string descryption,
    [StringLength(50)] string tag
);
}
