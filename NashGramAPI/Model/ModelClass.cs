using System.ComponentModel.DataAnnotations;

namespace NashGramAPI.Model;

public class ModelClass
{
    public static readonly string pathDB = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString().Replace("NashGramAPI.dll", "") + @"\sqlite\databaseNashGram.db";
    public record AccountCreateInput(
    [StringLength(50)][Required] string login,
    [StringLength(50)][Required] string password
);
    public record UpdateInput(
    [Required] long id,
    [Required] string text
);
    public record PostCreate(    
    [Required] string image,
    [StringLength(500)] string descryption,
    [StringLength(50)] string tag,
    long idAuthor
);
    public record CreateLike(    
    [Required] long id_post,
    [Required] long id_account
);
    public record UpdatePerson(
    [Required] long Id,
    [Required] string? Email,
    [Required] string? Name,
    [Required] string? Status, 
    [Required] long Country,
    [Required] long Age,
    [Required] string? Number,
    [Required] string? Avatar
);
}
