using System;

namespace Member.Common.Constants
{
	public class RegexConstants
	{
		public const string PhoneNumberRegex = @"^\+374\d{8}$";
		public const string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
	}
}

