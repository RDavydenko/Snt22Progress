using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;
using Snt22Progress.BussinesLogic.Models.DataContracts;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс сервиса аутентификации
	/// </summary>
	public interface IAuthService
	{
		Task<ResultResponse<JwtAuthorizationDto>> LoginAsync(string login, string password);

		Task<ResultResponse> LogoutAsync();
	}
}
