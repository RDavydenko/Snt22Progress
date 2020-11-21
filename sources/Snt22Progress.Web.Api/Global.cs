using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snt22Progress.BussinesLogic.Interfaces;

namespace Snt22Progress.Web.Api
{
	/// <summary>
	/// Класс для передачи зависимостей. Реализует паттерн синглтон
	/// </summary>
	public class Global
	{
		public static Global Instance { get; private set;  }

		public IAuthService AuthService { get; private set; }

		private Global() { }

		public static void Initialize(IAuthService authService)
		{
			if (Instance == null)
			{
				Instance = new Global();
				Instance.AuthService = authService;
			}
		}
	}
}
