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
using Snt22Progress.Contracts.Models.Documents;

namespace Snt22Progress.Web.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/documents")]
	public class DocumentsController : BaseApiController
	{
		private readonly IDocumentsService _documentsService;

		public DocumentsController(IDocumentsService documentsService)
		{
			_documentsService = documentsService;
		}

		/// <summary>
		/// Получить список документов
		/// </summary>
		/// <returns></returns>
		[HttpGet("list")]
		public async Task<ResultResponse<IEnumerable<DocumentDto>>> GetDocuments()
		{
			return await _documentsService.GetDocumentsAsync();
		}

		/// <summary>
		/// Получить документ по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор документа</param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<ResultResponse<DocumentDto>> GetDocument([FromRoute] int id)
		{
			return await _documentsService.GetDocumentAsync(id);
		}

		/// <summary>
		/// Добавить документ
		/// </summary>
		/// <param name="dto">Документ</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("add")]
		public async Task<ResultResponse<DocumentDto>> AddDocument([FromForm] DocumentCreateDto dto)
		{
			if (await IsInRole(Roles.Admin, Roles.Moderator))
			{
				if (dto != null && ModelState.IsValid)
				{
					if (dto.File == null)
					{
						if (Request.HasFormContentType && Request?.Form?.Files?.Count != 0)
						{
							dto = new DocumentCreateDto { File = Request.Form.Files[0] };
						}
					}
					if (dto.File == null)
					{
						return ResultResponse<DocumentDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest, "Необходимо передать файл");
					}
					else
					{
						return await _documentsService.AddDocumentAsync(dto, UserId.Value);
					}
				}
				return ResultResponse<DocumentDto>.GetBadResponse(BussinesLogic.Models.StatusCode.BadRequest);
			}
			return ResultResponse<DocumentDto>.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Удалить документ
		/// </summary>
		/// <param name="id">Идентификатор документа</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("{id}/delete")]
		public async Task<ResultResponse> RemoveDocument([FromRoute] int id)
		{
			if (await IsInRole(Roles.Admin, Roles.Moderator))
			{
				return await _documentsService.RemoveDocumentAsync(id);
			}
			return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}
	}
}
