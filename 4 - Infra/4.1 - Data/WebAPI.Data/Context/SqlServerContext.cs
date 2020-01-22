using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPI.Data.Mapping;
using WebAPI.Domain.Entities;

namespace WebAPI.Data.Context
{
    public class SqlServerContext : DbContext
    {
		private IOptions<AppSettings> settings;
        public DbSet<User> User { get; set; }

		public SqlServerContext(IOptions<AppSettings> settings)
		{
			this.settings = settings;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer(this.settings.Value.DataBaseConnectionString);			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>(new UserMap().Configure);
		}
    }
}