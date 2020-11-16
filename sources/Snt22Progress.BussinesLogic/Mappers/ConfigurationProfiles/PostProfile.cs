using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Snt22Progress.Contracts.Models.Base;
using Snt22Progress.Contracts.Models.Posts;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.BussinesLogic.Mappers.ConfigurationProfiles
{
	public class PostProfile : Profile
	{
		public PostProfile()
		{
			CreateMap<PostView, PostGetDto>()
				.ForMember(x => x.Creator, opt => opt.MapFrom(src => MapperHelper.InitCreator(src.Creator_Id, src.Creator_FName, src.Creator_SName, src.Creator_MName)))
				.ForMember(x => x.Editor, opt => opt.MapFrom(src => MapperHelper.InitCreator(src.Editor_Id, src.Editor_FName, src.Editor_SName, src.Editor_MName)));

			CreateMap<PostCreateDto, Post>();
		}	
	}
}
