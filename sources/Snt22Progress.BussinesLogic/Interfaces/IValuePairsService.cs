using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Models;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс сервиса для работы с константами в БД
	/// </summary>
	public interface IValuePairsService
	{
		Task<ResultResponse<string>> GetValueAsync(string key);
	}
}
