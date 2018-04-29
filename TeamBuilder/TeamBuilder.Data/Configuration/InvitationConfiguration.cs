namespace TeamBuilder.Data.Configuration
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	using TeamBuilder.Models;


	class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
	{
		public void Configure(EntityTypeBuilder<Invitation> builder)
		{
			builder.HasKey(i => i.Id);

			builder.HasOne(i => i.InvitedUser)
				.WithMany(iu => iu.ReceivedInvitations)
				.HasForeignKey(i => i.InvitedUserId);

			builder.HasOne(i => i.Team)
				.WithMany(t => t.UsersInvitations)
				.HasForeignKey(i => i.TeamId);
			
		}
	}
}
