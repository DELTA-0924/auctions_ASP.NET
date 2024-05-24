using System.Text;

namespace Auctions_Web_API.Contracts
{
    public class AuctionResponce
    { 
   public  Guid Id { get; set; }
    public string TitleName { get; set; }
    public string Description { get; set;}
    public DateTime Created { get; set; }
    public DateTime Finished { get; set; }
    public string Status { get; set; }
    public Guid? CreatorId { get; set; }
    public string? Creator { get; set; }

        public AuctionResponce(Guid id,string titleName,string description,DateTime created,DateTime finished,string status,Guid ?creatorId,string creator) {
            Id = id;
            TitleName = titleName;
            Description = description;
            Created = created;
            Finished = finished;
            Status = status;
            CreatorId = creatorId;
            Creator = creator;
        }
        public  StringBuilder print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(TitleName);
               sb.AppendLine(Description);
                sb.AppendLine(Created.ToString());
                sb.AppendLine(Finished.ToString());
            return sb;                    
        } 
    };
}
