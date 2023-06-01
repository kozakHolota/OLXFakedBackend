using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Utils;

namespace OLXFakedBackend.Controllers
{
    [Route("api/images")]
    public class ImageController : Controller
    {
        private IRepositoryWrapper _repositoryWrapper;
        private readonly IFileProvider _fileProvider;

        public ImageController(IRepositoryWrapper repositoryWrapper, IFileProvider fileProvider)
		{
            _repositoryWrapper = repositoryWrapper;
            _fileProvider = fileProvider;
        }

        private async Task<FileContentResult> GetImage(string imagePath)
        {
            if (!System.IO.File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image is not found");
            }

            string mType = Path.GetExtension(imagePath) == "jpg" ? "image/jpeg" : $"image/{Path.GetExtension(imagePath)}";

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return new FileContentResult(imageBytes, mType)
            {
                FileDownloadName = Path.GetFileName(imagePath)
            };
        }

        [Route("item/categories/{imageName}")]
        [HttpGet]
        public async Task<ActionResult> GetCategoryItem(string imageName)
        {
            string imgPath = Path.Combine(ImageUtil.GetCategoryImagesPAth(_fileProvider), imageName);
            try
            {
                return await GetImage(imgPath);
            }
            catch (FileNotFoundException)
            {
                var errorResponse = new
                {
                    StatusCode = 404,
                    Message = $"Image '{imgPath}' not found"
                };
                return NotFound(errorResponse);
            }
        }

        [Route("{userId}/{imageName}")]
        [HttpGet]
        public async Task<ActionResult> GetUserPic(string userId, string imageName)
        {
            string imgFolder = ImageUtil.GetUserPicPath(userId);
            string imgPath = Path.Combine(imgFolder, imageName);
            try
            {
                return await GetImage(imgPath);
            }
            catch (FileNotFoundException)
            {
                var errorResponse = new
                {
                    StatusCode = 404,
                    Message = $"Image '{imageName}' not found"
                };

                return NotFound(errorResponse);
            }
        }

        [Route("item/{itemId}/{imageName}")]
        [HttpGet]
        public async Task<ActionResult> GetItemPic(int itemId, string imageName)
        {
            string imgFolder = ImageUtil.GetItemImagesPath(itemId);
            string imgPath = Path.Combine(imgFolder, imageName);
            try
            {
                return await GetImage(imgPath);
            }
            catch (FileNotFoundException)
            {
                var errorResponse = new
                {
                    StatusCode = 404,
                    Message = $"Image '{imageName}' not found"
                };

                return NotFound(errorResponse);
            }
        }
    }
}

