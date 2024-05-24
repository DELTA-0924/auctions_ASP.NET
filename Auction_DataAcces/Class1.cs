using Auction_DataAcces.Entity;
using Microsoft.EntityFrameworkCore;
using Auction_DataAcces.Configurations;
using Microsoft.Extensions.Options;
using System.Net;
using Auction_DataAcces.Repository;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore.Storage;
namespace Auction_DataAcces
{
    public class AuctionDbContext:DbContext
    {
        IOptions<AuthorizationOptions> _authOptions;   
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options, IOptions<AuthorizationOptions> authOptions):base(options)
        {
            _authOptions = authOptions;
        }
        public DbSet <AuctionEntity> AuctionEntities { get; set; }
        public   DbSet <TaskEntity> TaskEntities { get; set; }
        public DbSet<RatingEntity>RatingEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<RoleEntity> RoleEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new AuctionConfigurtion());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());                        
            modelBuilder.ApplyConfiguration(new RatingConfiguration());                        
            modelBuilder.ApplyConfiguration(new TaskConfiguration());                        
            modelBuilder.ApplyConfiguration(new RolePermissonConfiguration(_authOptions.Value));            
        }
    }
}
