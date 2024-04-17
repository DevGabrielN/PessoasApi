using System.ComponentModel.DataAnnotations;

namespace People.Domain.DTOs;

public class ReadPersonDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string CPF { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateBirth { get; set; }
    
    public string UF { get; set; }
}
