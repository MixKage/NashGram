using System.ComponentModel.DataAnnotations;

namespace NashGramAPI.Model;

public class ModelClass
{
    public record AccountInput(
    [StringLength(50)][Required] string login,
    [StringLength(50)][Required] string password
);

}
