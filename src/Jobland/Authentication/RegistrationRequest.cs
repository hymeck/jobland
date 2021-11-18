namespace Jobland.Authentication;

public sealed record RegistrationRequest
(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Gender Gender,
    DateTime BirthDate,
    string PhoneNumber
);
