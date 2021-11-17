namespace Jobland.Authentication;

public class AuthResult
{
    public string Token { get; } = "";
    public bool Success { get; }
    public List<string> Errors { get; } = new();

    private AuthResult(string token)
    {
        Token = token;
        Success = true;
    }

    private AuthResult(List<string> errors)
    {
        Errors = errors;
        Success = false;
    }

    public static AuthResult Ok(string token) => new(token);
    public static AuthResult Fail(List<string> errors) => new(errors);
    public static AuthResult Fail(string error) => new(new List<string> { error });
}
