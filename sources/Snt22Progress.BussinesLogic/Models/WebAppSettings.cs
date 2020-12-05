using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.BussinesLogic.Models
{
	/// <summary>
	/// Класс, хранящий настройки веб-приложения
	/// </summary>
	public class WebAppSettings
	{
		public string BaseAddress { get; }

		public WebAppSettings(string baseAddress)
		{
			BaseAddress = baseAddress;
		}
	}
}
