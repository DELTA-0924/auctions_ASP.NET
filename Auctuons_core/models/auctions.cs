

using System.Data;
using System.Runtime.InteropServices;
using Auctuons_core.Enums;
using CSharpFunctionalExtensions;
namespace Auctuons_core.models
{
    public class Auctions
    {
        public const int MAX_TITLE_LENGTH = 20;
        public const int MAX_DESC_LENGTH = 1000;
        public Auctions() { }
        private Auctions(Guid id,string titleName,string description,DateTime created,DateTime finished,Status status,Guid ?creatorId=null,string ? creator=null) {
            Id = id;    
            TitleName = titleName;  
            Description = description;
            Created = created;
            Finished = finished;
            _status = status;
            CreatorId = creatorId;
            Creator = creator;
        }
        public Guid Id { get; }
        public string TitleName { get; } = string.Empty;
        public string Description { get; }=string.Empty;
        public string ?Creator { get; }
        public DateTime Created { get; }
        public DateTime Finished { get; }
        public Guid? CreatorId { get; }
        public int Views { get; private set; } = 0;
        public Status _status;
        public Image? Image { get; }
        public void CountView() => Views++;
        public static Result<Auctions>Create(Guid id, string titleName, string description, DateTime finished)
        {
            var created = DateTime.Now;
            if (string.IsNullOrEmpty(titleName) || titleName.Length > MAX_TITLE_LENGTH)
                return Result.Failure<Auctions>($"'{nameof(titleName)}'cannot be more then (20)symbols or empty");
            if (string.IsNullOrEmpty(description) || description.Length > MAX_DESC_LENGTH)
                return Result.Failure<Auctions>($"'{nameof(description)}'cannot be more then (1000)symbols or empty");
            if (created>finished)
               return Result.Failure<Auctions>($"'{nameof(finished)}' invalid  date");
            var status = Status.InActive;
            var auction = new Auctions(id, titleName, description, created, finished,status);
            return Result.Success<Auctions>(auction);
        }
        public static Auctions CreateFromDataBase(Guid id, string titleName, string description, DateTime created,DateTime finished,Status status,Guid creatorId,string creator)
        {            
            var auction = new Auctions(id, titleName, description, created, finished, status, creatorId, creator);
            return auction;
        }
    }
}
