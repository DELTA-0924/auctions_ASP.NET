using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.models
{
    public class ModelTask
    {

        public Guid Id { get; }
        public string Content { get; } = string.Empty;
        public string? StudentName { get; }
        public Guid? AuctionId { get; }
        public Guid ?StudentId { get; }
        private ModelTask(Guid id,string content,string? studentName,Guid ? auctionId,Guid ?studentId)
        {
            Id=id;
            Content=content;
            StudentName=studentName;
            AuctionId=auctionId;
            StudentId=studentId;
        }
        public static Result<ModelTask> CreateModelTask(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return Result.Failure<ModelTask>("content is null");
            }
            var modelTask = new ModelTask(Guid.NewGuid(), content,null,null,null);
            return Result.Success<ModelTask>(modelTask);
        }
        public static ModelTask CreateFromDatabase(Guid id,string content,string studentName,Guid studentId)
        {
            var modelTask = new ModelTask(id, content, studentName, null, studentId) ;
            return modelTask;
        }
    }
}
