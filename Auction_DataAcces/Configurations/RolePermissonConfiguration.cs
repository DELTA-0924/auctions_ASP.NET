using Auction_DataAcces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction_DataAcces.Repository;
using Auctuons_core.Enums;
using Serilog;
namespace Auction_DataAcces.Configurations
{

    public  class RolePermissonConfiguration:IEntityTypeConfiguration<RolePermissionsEntity>
    {
        private readonly AuthorizationOptions _authorization;
        public RolePermissonConfiguration(AuthorizationOptions authorization)
        {
            _authorization = authorization;
        }
        public void Configure(EntityTypeBuilder<RolePermissionsEntity> builder)
        {   
            builder.HasKey(r => new { r.RoleId, r.PermissionId });
            builder.HasData(ParseRolePermissions());
        }
        private RolePermissionsEntity[] ParseRolePermissions()
        {                        
            return _authorization.RolePermissions
                .SelectMany(rp => rp.Permissions
                    .Select(p => new RolePermissionsEntity
                    {
                        RoleId = (int)Enum.Parse<Role>(rp.Role),
                        PermissionId = (int)Enum.Parse<Permission>(p)
                    }))
                    .ToArray();
        }
    }
}
