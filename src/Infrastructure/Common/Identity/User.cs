using Microsoft.AspNetCore.Identity;

namespace Jobland.Infrastructure.Common.Identity;

public class User : IdentityUser
{
    public override string UserName => Email;
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public Gender Gender { get; set; } = Gender.Male;
    public DateTime? BirthDate { get; set; }
    public User SetUsername()
    {
        UserName = Email;
        return this;
    }

    public override string ToString() => $"{FirstName} {LastName}";
}
