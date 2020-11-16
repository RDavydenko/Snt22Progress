using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Snt22Progress.Contracts.Models.Users;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.BussinesLogic.Mappers.ConfigurationProfiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(x => x.AreaNumber, opt => opt.MapFrom(m => m.area_number))
				.ForMember(x => x.IsBanned, opt => opt.MapFrom(m => m.is_banned));

			CreateMap<UserRegisterDto, User>()
				.ForMember(x => x.area_number, opt => opt.MapFrom(m => m.AreaNumber))
				.ForMember(x => x.email, opt => opt.MapFrom(m => m.Email.ToLower()));
		}
	}
}
