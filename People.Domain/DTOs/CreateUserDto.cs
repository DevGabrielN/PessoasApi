
using System.ComponentModel.DataAnnotations;

namespace People.Domain.DTOs;

public class CreateUserDto
{
    public string UserName { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}
