using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.Logging
{
	/// <summary>
	/// Интерфейс логгера
	/// </summary>
	public interface IProgressLogger
	{
		/// <summary>
		/// Запись лога уровня "Информация"
		/// </summary>
		/// <param name="message">Сообщение</param>
		/// <param name="src">Объект логгирования</param>
		/// <param name="className">Имя класса</param>
		/// <param name="methodName">Имя метода</param>
		public void Information(string message, object src = null, string className = null, string methodName = null);

		/// <summary>
		/// Запись лога уровня "Опасность"
		/// </summary>
		/// <param name="message">Сообщение</param>
		/// <param name="src">Объект логгирования</param>
		/// <param name="className">Имя класса</param>
		/// <param name="methodName">Имя метода</param>
		public void Warning(string message, object src = null, string className = null, string methodName = null);

		/// <summary>
		/// Запись лога уровня "Опасность"
		/// </summary>
		/// <param name="ex">Исключение</param>
		/// <param name="src">Объект логгирования</param>
		/// <param name="className">Имя класса</param>
		/// <param name="methodName">Имя метода</param>
		public void Warning(Exception ex, object src = null, string className = null, string methodName = null);

		/// <summary>
		/// Запись лога уровня "Ошибка"
		/// </summary>
		/// <param name="message">Сообщение</param>
		/// <param name="src">Объект логгирования</param>
		/// <param name="className">Имя класса</param>
		/// <param name="methodName">Имя метода</param>
		public void Error(string message, object src = null, string className = null, string methodName = null);

		/// <summary>
		/// Запись лога уровня "Ошибка"
		/// </summary>
		/// <param name="ex">Исключение</param>
		/// <param name="src">Объект логгирования</param>
		/// <param name="className">Имя класса</param>
		/// <param name="methodName">Имя метода</param>
		public void Error(Exception ex, object src = null, string className = null, string methodName = null);

		/// <summary>
		/// Запись лога уровня "Фатальная ошибка"
		/// </summary>
		/// <param name="message">Сообщение</param>
		/// <param name="src">Объект логгирования</param>
		/// <param name="className">Имя класса</param>
		/// <param name="methodName">Имя метода</param>
		public void Fatal(string message, object src = null, string className = null, string methodName = null);

		/// <summary>
		/// Запись лога уровня "Фатальная ошибка"
		/// </summary>
		/// <param name="ex">Исключение</param>
		/// <param name="src">Объект логгирования</param>
		/// <param name="className">Имя класса</param>
		/// <param name="methodName">Имя метода</param>
		public void Fatal(Exception ex, object src = null, string className = null, string methodName = null);
	}
}
