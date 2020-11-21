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
		Task<ResultResponse<IEnumerable<AdvertisementDto>>> GetAdvertisementListAsync();
		Task<ResultResponse<AdvertisementDto>> GetAdvertisementAsync(int id);
		Task<ResultResponse<AdvertisementDto>> AddAdvertisementAsync(AdvertisementCreateDto dto, int creatorId);
		Task<ResultResponse<AdvertisementDto>> EditAdvertisementAsync(int id, AdvertisementEditDto dto, int editorId);
		Task<ResultResponse> RemoveAdvertisementAsync(int id);
	}
}
