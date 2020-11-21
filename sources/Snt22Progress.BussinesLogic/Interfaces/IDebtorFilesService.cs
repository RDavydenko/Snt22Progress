using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.DebtorFiles;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс для работы с файлами о должниках
	/// </summary>
	public interface IDebtorFilesService
	{
		/// <summary>
		/// Получить список файлов о должниках
		/// </summary>
		/// <returns></returns>
		Task<ResultResponse<IEnumerable<DebtorFileDto>>> GetListAsync();

		/// <summary>
		/// Получить информацию о файле о должниках
		/// </summary>
		/// <param name="id">Идентификатор файла о должниках</param>
		/// <returns></returns>
		Task<ResultResponse<DebtorFileDto>> GetDebtorFileAsync(int id);

		/// <summary>
		/// Добавить файл о должниках
		/// </summary>
		/// <param name="dto">Добавляемый файл</param>
		/// <param name="uploaderId">Идентификатор загружающего пользователя</param>
		/// <returns></returns>
		Task<ResultResponse<DebtorFileDto>> AddDebtorFileAsync(DebtorFileCreateDto dto, int uploaderId);

		/// <summary>
		/// Удалить файл о должниках
		/// </summary>
		/// <param name="id">Идентификатор файла о должниках</param>
		/// <returns></returns>
		Task<ResultResponse> RemoveDebtorFileAsync(int id);
	}
}
