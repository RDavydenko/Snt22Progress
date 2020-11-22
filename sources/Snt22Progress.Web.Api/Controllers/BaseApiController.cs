using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;

namespace Snt22Progress.Web.Api.Controllers
{
	[Produces("application/json")]
	public class BaseApiController : ControllerBase
	{
		private readonly IAuthService _authService;

		public int? UserId
		{
			get
			{
				var claim = (ClaimsIdentity)User?.Identity;
				var userId = claim?.Claims?.Where(x => x.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
				return userId != null ?
					int.Parse(userId)
					: (int?)null;
			}
		}

		public string UserEmail
		{
			get
			{
				var claim = (ClaimsIdentity)User?.Identity;
				var userEmail = claim?.Claims?.Where(x => x.Type == ClaimTypes.Email)?.FirstOrDefault()?.Value;
				return userEmail;
			}
		}
		public BaseApiController()
		{
			_authService = Global.Instance.AuthService;
		}

		/// <summary>
		/// Состоит ли пользователь в одной из ролей
		/// </summary>
		/// <param name="roles">Список ролей</param>
		/// <returns></returns>
		protected async Task<bool> IsInRole(params string[] roles)
		{
			return (await _authService.IsInRole(UserId.GetValueOrDefault(), roles)).IsSuccess;
		}

		/// <summary>
		/// Авторизован ли пользователь
		/// </summary>
		/// <returns></returns>
		protected bool IsAuthorized()
		{
			return UserId.HasValue;
		}
	}
}
