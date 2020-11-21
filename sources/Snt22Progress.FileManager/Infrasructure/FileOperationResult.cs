using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.FileManager.Infrasructure
{
	/// <summary>
	/// Класс, описывающий результат операции с файлом
	/// </summary>
	public class FileOperationResult
	{
		/// <summary>
		/// Успешна ли операция
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Путь к файлу
		/// </summary>
		public string FilePath { get; set; }

		/// <summary>
		/// Описание ошибки (если возникла)
		/// </summary>
		public string ErrorDescription { get; set; }
	}
}
