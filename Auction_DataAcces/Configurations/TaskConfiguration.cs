using Auction_DataAcces.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.Id);            
            builder.HasOne(t=>t.Student).WithMany(t=>t.Tasks).HasForeignKey(t => t.StudentId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
