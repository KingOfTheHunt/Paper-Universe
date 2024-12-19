using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperUniverse.Core.Contexts.AccountContext.Entities;

namespace PaperUniverse.Infra.Context.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        builder.OwnsOne(x => x.Name)
            .Property(y => y.FirstName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);
        
        builder.OwnsOne(x => x.Name)
            .Property(y => y.LastName)
            .IsRequired()
            .HasColumnName("LastName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

        builder.OwnsOne(x => x.Email)
            .Property(y => y.Address)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.Code)
            .IsRequired()
            .HasColumnName("VerificationCode")
            .HasColumnType("CHAR")
            .HasMaxLength(6);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.ExpiresAt)
            .IsRequired(false)
            .HasColumnName("VerificationCodeExpiresAt")
            .HasColumnType("DATETIME");
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.VerifiedAt)
            .IsRequired(false)
            .HasColumnName("VerificationCodeVerifiedAt")
            .HasColumnType("DATETIME");
        
        builder.OwnsOne(x => x.Password)
            .Property(y => y.Hash)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);
        
        builder.OwnsOne(x => x.Password)
            .Property(y => y.ResetCode)
            .IsRequired()
            .HasColumnName("ResetPasswordCode")
            .HasColumnType("CHAR")
            .HasMaxLength(6);

        builder.Ignore(x => x.Notifications);
        builder.OwnsOne(x => x.Name)
            .Ignore(y => y.Notifications);
        builder.OwnsOne(x => x.Email)
            .Ignore(y => y.Notifications);
        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Ignore(z => z.Notifications);
        builder.OwnsOne(x => x.Password)
            .Ignore(y => y.Notifications);
        
        builder.OwnsOne(x => x.Email)
            .HasIndex(y => y.Address)
            .IsUnique();
    }
}