using Auction_DataAcces.Entity;
using Auctuons_core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Configurations
{
    public class PermissionConfiguration:IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.HasKey(x => x.Id);
            var permissions = Enum.GetValues<Permission>().Select(p=> new PermissionEntity
            {
                Id=(int)p,
                Name=p.ToString(),
            });
            builder.HasData(permissions);
        }
    }
}
