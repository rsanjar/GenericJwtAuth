using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EDriveAuto.Auth.JwtManager
{
	public interface IJwtAuthManager
	{
		IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
		JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
		JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);
		void RemoveExpiredRefreshTokens(DateTime now);
		void RemoveRefreshTokenByUserName(string userName);
		(ClaimsPrincipal principal, JwtSecurityToken?) DecodeJwtToken(string token);
	}
}