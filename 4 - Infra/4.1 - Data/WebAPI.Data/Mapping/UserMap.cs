using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Entities;

namespace WebAPI.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

		    builder.HasKey(c => c.Id);

		    builder.Property(c => c.CPF)
			    .IsRequired()
			    .HasColumnName("Cpf");

		    builder.Property(c => c.BirthDate)
			    .IsRequired()
			    .HasColumnName("BirthDate");

		    builder.Property(c => c.Name)
			    .IsRequired()
			    .HasColumnName("Name");
        }
    }
}