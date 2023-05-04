using System;
using Member.Service.Model;

namespace Member.Service.Service.Interfaces
{
	public interface IAuthorizationService
	{
        //string GetJwtToken(int userId, JWTModel jWTModel);
        string GenerateToken(int userID);
        bool VerifyToken(string token);

    }
}

