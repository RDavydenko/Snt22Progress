using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.BussinesLogic.Models;

namespace Snt22Progress.BussinesLogic.Services
{
	public class ConfigurationService
	{
		public UploadedFilesSettings UploadedFilesSettings { get; }

		public WebAppSettings WebAppSettings { get; }

		public ConfigurationService(UploadedFilesSettings uploadedFilesSettings, WebAppSettings webAppSettings)
		{
			UploadedFilesSettings = uploadedFilesSettings;
			WebAppSettings = webAppSettings;
		}
	}
}
