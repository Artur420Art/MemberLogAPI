using System;
namespace Member.Common.Constants
{
	public class ApiRouting
	{
		public const string AccountRouting = "Member";
    }
	public class Endpoints
	{
		public const string Register = $"{ApiRouting.AccountRouting}/Register";
		public const string Login = $"{ApiRouting.AccountRouting}/Login";
		public const string GetInfo = $"{ApiRouting.AccountRouting}/GetInfo";
        public const string ProductAdd = $"{ApiRouting.AccountRouting}/AddProduct";
		public const string UpdateMem = $"{ApiRouting.AccountRouting}/UpdateMember";
		public const string DeleteMem = $"{ApiRouting.AccountRouting}/DeleteMem";
        

    }
}

