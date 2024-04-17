using Microsoft.EntityFrameworkCore;
using People.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace People.Domain.Entities;

[Index(nameof(CPF), IsUnique = true)]
public class Person
{
    public Person(Name name, DateTime dateBirth, string uf, string cpf)
    {
        Name = name;
        CPF = cpf.Replace(".", "").Replace("-", "");
        DateBirth = dateBirth;
        UF = uf.ToUpper();
    }
    public Person(){ }
    [Key]
    [Required]
    public int Id { get; private set; }
    [Required]    
    public virtual Name Name { get; private set; }
    [Required]
    public string CPF { get; private set; }
    [Required]
    public DateTime DateBirth { get; private set; }
    [Required]
    [MaxLength(2)]
    public string UF { get; private set; }
}
