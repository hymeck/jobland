using AutoMapper;
using Jobland.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Endpoints;

public class AuthEndpoints : ApiEndpointBase
{
    public const string RegisterRoot = "/register";
    public const string LoginRoot = "/login";
    
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IJwtTokenService _tokenService;

    public AuthEndpoints(UserManager<User> userManager, IMapper mapper, IJwtTokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost(RegisterRoot)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest? dto)
    {
        if (dto == null)
            return BadRequest(AuthResults.InvalidInput());

        if (await _userManager.FindByEmailAsync(dto.Email) != null)
            return BadRequest(AuthResults.AccountAlreadyExists());

        var user = _mapper.Map<RegistrationRequest, User>(dto).SetUsername();
        var created = await _userManager.CreateAsync(user, dto.Password);
        return !created.Succeeded
            ? UnprocessableEntity(AuthResult.Fail(created.Errors.Select(e => e.Description).ToList()))
            : Created(RegisterRoot, AuthResult.Ok(_tokenService.GenerateJwtToken(user)));
    }

    [AllowAnonymous]
    [HttpPost(LoginRoot)]
    public async Task<IActionResult> Login([FromBody] LoginRequest? dto)
    {
        if (dto == null)
            return BadRequest(AuthResults.InvalidInput());

        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return BadRequest(AuthResults.EmailNotFound());
        var valid = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!valid)
            return BadRequest(AuthResults.IncorrectPassword());

        return Ok(AuthResult.Ok(_tokenService.GenerateJwtToken(user)));
    }
}
