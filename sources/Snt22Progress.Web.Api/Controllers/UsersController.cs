using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models;
using Snt22Progress.Contracts.Models.Users;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/users")]
	public class UsersController : BaseApiController
	{
		private readonly IUsersService _usersService;

		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		/// <summary>
		/// Зарегистрировать нового пользователя
		/// </summary>
		/// <returns></returns>
		[HttpPost("register")]
		public async Task<ResultResponse> Register(UserRegisterDto dto)
		{
			if (dto == null || !ModelState.IsValid)
			{
				return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return await _usersService.RegisterAsync(dto);
		}

		/// <summary>
		/// Редактировать пользователя
		/// </summary>
		/// <param name="dto">Информация о пользователе</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("edit")]
		public async Task<ResultResponse<UserDto>> Edit(UserEditDto dto)
		{
			if (dto == null || !ModelState.IsValid)
			{
				return ResultResponse<UserDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return await _usersService.EditAsync(UserId.Value, dto);
		}

		[Authorize]
		[HttpPost("change-password")]
		public async Task<ResultResponse> ChangePassword(ChangePasswordDto dto)
		{
			if (dto == null || !ModelState.IsValid)
			{
				return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return await _usersService.ChangePasswordAsync(UserId.Value, dto);
		}
	}
}
