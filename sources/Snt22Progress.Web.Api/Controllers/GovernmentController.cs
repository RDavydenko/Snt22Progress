using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Users;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/government")]
	public class GovernmentController : BaseApiController
	{
		private readonly IGovernmentService _governmentService;

		public GovernmentController(IGovernmentService governmentService)
		{
			_governmentService = governmentService;
		}

		/// <summary>
		/// Получить список участников правления
		/// </summary>
		/// <returns></returns>
		[HttpGet("list")]
		public async Task<ResultResponse<IEnumerable<UserDto>>> GetListMembers()
		{
			return await _governmentService.GetMembersAsync();
		}

		/// <summary>
		/// Добавить участника правления
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("add")]
		public async Task<ResultResponse> AddMember([FromBody] int userId)
		{
			if (await IsInRole(Roles.Admin))
			{
				return await _governmentService.AddMemberAsync(userId);
			}
			return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Удалить участника правления
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("delete")]
		public async Task<ResultResponse> DeleteMember([FromBody] int userId)
		{
			if (await IsInRole(Roles.Admin))
			{
				return await _governmentService.DeleteMemberAsync(userId);
			}
			return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}
	}

}