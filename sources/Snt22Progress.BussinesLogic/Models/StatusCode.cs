using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.BussinesLogic.Models
{
	/// <summary>
	/// Статус коды ответа
	/// </summary>
	public enum StatusCode
	{
		/// <summary>
		/// Успешно
		/// </summary>
		OK = 200,

		/// <summary>
		/// Неверный запрос
		/// </summary>
		BadRequest = 400,

		/// <summary>
		/// Не авторизован
		/// </summary>
		Unauthorized = 401,

		/// <summary>
		/// Запрещено
		/// </summary>
		Forbidden = 403,

		/// <summary>
		/// Не найдено
		/// </summary>
		NotFound = 404,

		/// <summary>
		/// Внутренняя ошибка сервера
		/// </summary>
		InternalServerError = 500
	}
}
