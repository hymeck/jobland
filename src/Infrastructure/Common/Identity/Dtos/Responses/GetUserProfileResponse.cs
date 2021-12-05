namespace Jobland.Infrastructure.Common.Identity.Dtos.Responses;

public sealed record GetUserProfileResponse(
    string Id, 
    string UserName, 
    string FirstName, 
    string LastName,
    Gender Gender, 
    DateTime? BirthDate, 
    string Email,
    string PhoneNumber)
{
    public List<string> Images { get; set; } = new();
}
