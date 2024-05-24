using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Entity
{
    public class RatingEntity
    {
        public Guid Id { get; set; }
        public AuctionEntity? Auction { get; set; }
        public Guid AuctionId { get; set; }
        public int Point {get;set;}        
        public UserEntity? Student{ get; set; }
        public Guid StudentId { get; set; }

    }
}
