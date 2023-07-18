using Microsoft.EntityFrameworkCore;
using Member.DOMAIN.Entity;

namespace Member.Context
{

	public class MemberContext : DbContext
	{

		public DbSet<MemberEntity> Users { get; set; }


		public MemberContext() : base()
		{

		}

		public MemberContext(DbContextOptionsBuilder<MemberContext> options)
			: base()
		{
			OnConfiguring(options);

		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
            options.UseMySql("server=localhost;database=Members;user=root;password=Artur2023art", new MySqlServerVersion(new Version(8, 0, 23)));

			base.OnConfiguring(options);
		}
		

	}
}


