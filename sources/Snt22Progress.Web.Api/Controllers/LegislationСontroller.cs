using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Legislations;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/legislation")]
	public class LegislationСontroller : BaseApiController
	{
		private readonly ILegislationService _legislationService;

		public LegislationСontroller(ILegislationService legislationService)
		{
			_legislationService = legislationService;
		}

		/// <summary>
		/// Получить список ссылок законодательства
		/// </summary>
		/// <returns></returns>
		[HttpGet("list")]
		public async Task<ResultResponse<IEnumerable<LegislationDto>>> GetListLegislations()
		{
			return await _legislationService.GetListAsync();
		}

		/// <summary>
		/// Добавить ссылку законодательства
		/// </summary>
		/// <param name="createDto">Добавляемое законодательство</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("add")]
		public async Task<ResultResponse<LegislationDto>> AddLegislation([FromBody] LegislationCreateDto createDto)
		{
			if (await IsInRole(Roles.Admin))
			{
				return await _legislationService.AddAsync(createDto, UserId.Value);
			}
			return ResultResponse<LegislationDto>.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Удалить ссылку законодательства
		/// </summary>
		/// <param name="legislationId">Идентификатор законодательства</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("{legislationId}/delete")]
		public async Task<ResultResponse> DeleteMember([FromRoute] int legislationId)
		{
			if (await IsInRole(Roles.Admin))
			{
				return await _legislationService.DeleteAsync(legislationId);
			}
			return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}
	}
}
