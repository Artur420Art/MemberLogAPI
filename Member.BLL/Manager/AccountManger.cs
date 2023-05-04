using System;
using Member.Common.Model;
using Member.Common.Helper;
using Member.Common.Constants;
using Member.BLL.Interfaces;
using Member.Infrastructure.Abstraction.Interfaces;
using Member.Service.Service.Interfaces;
using Member.DOMAIN.Entity;
using Member.Service.Model;
using Microsoft.Extensions.Options;
using Member.Infrastructure.Repostory;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;

namespace Member.BLL.Manager
{
	public class AccountManger : IAccountManager
	{
		private readonly IRepository<MemberEntity>  _repostory;
        private readonly IAuthorizationService _authorization;
		private readonly IOptions<JWTModel> _jWTModel;
		private readonly IMapper _mapper;
		private readonly IEmailSender _emailSender;
		
		
		

		public AccountManger(IRepository<MemberEntity> repository,
			IAuthorizationService authorization,
			IOptions<JWTModel> options,
			IEmailSender emailSender,
            IMapper mapper)
		{
			_repostory = repository;
            _authorization = authorization;
			_jWTModel = options;
			_mapper = mapper;
			_emailSender = emailSender;
		}

		public async Task<ResultModel<string>> RegisterUserAsync(MemberRegisterModel member)
		{
			Validate
				.For(member)
				.IsNull();

			await IsRegistered(member);
			await _repostory.AddAsync(_mapper.Map<MemberEntity>(member));
			int userId = _repostory.FirstOrDefaultAsync(u => u.phonenumber == member.PhoneNumber).Id;
			var token = _authorization.GenerateToken(userId);
			
            Random random = new Random();
            
			var Verify = token.Substring(token.Length - SuccsesfullMessage.VerifyIndex);
	  	    //await _emailSender.SendEmailSender(member.Email, "Verify Email", $"{Verify} your Verification number");
            await _repostory.SaveChangesAsync();

			return Result.From(SuccsesfullMessage.Secscsesfully);
			
        }

        public async Task<ResultModel<string>> LoginUserAsync(MemberLoginModel member)
		{
			
			await isLogined(member);
			var user = await _repostory.FirstOrDefaultAsync(mem => mem.email == member.Email && mem.password == member.Password);
			Validate
				.For(user)
				.IsNull();
			
            var token = _authorization.GenerateToken(user.ID);
			

			return Result.From($"{SuccsesfullMessage.SecscsLogin} and your token = {token}");
		}
		public async Task isLogined(MemberLoginModel member)
		{
			Validate
				.For(member)
				.IsNull();
			//Validate
			//	.For(member.Email)
			//	.IsNull()
			//	.IsEmpty()
			//	.RegexValidation(RegexConstants.EmailRegex, ExceptionMessage.UsedEmail);
				
		}
		

        public async Task<ResultModel<MemberEntity>> GetInfo(string token)
		{
			var member = await GetUserFromTokenAsync(token).ConfigureAwait(false);
			Validate
				.For(member)
				.IsNull();
			var per = await _repostory.GetByIdAsync(member.ID);
			return Result.From(per);
		}
        private async Task IsRegistered(MemberRegisterModel member)
		{
            var uniqueUser = await isUniqe(member);

			Validate
				.For(member)
				.IsNull()
				.isUniqe(uniqueUser);

            Validate.
				For(member.Name)
				.IsNull()
				.IsEmpty();


			Validate
				.For(member.PhoneNumber)
				.IsNull()
				.RegexValidation(RegexConstants.PhoneNumberRegex, ExceptionMessage.UsedPhoneNumber);
                //.RegexValidation(RegexConstants.EmailRegex, ExceptionMessage.UsedEmail);

        }

        private async Task<bool> isUniqe(MemberRegisterModel member)
		{
            var f = await _repostory.FirstOrDefaultAsync(u => u.email == member.Email && u.phonenumber == member.PhoneNumber).ConfigureAwait(false);
			return f != null;
		}

        private async Task<MemberEntity> GetUserFromTokenAsync(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            int id = Convert.ToInt32(jwt.Claims.First(u => u.Type == "userID").Value);
            return await _repostory.GetByIdAsync(id).ConfigureAwait(false);
        }

        public async Task<MemberEntity> UserUpdate(MemberUpdateModel memberUpdate, string token)
        {
			var currnetUser = await GetUserFromTokenAsync(token);
			Validate
				.For(currnetUser)
				.IsEmpty()
				.IsNull();


			var user = _mapper.Map<MemberUpdateModel, MemberEntity>(memberUpdate, currnetUser);
			_repostory.UpdateAsync(user);
			await _repostory.SaveChangesAsync();
			return user;
        }
    }
}

