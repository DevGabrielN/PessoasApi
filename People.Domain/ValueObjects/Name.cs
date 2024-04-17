using System.ComponentModel.DataAnnotations;

namespace People.Domain.ValueObjects;
public class Name 
{
    [MaxLength(150)]
    public string FirstName { get; private set; }
    [MaxLength(150)]
    public string LastName { get; private set; }

    public Name(string firstName, string lastName)
    {
        FirstName = firstName.Trim().ToUpper();
        LastName = lastName.Trim().ToUpper();
    }
    public Name() { }    
}
