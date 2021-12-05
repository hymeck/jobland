namespace Jobland.Infrastructure.Common.Identity.Dtos.Responses;

public sealed record GetUserProfileResponse(
    string Id, 
    string UserName, 
    string FirstName, 
    string LastName,
    Gender Gender, 
    DateTime? BirthDate, 
    string Email)
{
    public string ImageUrl { get; set; } = "";
}
