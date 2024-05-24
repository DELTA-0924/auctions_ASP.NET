using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auction_DataAcces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Auction_DataAcces.Configurations
{
    public  class AuctionConfigurtion:IEntityTypeConfiguration<AuctionEntity>
    {
        public void Configure(EntityTypeBuilder<AuctionEntity>builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Finished).IsRequired();
            builder.Property(x=>x.Description).IsRequired();
            builder.Property(x=>x.TitleName).IsRequired().HasMaxLength(30);            
            builder.HasOne(a=>a.Creator).WithMany(r => r.Auctions).HasForeignKey(r => r.CreatorId);
        }
    }
}
