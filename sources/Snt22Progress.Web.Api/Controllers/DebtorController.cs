using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.DebtorFiles;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/debtors")]
	public class DebtorController : BaseApiController
	{
		private readonly IDebtorFilesService _debtorFilesService;

		public DebtorController(IDebtorFilesService debtorFilesService)
		{
			_debtorFilesService = debtorFilesService;
		}

		/// <summary>
		/// Получить список файлов о должниках
		/// </summary>
		/// <returns></returns>
		[HttpGet("list")]
		public async Task<ResultResponse<IEnumerable<DebtorFileDto>>> GetDebtorFiles()
		{
			return await _debtorFilesService.GetListAsync();
		}

		/// <summary>
		/// Получить информацию по файлу о должниках
		/// </summary>
		/// <param name="id">Идентификатор файла о должниках</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ResultResponse<DebtorFileDto>> GetDebtorFileById([FromRoute] int id)
		{
			return await _debtorFilesService.GetDebtorFileAsync(id);
		}

		/// <summary>
		/// Добавить файл о должниках
		/// </summary>
		/// <param name="dto">Файл о должниках</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("add")]
		public async Task<ResultResponse<DebtorFileDto>> AddDebtorFile([FromForm] DebtorFileCreateDto dto)
		{
			if (await IsInRole(Roles.Admin))
			{
				if (dto != null && ModelState.IsValid)
				{
					if (dto.File == null)
					{
						if (Request?.HasFormContentType == true && Request?.Form?.Files?.Count != 0)
						{
							dto.File = Request.Form.Files[0];
						}
					}
					if (dto.File == null)
					{
						return ResultResponse<DebtorFileDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest, "Необходимо передать файл");
					}
					else
					{
						return await _debtorFilesService.AddDebtorFileAsync(dto, UserId.Value);
					}
				}
				return ResultResponse<DebtorFileDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return ResultResponse<DebtorFileDto>.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Удалить файл о должниках
		/// </summary>
		/// <param name="id">Идентификатор файла о должниках</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("{id}/delete")]
		public async Task<ResultResponse> RemoveDebtorFile([FromRoute] int id)
		{
			if (await IsInRole(Roles.Admin))
			{
				return await _debtorFilesService.RemoveDebtorFileAsync(id);
			}
			return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}
	}
}
