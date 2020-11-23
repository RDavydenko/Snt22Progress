using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Questions;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс сервиса для работы с опросами
	/// </summary>
	public interface IQuestionsService
	{
		/// <summary>
		/// Получить список опросов
		/// </summary>
		/// <param name="userId">Идентификатор пользователя, чтобы определять проголосовал уже или нет</param>
		/// <returns></returns>
		Task<ResultResponse<IEnumerable<QuestionDto>>> GetQuestionListAsync(int userId);

		/// <summary>
		/// Добавить опрос
		/// </summary>
		/// <param name="dto">Опрос</param>
		/// <param name="userId">Идентификатор добавляющего пользователя</param>
		/// <returns></returns>
		Task<ResultResponse<QuestionDto>> AddQuestionAsync(QuestionCreateDto dto, int userId);

		/// <summary>
		/// Удалить опрос
		/// </summary>
		/// <param name="id">Идентификатор опроса</param>
		/// <returns></returns>
		Task<ResultResponse> RemoveQuestionAsync(int id);

		/// <summary>
		/// Проголосовать в опросе
		/// </summary>
		/// <param name="id">Идентификатор опроса</param>
		/// <param name="voteId">Идентификатор выбора в опросе</param>
		/// <param name="voterId">Идентификатор голосующего пользователя</param>
		/// <returns></returns>
		Task<ResultResponse<QuestionDto>> VoteAsync(int id, int voteId, int voterId);
	}
}
