using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Snt22Progress.Contracts.Models.Documents;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.BussinesLogic.Mappers.ConfigurationProfiles
{
	public class DocumentProfile : Profile
	{
		public DocumentProfile()
		{
			CreateMap<DocumentView, DocumentDto>()
				.ForMember(x => x.IsActive, opt => opt.MapFrom(m => m.is_active))
				.ForMember(x => x.NativeName, opt => opt.MapFrom(m => m.native_name))
				.ForMember(x => x.Creator, opt => opt.MapFrom(m => MapperHelper.InitCreator(m.creator_id, m.creator_fname, m.creator_lname, m.creator_mname)))
				.ForMember(x => x.Editor, opt => opt.MapFrom(m => MapperHelper.InitEditor(m.editor_id, m.editor_fname, m.editor_sname, m.editor_mname)));
		}
	}
}
