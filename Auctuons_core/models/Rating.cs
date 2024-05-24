using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.models
{
    public  class Rating
    {
        public Guid Id { get; }
        public Guid StudentId { get;}
        public string? StudentName { get;}
        public Guid AuctionId { get;}
        public int Point { get; }
        private Rating(Guid ratingId,Guid stId,Guid AucId,int point,string? studentName) { 
            StudentId = stId;
            AuctionId = AucId;
            Point = point;
            StudentName = studentName;
            Id = ratingId; 
        }
        public static Result<Rating> Create(Guid stdId,Guid AucId,int point)
        {
            if (point < 0 || point > 100)
            {
                return Result.Failure<Rating>("Invalid point");
            }

            var ratingModel = new Rating(Guid.NewGuid(),stdId, AucId, point,null);
            return Result.Success(ratingModel); 
        }
        public static Rating CreateFromDataBase(Guid ratingId,Guid stdId, Guid AucId, int point,string studentName)
        {          
            var ratingModel = new Rating(ratingId,stdId, AucId, point,studentName);
            return ratingModel;
        }
    }
}
