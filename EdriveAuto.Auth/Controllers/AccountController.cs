using System.Security.Claims;
using EDriveAuto.Auth.JwtManager;
using EDriveAuto.Auth.Service.Interfaces;
using EDriveAuto.Auth.UserServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EdriveAuto.Common;
using EDriveAuto.Auth.DTO;

namespace EDriveAuto.Auth.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class AccountController : ControllerBase
    {
	    #region ctor

	    private readonly ILogger<AccountController> _logger;
	    private readonly IJwtAuthManager _jwtAuthManager;
	    private IAuthenticatable _service;

	    public AccountController(
		    ILogger<AccountController> logger, 
		    IJwtAuthManager jwtAuthManager,
			IAuthenticatable authenticatable)
	    {
		    _logger = logger;
		    _jwtAuthManager = jwtAuthManager;
            _service = authenticatable;
	    }

	    #endregion

		[AllowAnonymous]
	    [HttpPost("login")]
	    public virtual async Task<ActionResult> Login([FromBody] LoginRequest request)
	    {
		    if (!ModelState.IsValid)
			    return BadRequest();
		    
		    if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
			    return Unauthorized();
			
			request.Role = request.Role.TrimCapitalize(defaultValue: UserRoles.Dealer);

			PasswordHasher<Account> hasher = new PasswordHasher<Account>();
			var user = await _service.GetUserByUniqueKey(Guid.Parse(request.UserName));
            //var pass = hasher.HashPassword(null, request.Password);

            if (user == null)
	            throw new NullReferenceException("User not found");

			var isPasswordCorrect = hasher.VerifyHashedPassword(null, user.Password, request.Password);
			if (isPasswordCorrect.ToString().ToLower() != "success")
			    return Unauthorized();

		    await _service.UpdateLastLoginDate(Guid.Parse(request.UserName));
			
		    return GenerateAuthentication(request);
	    }

	    [HttpGet("user")]
	    [Authorize]
	    public virtual ActionResult GetCurrentUser()
	    {
		    if (User.Identity == null) 
			    return Unauthorized();

		    return Ok(new LoginResult
		    {
			    UserName = User.Identity.Name ?? string.Empty,
			    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
			    OriginalUserName = User.FindFirst("OriginalUserName")?.Value ?? string.Empty
		    });

	    }

	    [HttpPost("logout")]
	    [Authorize]
	    public virtual ActionResult Logout()
	    {
		    if (User.Identity?.Name == null) 
			    return Ok();

		    var userName = User.Identity.Name;
		    _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
		    _logger.LogInformation($"User [{userName}] logged out the system.");

		    return Ok();

	    }

	    [HttpPost("refresh-token")]
	    [Authorize]
	    public virtual async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
	    {
		    try
		    {
			    if (User.Identity == null) 
				    return await Task.FromResult<ActionResult>(Ok());

			    var userName = User.Identity.Name;
			    _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

			    if (string.IsNullOrWhiteSpace(request.RefreshToken))
			    {
				    return Unauthorized();
			    }

			    var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");

			    if (accessToken == null)
				    throw new NullReferenceException("Access Token is null");

			    var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
			    _logger.LogInformation($"User [{userName}] has refreshed JWT token.");

			    if (userName == null)
				    throw new NullReferenceException("Username is null");

			    return Ok(new LoginResult
			    {
				    UserName = userName,
				    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
				    AccessToken = jwtResult.AccessToken,
				    RefreshToken = jwtResult.RefreshToken.TokenString
			    });
		    }
		    catch (SecurityTokenException e)
		    {
			    return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
		    }
	    }

	    [HttpPost("impersonation")]
	    [Authorize(Roles = UserRoles.Admin)]
	    public virtual async Task<ActionResult> Impersonate([FromBody] ImpersonationRequest request)
	    {
		    if (User.Identity == null) 
			    return await Task.FromResult<ActionResult>(Ok());

		    var userName = User.Identity.Name;
		    _logger.LogInformation($"User [{userName}] is trying to impersonate [{request.UserName}].");

		    var impersonatedRole = UserRoles.Admin; //await _userService.GetUserRole(request.UserName);
		    if (string.IsNullOrWhiteSpace(impersonatedRole))
		    {
			    _logger.LogInformation($"User [{userName}] failed to impersonate [{request.UserName}] due to the target user not found.");
			    return BadRequest($"The target user [{request.UserName}] is not found.");
		    }
		    if (impersonatedRole == UserRoles.Admin)
		    {
			    _logger.LogInformation($"User [{userName}] is not allowed to impersonate another Admin.");
			    return BadRequest("This action is not supported.");
		    }

		    var claims = new[]
		    {
			    new Claim(ClaimTypes.Name,request.UserName),
			    new Claim(ClaimTypes.Role, impersonatedRole),
			    new Claim("OriginalUserName", userName ?? string.Empty)
		    };

		    var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
		    _logger.LogInformation($"User [{request.UserName}] is impersonating [{request.UserName}] in the system.");
		    return Ok(new LoginResult
		    {
			    UserName = request.UserName,
			    Role = impersonatedRole,
			    OriginalUserName = userName ?? string.Empty,
			    AccessToken = jwtResult.AccessToken,
			    RefreshToken = jwtResult.RefreshToken.TokenString
		    });
	    }

	    [HttpPost("stop-impersonation")]
	    public virtual async Task<ActionResult> StopImpersonation()
	    {
		    if (User.Identity == null) 
			    return await Task.FromResult<ActionResult>(Ok());

		    var userName = User.Identity.Name;
		    var originalUserName = User.FindFirst("OriginalUserName")?.Value;
		    if (string.IsNullOrWhiteSpace(originalUserName))
		    {
			    return BadRequest("You are not impersonating anyone.");
		    }
		    _logger.LogInformation($"User [{originalUserName}] is trying to stop impersonate [{userName}].");

		    var role = UserRoles.Admin; //await _userService.GetUserRole(originalUserName);
		    var claims = new[]
		    {
			    new Claim(ClaimTypes.Name,originalUserName),
			    new Claim(ClaimTypes.Role, role)
		    };

		    var jwtResult = _jwtAuthManager.GenerateTokens(originalUserName, claims, DateTime.Now);
		    _logger.LogInformation($"User [{originalUserName}] has stopped impersonation.");
		    return Ok(new LoginResult
		    {
			    UserName = originalUserName,
			    Role = role,
			    OriginalUserName = null,
			    AccessToken = jwtResult.AccessToken,
			    RefreshToken = jwtResult.RefreshToken.TokenString
		    });

	    }
		
	    protected virtual ActionResult GenerateAuthentication(LoginRequest request)
	    {
		    var claims = new[]
		    {
			    new Claim(ClaimTypes.Name, request.UserName),
			    new Claim(ClaimTypes.Role, request.Role)
		    };

		    var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
		    _logger.LogInformation($"User [{request.UserName}] logged in the system.");
		    return Ok(new LoginResult
		    {
			    UserName = request.UserName,
			    Role = request.Role,
			    AccessToken = jwtResult.AccessToken,
			    RefreshToken = jwtResult.RefreshToken.TokenString
		    });
	    }
    }
}
