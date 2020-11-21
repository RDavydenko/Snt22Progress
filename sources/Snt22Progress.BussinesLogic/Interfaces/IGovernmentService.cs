using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models.Users;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс для работы с правлением
	/// </summary>
	public interface IGovernmentService
	{
		/// <summary>
		/// Получить список пользователей-участников правления
		/// </summary>
		/// <returns></returns>
		Task<ResultResponse<IEnumerable<UserDto>>> GetMembersAsync();

		/// <summary>
		/// Добавить нового пользователя в правление
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <returns></returns>
		Task<ResultResponse> AddMemberAsync(int userId);

		/// <summary>
		/// Удалить пользователя из правления
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <returns></returns>
		Task<ResultResponse> DeleteMemberAsync(int userId);

	}
}
