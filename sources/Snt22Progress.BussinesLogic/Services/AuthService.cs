using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.BussinesLogic.Models.DataContracts;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.Logging;

namespace Snt22Progress.BussinesLogic.Services
{
	public class AuthService : IAuthService
	{
		private readonly AuthSettings _authSettings;
		private readonly IPasswordHashService _passwordHashService;
		private readonly IProgressLogger _progressLogger;

		private readonly IRepository<User, int> _userRepository;
		private readonly IRepository<Role, int> _rolesRepository;
		private readonly IRepository<UserToRole, int> _userToRolesRepository;

		public AuthService(AuthSettings authSettings,
			IPasswordHashService passwordHashService,
			IProgressLogger progressLogger,
			IRepository<User, int> userRepository,
			IRepository<Role, int> rolesRepository,
			IRepository<UserToRole, int> userToRolesRepository)
		{
			_authSettings = authSettings;
			_passwordHashService = passwordHashService;
			_progressLogger = progressLogger;
			_userRepository = userRepository;
			_rolesRepository = rolesRepository;
			_userToRolesRepository = userToRolesRepository;
		}

		public async Task<ResultResponse<JwtAuthorizationDto>> LoginAsync(string login, string password)
		{
			try
			{
				var identity = await GetIdentityAsync(login, password);
				if (identity == null)
				{
					return ResultResponse<JwtAuthorizationDto>.GetBadResponse(StatusCode.BadRequest, "Неверный логин и(или) пароль");
				}

				DateTime? endLifeTime = null;
				DateTime? notBefore = null;
				DateTime? expires = null;

				if (_authSettings.ValidateLifetime) // Если токен валидируется по времени
				{
					notBefore = DateTime.UtcNow;
					expires = notBefore.Value.Add(TimeSpan.FromMinutes(_authSettings.Lifetime));
				}

				// Создаем JWT-токен
				var jwt = new JwtSecurityToken(
						issuer: _authSettings.Issuer,
						audience: _authSettings.Audience,
						notBefore: notBefore,
						claims: identity.Claims,
						expires: expires,
						signingCredentials: new SigningCredentials(_authSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
				var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

				return ResultResponse<JwtAuthorizationDto>.GetSuccessResponse(
					new JwtAuthorizationDto
					{
						UsedLifetime = _authSettings.ValidateLifetime,
						EndLifetime = endLifeTime,

						Token = encodedJwt
					});
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { login, password }, GetType().Name, nameof(LoginAsync));
				return ResultResponse<JwtAuthorizationDto>.GetInternalServerErrorResponse();
			}
		}

		private async Task<ClaimsIdentity> GetIdentityAsync(string email, string password)
		{
			var user = (await _userRepository.GetAsync($"where lower(email) = lower('{email}')")).FirstOrDefault();
			if (user == null) // Если пользователя не найдено
			{
				return null;
			}
			var passwordHash = await _passwordHashService.GetPasswordHashWithSalt(password, user.salt);
			if (passwordHash == user.password_hash) // Пароль проходит по хешу
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
					new Claim(ClaimTypes.Email, user.email.ToString())
				};
				ClaimsIdentity claimsIdentity =
				new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
				return claimsIdentity;
			}

			// Если пароль не подошел
			return null;
		}

		public async Task<ResultResponse> IsInRole(int userId, params string[] roles)
		{
			try
			{
				var user = await _userRepository.GetAsync(userId);
				if (user != null)
				{
					var roleIds = (await _userToRolesRepository.GetAsync($"where user_id = {userId}")).Select(x => x.role_id);
					IEnumerable<string> roleNames = new List<string>();
					if (roleIds.Count() > 0)
					{
						roleNames = (await _rolesRepository.GetAsync($"where id IN ({string.Join(", ", roleIds)})")).Select(x => x.name);
					}
					var isInRole = false;
					foreach (var r in roleNames)
					{
						if (roles.Any(x => x.ToLower() == r.ToLower()))
						{
							isInRole = true;
							break;
						}
					}
					return new ResultResponse(isSuccess: isInRole, statusCode: isInRole ? StatusCode.OK : StatusCode.NotFound);
				}
				return ResultResponse.GetBadResponse(StatusCode.NotFound);
			}
			catch (Exception ex)
			{
				_progressLogger.Error(ex, new { userId, roles }, GetType().Name, nameof(IsInRole));
				return ResultResponse.GetInternalServerErrorResponse();
			}
		}
	}
}
