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
    public class RatingConfiguration : IEntityTypeConfiguration<RatingEntity>
    {
        public void Configure(EntityTypeBuilder<RatingEntity> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r=>r.Auction).WithMany(a => a.Ratings).HasForeignKey(r=>r.AuctionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(r=>r.Student).WithMany(u => u.Ratings).HasForeignKey(r=>r.StudentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
