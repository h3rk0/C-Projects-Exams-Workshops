namespace TeamBuilder.Data
{
	using Microsoft.EntityFrameworkCore;
	using TeamBuilder.Data.Configuration;
	using TeamBuilder.Models;
	
	public class TeamBuilderContext : DbContext
	{
		public TeamBuilderContext(DbContextOptions options)
			:base(options)
		{

		}

		public TeamBuilderContext()
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Invitation> Invitations { get; set; }
		public bool Any { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new EventConfiguration());

			builder.ApplyConfiguration(new UserConfiguration());

			builder.ApplyConfiguration(new EventTeamConfiguration());

			builder.ApplyConfiguration(new UserTeamConfiguration());

			builder.ApplyConfiguration(new InvitationConfiguration());

			builder.ApplyConfiguration(new TeamConfiguration());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Connection.ConnectionString);
			}
		}
	}
}
