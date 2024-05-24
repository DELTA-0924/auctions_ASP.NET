using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctuons_core.models;
using Microsoft.AspNetCore.Http;
namespace Auction.Application.Services
{
    public class ImageService
    {
        public async Task<Result<Image>>CreateImage(IFormFile titleImage,string path)
        {
            try
            {
                var filename = Path.GetFileName(titleImage.FileName);
                var filepath = Path.Combine(path, filename);
                await using (var stream =new  FileStream(filepath, FileMode.Create))
                {
                    await titleImage.CopyToAsync(stream);

                }
                var image = Image.Create(filepath);
                return image;
            }
            catch(Exception ex)
            {
                return Result.Failure<Image>(ex.Message);
            }
        }
    }
}
