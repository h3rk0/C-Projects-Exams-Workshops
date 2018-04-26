using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.Username).IsRequired();

			builder.Property(u => u.Password).IsRequired();

			builder
				.HasIndex(u => u.Username);

			builder
				.Property(u => u.FirstName)
				.HasMaxLength(25);

			builder
				.Property(u => u.LastName)
				.HasMaxLength(25);

			builder
				.Property(u => u.Password)
				.HasMaxLength(30);

			builder.HasMany(u => u.CreatedTeams)
				.WithOne(t => t.Creator)
				.HasForeignKey(i => i.CreatorId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(u => u.ReceivedInvitations)
				.WithOne(i => i.InvitedUser)
				.HasForeignKey(i => i.InvitedUserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(u => u.CreatedEvents)
				.WithOne(e => e.Creator)
				.HasForeignKey(e => e.CreatorId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
