using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Entity
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? AboutMe { get; set; } = string.Empty;
        public string? FirstName { get; set; } =string.Empty;
        public string? SurName { get; set; }=string.Empty;
        public int? Age { get; set; } = 0;
        public string AboutCompany { get; set; } = string.Empty;
        public ICollection<AuctionEntity> Auctions { get; set; } = [];
        public ICollection<TaskEntity> Tasks { get; set; } = [];
        public ICollection<RoleEntity> Roles { get; set; } = [];
        public ICollection<RatingEntity> Ratings{ get; set; } = [];
        
    }    
}
