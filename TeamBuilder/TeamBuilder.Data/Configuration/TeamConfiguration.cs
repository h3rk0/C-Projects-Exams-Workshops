using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
	class TeamConfiguration : IEntityTypeConfiguration<Team>
	{
		public void Configure(EntityTypeBuilder<Team> builder)
		{
			builder.HasKey(t => t.Id);

			builder
				.HasIndex(t => t.Name)
				.IsUnique();

			builder
				.Property(t => t.Name)
				.HasMaxLength(25)
				.IsRequired();

			builder
				.Property(t => t.Description)
				.HasMaxLength(32);

			builder
				.Property(t => t.Acronym)
				.HasMaxLength(3)
				.HasColumnType("CHAR(3)")
				.IsRequired();
		}
	}
}
