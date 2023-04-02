using System;
using System.Net;
using System.Net.Mime;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Exceptions;
using ChoETL;
using OLXFakedBackend.Models;
using Microsoft.Extensions.FileProviders;

namespace OLXFakedBackend.Controllers
{
    [Route("api/images")]
    public class ImageController : Controller
    {
        private IRepositoryWrapper _repositoryWrapper;
        private readonly IFileProvider _fileProvider;
        private readonly string categoryIconsPath;

        public ImageController(IRepositoryWrapper repositoryWrapper, IFileProvider fileProvider)
		{
            _repositoryWrapper = repositoryWrapper;
            _fileProvider = fileProvider;
            categoryIconsPath = Path.Combine(_fileProvider.GetFileInfo("/").PhysicalPath, "images", "categories");
        }

        private async Task<bool> PlaceImage(IFormFile image, string serverPath) {
            if (image == null || image.Length == 0)
            {
                throw new EmptyImageException("Empty Image");
            }


            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            var fileName = Path.GetExtension(image.FileName);
            var filePath = Path.Combine(serverPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return true;
        }

        private async Task<FileContentResult> GetImage(string imagePath) {
            if (!System.IO.File.Exists(imagePath)) {
                throw new FileNotFoundException("Image is not found");
            }

            string mType = Path.GetExtension(imagePath) == "jpg" ? "image/jpeg" : $"image/{Path.GetExtension(imagePath)}";

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return new FileContentResult(imageBytes, mType)
            {
                FileDownloadName = Path.GetFileName(imagePath)
            };
        }

        private string GetItemImagesPath(string userId) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), userId, "items");
        private string GetUserPicPath(string userId) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), userId, "user");

        // api/images/item/add - POST
        [Produces(MediaTypeNames.Application.Json)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("item/add")]
        [HttpPost]
        public async Task<ActionResult> AddItemImage(IFormFile image) {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (userid == null) return BadRequest("Problems with the authentication. User ID does not exist");
            bool result;
            string imgFolder = GetItemImagesPath(userid);
            string imgPath = Path.Combine(imgFolder, image.FileName);
            try {
                result = await PlaceImage(image, imgFolder);
            } catch(EmptyImageException) {
                return BadRequest("Imge is empty");
            }

            if (!result) return StatusCode(500, new {success=false, error="Couldn't place the image"});

            return Ok(new { success = true, imagePath = image.FileName });
        }

        // api/images/item/{imageName} - GET
        [Route("item/{itemId}/{imageName}")]
        [HttpGet]
        public async Task<ActionResult> GetItemImage(int itemId, string imageName) {
            string userId = (
                await _repositoryWrapper.UserItemRepository.FindByConditions(
                    new List<System.Linq.Expressions.Expression<Func<Models.UserItem, bool>>>() { ui => ui.ItemId == itemId }
                  )
                ).FirstOrDefault<UserItem>().UserId;

            string imgFolder = GetItemImagesPath(userId);
            string imgPath = Path.Combine(imgFolder, imageName);
            try {
                return await GetImage(imgPath);
            } catch(FileNotFoundException) {
                var errorResponse = new
                {
                    StatusCode = 404,
                    Message = $"Image '{imageName}' not found"
                };

                return NotFound(errorResponse);
            }
        }

        // api/images/category/{imageName} - GET
        [Route("item/categories/{imageName}")]
        [HttpGet]
        public async Task<ActionResult> GetCategoryItem(string imageName)
        {
            string imgPath = Path.Combine(categoryIconsPath, imageName);
            try
            {
                return await GetImage(imgPath);
            }
            catch (FileNotFoundException)
            {
                var errorResponse = new {
                    StatusCode = 404,
                    Message = $"Image '{imgPath}' not found"
                };
                return NotFound(errorResponse);
            }
        }

        // api/images/user/add - POST
        [Produces(MediaTypeNames.Application.Json)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("user/add")]
        [HttpPost]
        public async Task<ActionResult> AddUserPic(IFormFile image)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (userid == null) return BadRequest("Problems with the authentication. User ID does not exist");
            bool result;
            string imgFolder = GetUserPicPath(userid);
            string imgPath = Path.Combine(imgFolder, image.FileName);
            try
            {
                result = await PlaceImage(image, imgFolder);
            }
            catch (EmptyImageException)
            {
                return BadRequest("Imge is empty");
            }

            if (!result) return StatusCode(500, new { success = false, error = "Couldn't place the image" });

            return Ok(new { success = true, imagePath = image.FileName });
        }

        // api/images/user/{imageName} - GET
        [Route("{userId}/{imageName}")]
        [HttpGet]
        public async Task<ActionResult> GetUserPic(string userId, string imageName) {
            string imgFolder = GetUserPicPath(userId);
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

