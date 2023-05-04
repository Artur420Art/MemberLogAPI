using System;
using System.ComponentModel.DataAnnotations;
namespace Member.DOMAIN.Entity
{
	public class MemberEntity 
	{
		[Key]
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? surname { get; set; }
		public string? phonenumber { get; set; }
		public string? email { get; set; }
		public string? password { get; set; }
	}
}