using System.ComponentModel.DataAnnotations;

namespace NashGramAPI.Model;

public class ModelClass
{
    public record AccountCreateInput(
    [StringLength(50)][Required] string login,
    [StringLength(50)][Required] string password
);
    public record AccountUpdateInput(
    [Required] long id,
    [StringLength(50)][Required] string text
);
}
