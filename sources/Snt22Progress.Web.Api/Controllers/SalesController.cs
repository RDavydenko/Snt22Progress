using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Advertisements;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/sales")]
	public class SalesController : BaseApiController
	{
		private readonly IAdvertisementsService _advertisementsService;
		private readonly IValuePairsService _valuePairsService;

		public SalesController(IAdvertisementsService advertisementsService,
			IValuePairsService valuePairsService)
		{
			_advertisementsService = advertisementsService;
			_valuePairsService = valuePairsService;
		}

		/// <summary>
		/// Получить список объявлений о продаже участков
		/// </summary>
		/// <returns></returns>
		[HttpGet("list")]
		public async Task<ResultResponse<IEnumerable<AdvertisementDto>>> GetAdvertisements()
		{
			return await _advertisementsService.GetAdvertisementListAsync();
		}

		/// <summary>
		/// Получить информацию по объявлению о продаже участка
		/// </summary>
		/// <param name="id">Идентификатор объявления</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ResultResponse<AdvertisementDto>> GetAdvertisementById([FromRoute] int id)
		{
			return await _advertisementsService.GetAdvertisementAsync(id);
		}

		/// <summary>
		/// Добавить объявление о продаже участка
		/// </summary>
		/// <param name="dto">Объявление о продаже участка</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("add")]
		public async Task<ResultResponse<AdvertisementDto>> AddAdvertisement([FromForm] AdvertisementCreateDto dto)
		{
			if (await IsInRole(Roles.Admin, Roles.Moderator))
			{
				if (dto != null && ModelState.IsValid)
				{
					return await _advertisementsService.AddAdvertisementAsync(dto, UserId.Value);
				}
				return ResultResponse<AdvertisementDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return ResultResponse<AdvertisementDto>.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Редактировать объявление о продаже участка
		/// </summary>
		/// <param name="id">Идентификатор объявления о продаже участка</param>
		/// <param name="dto">Объявление о продаже участка</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("{id}/edit")]
		public async Task<ResultResponse<AdvertisementDto>> EditAdvertisement([FromRoute] int id, [FromBody] AdvertisementEditDto dto)
		{
			if (await IsInRole(Roles.Admin, Roles.Moderator))
			{
				if (dto != null && ModelState.IsValid)
				{
					return await _advertisementsService.EditAdvertisementAsync(id, dto);
				}
				return ResultResponse<AdvertisementDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return ResultResponse<AdvertisementDto>.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Удалить объявление о продаже участка
		/// </summary>
		/// <param name="id">Идентификатор объявления</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("{id}/delete")]
		public async Task<ResultResponse> RemoveDocument([FromRoute] int id)
		{
			if (await IsInRole(Roles.Admin, Roles.Moderator))
			{
				return await _advertisementsService.RemoveAdvertisementAsync(id);
			}
			return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Стандартная картинка
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet("get-default-image")]
		[ResponseCache(Duration = 3600)]
		public async Task<ResultResponse<string>> GetDefaultImage()
		{
			if (IsAuthorized())
			{
				return await _valuePairsService.GetValueAsync(ValuePairsKeys.DefaultImagePath);
			}
			return ResultResponse<string>.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}
	}
}
