using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.BussinesLogic.Models.DataContracts
{
	/// <summary>
	/// ДТО JWT авторизации
	/// </summary>
	public class JwtAuthorizationDto
	{
		/// <summary>
		/// Используется ли ограничение по времени жизни токена
		/// </summary>
		public bool UsedLifetime { get; set; }

		/// <summary>
		/// Дата окончания действия токена
		/// </summary>
		public DateTime? EndLifetime { get; set; }

		/// <summary>
		/// Токен
		/// </summary>
		public string Token { get; set; }
	}
}
