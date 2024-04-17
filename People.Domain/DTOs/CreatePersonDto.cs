using System.ComponentModel.DataAnnotations;

namespace People.Domain.DTOs;

public class CreatePersonDto
{
    /// <example>Joao</example>
    public string FirstName { get; set; }
    /// <example>da Silva</example>
    public string LastName { get; set; }
    /// <example>000.000.000-00</example>
    public string CPF { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateBirth { get; set; }
    /// <example>GO</example>
    public string UF { get; set; }
}
