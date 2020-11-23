using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Questions;

namespace Snt22Progress.Web.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/questions")]
	public class QuestionController : BaseApiController
	{
		private readonly IQuestionsService _questionsService;

		public QuestionController(IQuestionsService questionsService)
		{
			_questionsService = questionsService;
		}

		/// <summary>
		/// Получить список опросов
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet("list")]
		public async Task<ResultResponse<IEnumerable<QuestionDto>>> GetQuestionList()
		{
			if (IsAuthorized())
			{
				return await _questionsService.GetQuestionListAsync(UserId.Value);
			}
			return ResultResponse<IEnumerable<QuestionDto>>.GetBadResponse(BussinesLogic.Models.StatusCode.Unauthorized);
		}

		/// <summary>
		/// Добавить опрос
		/// </summary>
		/// <param name="dto">Новый опрос</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("add")]
		public async Task<ResultResponse<QuestionDto>> AddQuestion(QuestionCreateDto dto)
		{
			if (await IsInRole(Roles.Admin, Roles.Moderator))
			{
				return await _questionsService.AddQuestionAsync(dto, UserId.Value);
			}
			return ResultResponse<QuestionDto>.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Удалить опрос
		/// </summary>
		/// <param name="id">Идентификатор опроса</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("{id}/delete")]
		public async Task<ResultResponse> DeleteQuestion([FromRoute] int id)
		{
			if (await IsInRole(Roles.Admin, Roles.Moderator))
			{
				return await _questionsService.RemoveQuestionAsync(id);
			}
			return ResultResponse.GetBadResponse(BussinesLogic.Models.StatusCode.Forbidden);
		}

		/// <summary>
		/// Оставить голос в опросе
		/// </summary>
		/// <param name="id">Идентификатор опроса</param>
		/// <param name="voteId">Идентификатор голоса в опросе</param>
		/// <returns></returns>
		[Authorize]
		[HttpPost("{id}/vote")]
		public async Task<ResultResponse<QuestionDto>> VoteOnQuestion([FromRoute] int id, [FromBody] int voteId)
		{
			if (IsAuthorized())
			{
				return await _questionsService.VoteAsync(id, voteId, UserId.Value);
			}
			return ResultResponse<QuestionDto>.GetBadResponse(BussinesLogic.Models.StatusCode.Unauthorized);
		}
	}
}
