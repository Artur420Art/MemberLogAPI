using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Member.Common.Model;
using MySql.Data.MySqlClient;
using Member.DOMAIN.Entity;
using Member.Context;
using System.Data.Entity;
using Member.Infrastructure.Abstraction.Interfaces;
using System.Net.Mail;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using Member.Common.Helper;
using Member.Common.Constants;
using Member.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Member.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        private readonly IEmailSender _emailSender;

        public MemberController(IAccountManager accountManager,
            IEmailSender emailSender)
        {
            _accountManager = accountManager;
            _emailSender = emailSender;
        }
       

        [HttpPost(Endpoints.Register)]
        public async Task<IActionResult> MemberRegister(MemberRegisterModel member)
        {
            return Ok(await _accountManager.RegisterUserAsync(member).ConfigureAwait(false));
        }

        [HttpPost(Endpoints.Login)]
        public async Task<IActionResult> MemberLogin(MemberLoginModel member)
        {
            
            return Ok(await _accountManager.LoginUserAsync(member).ConfigureAwait(false));
        }

        [HttpGet(Endpoints.GetInfo), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetJwtTk()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _accountManager.GetInfo(token).ConfigureAwait(false));
        }
        [HttpPatch(Endpoints.UpdateMem), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateMember(MemberUpdateModel memberUpdate)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _accountManager.UserUpdate(memberUpdate, token).ConfigureAwait(false));
        }
        [HttpDelete(Endpoints.DeleteMem), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUser(string Token)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(await _accountManager.DeleteUser(token).ConfigureAwait(false));
        }

    }
}

