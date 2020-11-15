using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.BussinesLogic.Models
{
	public class ResultResponse<TResult> 
		where TResult : class
	{
		/// <summary>
		/// Успешна операция или нет?
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Статус код
		/// </summary>
		public StatusCode StatusCode { get; set; }

		/// <summary>
		/// Результат (если есть)
		/// </summary>
		public TResult Result { get; set; }

		/// <summary>
		/// Описание ошибки (если возникла)
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="result">Результат ответа</param>
		/// <param name="isSuccess">Успешна ли операция</param>
		/// <param name="statusCode">Статус код ответа</param>
		/// <param name="description">Описание ошибки</param>
		public ResultResponse(TResult result = null, bool isSuccess = true, StatusCode statusCode = StatusCode.OK, string description = null)
		{
			Result = result;
			IsSuccess = isSuccess;
			StatusCode = statusCode;
			Description = description;
		}

		/// <summary>
		/// Возвращает успешный запрос
		/// </summary>
		/// <param name="result">Результат ответа</param>
		/// <returns></returns>
		public static ResultResponse<TResult> GetSuccessResponse(TResult result = null)
		{
			return new ResultResponse<TResult>(result);
		}

		/// <summary>
		/// Возвращает неуспешный запрос
		/// </summary>
		/// <param name="statusCode">Статус код ответа</param>
		/// <param name="description">Описание ошибки</param>
		/// <param name="result">Результат ответа</param>
		/// <returns></returns>
		public static ResultResponse<TResult> GetBadResponse(StatusCode statusCode, string description = null, TResult result = null)
		{
			return new ResultResponse<TResult>(result, false, statusCode, description);
		}

		/// <summary>
		/// Возвращает неуспешный запрос с кодом ошибки 500
		/// </summary>
		/// <param name="description">Описание ошибки</param>
		/// <param name="result">Результат ответа</param>
		/// <returns></returns>
		public static ResultResponse<TResult> GetInternalServerErrorResponse(string description = null, TResult result = null)
		{
			return new ResultResponse<TResult>(result, false, StatusCode.InternalServerError, description);
		}
	}

	/// <summary>
	/// Не Generic класс результата ответа от сервера
	/// </summary>
	public class ResultResponse
	{
		/// <summary>
		/// Успешна операция или нет?
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Статус код
		/// </summary>
		public StatusCode StatusCode { get; set; }

		/// <summary>
		/// Результат (если есть)
		/// </summary>
		public object Result { get; set; }

		/// <summary>
		/// Описание ошибки (если возникла)
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="result">Результат ответа</param>
		/// <param name="isSuccess">Успешна ли операция</param>
		/// <param name="statusCode">Статус код ответа</param>
		/// <param name="description">Описание ошибки</param>
		public ResultResponse(object result = null, bool isSuccess = true, StatusCode statusCode = StatusCode.OK, string description = null)
		{
			Result = result;
			IsSuccess = isSuccess;
			StatusCode = statusCode;
			Description = description;
		}

		/// <summary>
		/// Возвращает успешный запрос
		/// </summary>
		/// <param name="result">Результат ответа</param>
		/// <returns></returns>
		public static ResultResponse GetSuccessResponse(object result = null)
		{
			return new ResultResponse(result);
		}

		/// <summary>
		/// Возвращает неуспешный запрос
		/// </summary>
		/// <param name="statusCode">Статус код ответа</param>
		/// <param name="description">Описание ошибки</param>
		/// <param name="result">Результат ответа</param>
		/// <returns></returns>
		public static ResultResponse GetBadResponse(StatusCode statusCode, string description = null, object result = null)
		{
			return new ResultResponse(result, false, statusCode, description);
		}

		/// <summary>
		/// Возвращает неуспешный запрос с кодом ошибки 500
		/// </summary>
		/// <param name="description">Описание ошибки</param>
		/// <param name="result">Результат ответа</param>
		/// <returns></returns>
		public static ResultResponse GetInternalServerErrorResponse(string description = null, object result = null)
		{
			return new ResultResponse(result, false, StatusCode.InternalServerError, description);
		}
	}
}
