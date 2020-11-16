using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.Contracts.Models;
using Snt22Progress.Contracts.Models.Users;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс сервиса для работы с пользователями
	/// </summary>
	public interface IUsersService
	{
		/// <summary>
		/// Зарегистрировать пользователя
		/// </summary>
		/// <param name="dto">Пользователь</param>
		/// <returns></returns>
		Task<ResultResponse> RegisterAsync(UserRegisterDto dto);

		/// <summary>
		/// Редактировать пользователя
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <param name="dto">Пользователь</param>
		/// <returns>Отредактированный пользователь</returns>
		Task<ResultResponse<UserDto>> EditAsync(int userId, UserEditDto dto);

		/// <summary>
		/// Получить информацию о пользователе
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <returns>Пользователь</returns>
		Task<ResultResponse<UserDto>> GetAsync(int userId);

		/// <summary>
		/// Сменить пароль
		/// </summary>
		/// <param name="userId">Идентификатор пользователя</param>
		/// <param name="dto">Новый пароль и старый пароли</param>
		/// <returns></returns>
		Task<ResultResponse> ChangePasswordAsync(int userId, ChangePasswordDto dto);
	}
}
