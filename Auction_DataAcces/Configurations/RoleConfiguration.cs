using Auction_DataAcces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctuons_core.Enums;
namespace Auction_DataAcces.Configurations
{
    public  class RoleConfiguration:IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity>builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermissionsEntity>(
                l => l.HasOne<PermissionEntity>().WithMany().HasForeignKey(l => l.PermissionId),
                r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(r => r.RoleId));

            var roles = Enum.GetValues<Role>().Select(r => new RoleEntity
            {
                Id = (int)r,
                Name = r.ToString()
            });
            builder.HasData(roles); 
        }
    }
}
