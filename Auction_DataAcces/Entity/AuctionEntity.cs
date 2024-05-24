using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Entity
{
    public  class AuctionEntity     
    {
        public Guid Id { get; set; }
        public string TitleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Finished { get; set;}
                
        public UserEntity? Creator{ get; set; }
        public Guid CreatorId { get; set; }

        public ICollection<RatingEntity> Ratings { get; set; } = [];
        public Guid RatingId { get; set; }
    }
}
