using System.ComponentModel.DataAnnotations;

namespace People.Domain.DTOs;

public class LoginUserDto
{
    public string UserName { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}
