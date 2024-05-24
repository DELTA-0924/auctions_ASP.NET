using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.models
{
    public class Image
    {
        private Image(string fileName)
        {
            FileName= fileName;
        }
        Guid Id { get;  }
        public string FileName { get;} = string.Empty;
        public static Result<Image>Create(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return Result.Failure<Image>($"'{nameof(fileName)}'cannnot be null or empty");
            var image = new Image(fileName);
            return Result.Success(image);
        }
    }
}
