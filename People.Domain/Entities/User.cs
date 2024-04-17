using Microsoft.AspNetCore.Identity;

namespace People.Domain.Entities;

public class User : IdentityUser
{
    public User() : base()
    {
    }
}
