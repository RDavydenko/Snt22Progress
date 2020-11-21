using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Snt22Progress.Contracts.Models.Legislations;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.BussinesLogic.Mappers.ConfigurationProfiles
{
	public class LegislationProfile : Profile
	{
		public LegislationProfile()
		{
			CreateMap<Legislation, LegislationDto>()
				.ForMember(x => x.CreatorId, opt => opt.MapFrom(m => m.creator_id));

			CreateMap<LegislationCreateDto, Legislation>();
		}
	}
}
