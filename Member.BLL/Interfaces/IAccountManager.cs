using System;
using Member.Common.Model;
using Member.DOMAIN.Entity;

namespace Member.BLL.Interfaces
{
	public interface IAccountManager
	{
		Task<ResultModel<string>> RegisterUserAsync(MemberRegisterModel member);
        Task<ResultModel<string>> LoginUserAsync(MemberLoginModel member);
        Task<ResultModel<MemberEntity>> GetInfo(string token);
		Task<MemberEntity> UserUpdate(MemberUpdateModel memberUpdate, string token);
		Task<bool> DeleteUser(string token);
    }
	
}

