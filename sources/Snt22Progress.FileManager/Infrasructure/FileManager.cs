using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snt22Progress.Logging;

namespace Snt22Progress.FileManager.Infrasructure
{
	/// <summary>
	/// Класс для работы с файлами
	/// </summary>
	public class FileManager
	{
		private readonly string _baseAddress;
		private readonly string _folder;
		private readonly IProgressLogger _progressLogger;

		private string _absolutePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _folder);

		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="baseAddress">Базовый адрес</param>
		/// <param name="folder">Имя папки, с которой будем работать</param>
		/// <param name="progressLogger">Логгер (опционально)</param>
		public FileManager(string baseAddress, string folder, IProgressLogger progressLogger = null)
		{
			_baseAddress = baseAddress;
			_folder = folder;
			_progressLogger = progressLogger;
		}

		/// <summary>
		/// Загрузить файл
		/// </summary>
		/// <param name="file">Файл</param>
		/// <param name="allowedFormats">Разрешенные форматы при загрузке файла. Например, jpg, png</param>
		/// <returns>Результат операции по загрузке файла</returns>
		public async Task<FileOperationResult> UploadFileAsync(FileDto file, string[] allowedFormats = null)
		{
			try
			{
				file.FileName = file.FileName.Replace(' ', '_'); // Заменяем все пробелы в названии на нижнее подчеркивание

				if (file.FileName.Contains("\\") || file.FileName.Contains("/"))
				{
					return new FileOperationResult { IsSuccess = false, ErrorDescription = "Названия файлов не должны содержать слешей (/ и \\)" };
				}
				var splitted = file.FileName.Split('.');
				var fileExtension = splitted.Length > 0 ? splitted[splitted.Length - 1] : null; // Расширение файла
				if (allowedFormats != null &&
					allowedFormats.Any(x => x.ToLower() == fileExtension.ToLower()) == false)
				{
					return new FileOperationResult { IsSuccess = false, ErrorDescription = $"Файл недопустимого расширения. Доступные расширения файлов: {string.Join(", ", allowedFormats)}" };
				}

				if (Directory.Exists(_absolutePath) == false)
				{
					Directory.CreateDirectory(_absolutePath);
				}

				var hypotheticalFilePath = Path.Combine(_absolutePath, file.FileName);
				string realUniqueFilePath; // Физический путь к файлу (Включает кучу папок, букву диска и т.д.)				
				string realFileName;
				if (File.Exists(hypotheticalFilePath))
				{
					realFileName = DateTime.Now.ToString("yyyy-MM-dd_mm-ss-fff") + "_" + file.FileName;
					realUniqueFilePath = Path.Combine(_absolutePath, realFileName);
				}
				else
				{
					realFileName = file.FileName;
					realUniqueFilePath = hypotheticalFilePath;
				}
				string serverFilePath = Path.Combine(_baseAddress, _folder, realFileName);  // Путь к файлу через сервер (<протокол><домен><путь>)

				await File.WriteAllBytesAsync(realUniqueFilePath, file.Bytes);

				return new FileOperationResult { IsSuccess = true, FilePath = serverFilePath };
			}
			catch (Exception ex)
			{
				_progressLogger?.Error(ex, new { file }, GetType().Name, nameof(UploadFileAsync));
				return new FileOperationResult { IsSuccess = false, ErrorDescription = "Произошла неизвестная ошибка при загрузке файла" };
			}
		}
	}
}