namespace TeamBuilder.Data.Configuration
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using TeamBuilder.Models;


	class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
	{
		public void Configure(EntityTypeBuilder<UserTeam> builder)
		{
			builder.HasKey(et => new { et.UserId, et.TeamId });

			builder.HasOne(ut => ut.User)
				.WithMany(u => u.UserTeams)
				.HasForeignKey(ut => ut.UserId);

			builder.HasOne(ut => ut.Team)
				.WithMany(t => t.UserTeams)
				.HasForeignKey(ut => ut.TeamId);
		}
	}
}
