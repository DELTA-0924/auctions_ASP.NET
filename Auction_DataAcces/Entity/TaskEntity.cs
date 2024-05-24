using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Entity
{
    public  class TaskEntity
    {
        public Guid Id { get; set; }        
        public string Content { get; set; } = string.Empty;
        public AuctionEntity? Auction { get; set; }
        public Guid AuctionId { get; set; }
        public UserEntity? Student { get; set; }
        public Guid StudentId { get; set; }


    }
}
