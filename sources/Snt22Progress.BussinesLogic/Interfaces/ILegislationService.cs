using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Legislations;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс сервиса для работы с законодательством
	/// </summary>
	public interface ILegislationService
	{
		/// <summary>
		/// Получить список законодательства
		/// </summary>
		/// <returns></returns>
		Task<ResultResponse<IEnumerable<LegislationDto>>> GetListAsync();

		/// <summary>
		/// Добавить новое законодательство
		/// </summary>
		/// <param name="createDto">Новое законодательство</param>
		/// <param name="creatorId">Идентификатор добавляющего пользователя</param>
		/// <returns></returns>
		Task<ResultResponse<LegislationDto>> AddAsync(LegislationCreateDto createDto, int creatorId);

		/// <summary>
		/// Удалить законодательство
		/// </summary>
		/// <param name="legislationId">Идентификатор законодательства</param>
		/// <returns></returns>
		Task<ResultResponse> DeleteAsync(int legislationId);
	}
}
