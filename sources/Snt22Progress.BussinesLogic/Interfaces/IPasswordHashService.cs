using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snt22Progress.BussinesLogic.Interfaces
{
	/// <summary>
	/// Интерфейс сервиса, предоставляющего хеширование паролей
	/// </summary>
	public interface IPasswordHashService
	{
		/// <summary>
		/// Хеширование пароля с солью
		/// </summary>
		/// <param name="password">Пароль</param>
		/// <param name="salt">Соль</param>
		/// <returns>Хеш</returns>
		Task<string> GetPasswordHashWithSalt(string password, string salt);
	}
}
