using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Snt22Progress.Contracts.Models.DebtorFiles;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.BussinesLogic.Mappers.ConfigurationProfiles
{
	public class DebtorFileProfile : Profile
	{
		public DebtorFileProfile()
		{
			CreateMap<DebtorFile, DebtorFileDto>()
				.ForMember(x => x.NativeName, opt => opt.MapFrom(m => m.native_name))
				.ForMember(x => x.UploaderId, opt => opt.MapFrom(m => m.uploader_id));
		}
	}
}
