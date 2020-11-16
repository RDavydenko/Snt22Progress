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
				.ForMember(x => x.Creator, opt => opt.MapFrom(src => MapperHelper.InitCreator(src.creator_id, src.creator_fname, src.creator_sname, src.creator_mname)))
				.ForMember(x => x.Editor, opt => opt.MapFrom(src => MapperHelper.InitEditor(src.editor_id, src.editor_fname, src.editor_sname, src.editor_mname)));

			CreateMap<PostCreateDto, Post>();
		}	
	}
}
