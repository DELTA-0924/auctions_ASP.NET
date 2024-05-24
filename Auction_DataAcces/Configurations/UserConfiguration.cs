using Auction_DataAcces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Configurations
{
    public  class UserConfiguration:IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {            
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u=>u.Email).IsRequired();
            builder.Property(u=>u.PasswordHash).IsRequired();
            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRoleEntity>(
                l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(e => e.RoleId),
                r => r.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UserId)                         
                );
        }
    }
}
