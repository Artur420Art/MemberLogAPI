using System;
using AutoMapper;
using Member.Common.Model;
using Member.DOMAIN.Entity;

namespace Member.Service.Profiles
{
	public class UpdateMapProfile : Profile
	{
		public UpdateMapProfile()
		{
			CreateMap<MemberUpdateModel, MemberEntity>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != ""));
        }
	}
}

