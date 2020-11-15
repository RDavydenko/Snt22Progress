using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.BussinesLogic.Models.DataContracts;
using Snt22Progress.BussinesLogic.Services;
using Snt22Progress.Contracts.Models.Auth;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[Route("api/auth")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}


		[HttpPost("login")]
		public async Task<ResultResponse<JwtAuthorizationDto>> Login([FromBody] LoginPasswordDto dto)
		{
			if (dto == null || ModelState.IsValid == false)
			{
				return ResultResponse<JwtAuthorizationDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return await _authService.LoginAsync(dto.Login, dto.Password);
		}
	}
}
