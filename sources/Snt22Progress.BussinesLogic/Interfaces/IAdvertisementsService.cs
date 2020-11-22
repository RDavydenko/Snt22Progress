using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Advertisements;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс сервиса для работы с объявлениями о продаже участков
	/// </summary>
	public interface IAdvertisementsService
	{
		/// <summary>
		/// Получить список объявлений о продаже участков
		/// </summary>
		/// <returns></returns>
		Task<ResultResponse<IEnumerable<AdvertisementDto>>> GetAdvertisementListAsync();

		/// <summary>
		/// Получить объявление о продаже участков по идентификатору
		/// </summary>
		/// <param name="id">Идентификатор</param>
		/// <returns></returns>
		Task<ResultResponse<AdvertisementDto>> GetAdvertisementAsync(int id);

		/// <summary>
		/// Добавить объявление о продаже участка
		/// </summary>
		/// <param name="dto">ДТО объявления о продаже участка</param>
		/// <param name="creatorId">Идентификатор создателя объявления</param>
		/// <returns></returns>
		Task<ResultResponse<AdvertisementDto>> AddAdvertisementAsync(AdvertisementCreateDto dto, int creatorId);

		/// <summary>
		/// Редактировать объявление о продаже участка
		/// </summary>
		/// <param name="id">Идентификатор редактируемого объявления</param>
		/// <param name="dto">ДТО для редактирования объявления</param>
		/// <returns></returns>
		Task<ResultResponse<AdvertisementDto>> EditAdvertisementAsync(int id, AdvertisementEditDto dto);

		/// <summary>
		/// Удалить объявление о продаже участка
		/// </summary>
		/// <param name="id">Идентификатор объявления</param>
		/// <returns></returns>
		Task<ResultResponse> RemoveAdvertisementAsync(int id);
	}
}
