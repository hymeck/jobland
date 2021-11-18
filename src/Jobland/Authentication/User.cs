using Microsoft.AspNetCore.Identity;

namespace Jobland.Authentication;

public enum Gender
{
    Male = 0,
    Female = 1
}

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
