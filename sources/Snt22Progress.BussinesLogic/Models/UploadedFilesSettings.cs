using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.BussinesLogic.Models
{
	public class UploadedFilesSettings
	{
		public string DocumentsFilesFolderRelativePath { get; }
		public string AdvertisementFilesFolderRelativePath { get; }
		public string DebtorFilesFolderRelativePath { get; }

		public UploadedFilesSettings(string documentsFilesFolderRelativePath,
			string advertisementFilesFolderRelativePath,
			string debtorFilesFolderRelativePath)
		{
			DocumentsFilesFolderRelativePath = documentsFilesFolderRelativePath;
			AdvertisementFilesFolderRelativePath = advertisementFilesFolderRelativePath;
			DebtorFilesFolderRelativePath = debtorFilesFolderRelativePath;
		}
	}
}
