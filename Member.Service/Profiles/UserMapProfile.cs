using System;
using AutoMapper;
using Member.Common.Model;
using Member.DOMAIN.Entity;


namespace Member.Service.Profiles
{
	public class UserMapProfile : Profile
	{
		public UserMapProfile()
		{
			CreateMap<MemberRegisterModel, MemberEntity>().ReverseMap();
        }
	}
}

