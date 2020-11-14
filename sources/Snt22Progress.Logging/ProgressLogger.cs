using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Snt22Progress.Logging
{
	/// <summary>
	/// Реализация логгера
	/// </summary>
	public class ProgressLogger : IProgressLogger
	{
		protected readonly string _folderName;

		protected readonly ILogger _logger;

		public string DirectoryPath => Path.Combine(Directory.GetCurrentDirectory(), _folderName);

		public ProgressLogger(string folderName = "logs")
		{
			_folderName = folderName;

			var configuration = new LoggerConfiguration();
			configuration.WriteTo.File(
				path: DirectoryPath				
				);

			_logger = configuration.CreateLogger();
		}

		public void Information(string message, object src = null, string className = null, string methodName = null)
		{
			_logger.Information(message, src, className, methodName);
		}

		public void Warning(string message, object src = null, string className = null, string methodName = null)
		{
			_logger.Warning(message, src, className, methodName);
		}

		public void Warning(Exception ex, object src = null, string className = null, string methodName = null)
		{
			_logger.Warning(ex, ex.Message, src, className, methodName);
		}

		public void Error(string message, object src = null, string className = null, string methodName = null)
		{
			_logger.Error(message, src, className, methodName);
		}

		public void Error(Exception ex, object src = null, string className = null, string methodName = null)
		{
			_logger.Error(ex, ex.Message, src, className, methodName);
		}

		public void Fatal(string message, object src = null, string className = null, string methodName = null)
		{
			_logger.Fatal(message, src, className, methodName);
		}

		public void Fatal(Exception ex, object src = null, string className = null, string methodName = null)
		{
			_logger.Fatal(ex, ex.Message, src, className, methodName);
		}
	}
}
