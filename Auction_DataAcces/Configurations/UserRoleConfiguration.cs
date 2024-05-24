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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>

    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.HasKey(x => new { x.RoleId,x.UserId});
        }
    }
}
