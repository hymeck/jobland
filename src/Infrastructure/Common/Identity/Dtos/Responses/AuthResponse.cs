namespace Jobland.Infrastructure.Common.Identity.Dtos.Responses;

public sealed record AuthResponse
{
    public string Token { get; } = "";
    public bool Success { get; }
    public string UserId { get; init; } = "";
    public IReadOnlyList<string> Errors { get; } = new List<string>();

    private AuthResponse(string token)
    {
        Token = token;
        Success = true;
    }

    private AuthResponse(IReadOnlyList<string> errors)
    {
        Errors = errors;
        Success = false;
    }

    public static AuthResponse Ok(string token, string userId) => new(token) { UserId = userId };
    public static AuthResponse Fail(List<string> errors) => new(errors);
    public static AuthResponse Fail(string error) => new(new List<string> { error });
}

public static class AuthResponses
{
    public static AuthResponse InvalidInput() => AuthResponse.Fail("invalid input");
    public static AuthResponse AccountAlreadyExists() => AuthResponse.Fail("account with specified email already exists");
    public static AuthResponse EmailNotFound() => AuthResponse.Fail("there is no account with specified email");
    public static AuthResponse IncorrectPassword() => AuthResponse.Fail("incorrect password");
}
