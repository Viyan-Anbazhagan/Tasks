using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Models;

public class UserCredential
{
    [Key]
    public string? Username { get; set; }

    public string? Password { get; set; }

}
