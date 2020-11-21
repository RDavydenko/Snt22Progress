using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.FileManager.Infrasructure
{
	/// <summary>
	/// ДТО файла
	/// </summary>
	public class FileDto
	{
		public string FileName { get; set; }

		public byte[] Bytes { get; set; }

		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="fileName">Название файла, включая расширение</param>
		/// <param name="bytes">Массив байтов файла</param>
		public FileDto(string fileName, byte[] bytes)
		{
			FileName = fileName;
			Bytes = bytes;
		}
	}
}
