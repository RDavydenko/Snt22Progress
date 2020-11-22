using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Snt22Progress.Contracts.Models.Advertisements;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.BussinesLogic.Mappers.ConfigurationProfiles
{
	public class AdvertisementProfile : Profile
	{
		public AdvertisementProfile()
		{
			CreateMap<AdvertisementView, AdvertisementDto>()
				.ForMember(x => x.Creator, opt => opt.MapFrom(m => MapperHelper.InitCreator(m.creator_id, m.creator_fname, m.creator_lname, m.creator_mname)))
				.ForMember(x => x.Image, opt => opt.MapFrom(m => GetAdvertisementFileDto(m)))
				.ForMember(x => x.IsPrivatizated, opt => opt.MapFrom(m => m.is_privatizated));

			CreateMap<AdvertisementCreateDto, Advertisement>()
				.ForMember(x => x.is_privatizated, opt => opt.MapFrom(m => m.IsPrivatizated));
		}

		private static AdvertisementFileDto GetAdvertisementFileDto(AdvertisementView m)
		{
			if (m.image_file_id.HasValue == false)
			{
				return null;
			}
			else
			{
				return new AdvertisementFileDto
				{
					Id = m.image_file_id.Value,
					Length = m.image_length.Value,
					NativeName = m.image_native_name,
					Path = m.image_path,
					Uploaded = m.image_uploaded.Value,
					Uploader = MapperHelper.InitUploader(m.image_uploader_id, m.image_uploader_fname, m.image_uploader_lname, m.image_uploader_mname)
				};
			}
		}
	}
}
